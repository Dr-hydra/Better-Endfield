package dev.betterendfield.next;

import java.io.*;
import java.nio.charset.Charset;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.file.Files;
import java.util.*;
import java.util.zip.*;

/** Runs the actual BEM/ZIP staging code with real ZIP streams and filesystem bytes. */
final class BemImportArchiveTest {
    private static int checks;
    private static final BemImportStream.Checkpoint CONTINUE=()->{};
    private interface Operation {void run() throws Exception;}
    private static void check(boolean condition,String message) {checks++;if(!condition)throw new AssertionError(message);}
    private static void rejects(Operation operation) throws Exception {
        try {operation.run();throw new AssertionError("Unexpected acceptance");}
        catch(IOException expected) {checks++;}
    }
    private static byte[] bem(int size,int value) {
        byte[] bytes=new byte[size];Arrays.fill(bytes,(byte)value);
        System.arraycopy(new byte[]{'B','E','M',0,'P','K','G',0},0,bytes,0,8);return bytes;
    }
    private static byte[] zip(Map<String,byte[]> entries,Charset charset,boolean stored) throws Exception {
        ByteArrayOutputStream bytes=new ByteArrayOutputStream();
        try(ZipOutputStream zip=new ZipOutputStream(bytes,charset)) {
            for(var item:entries.entrySet()) {
                ZipEntry entry=new ZipEntry(item.getKey());
                if(stored) {CRC32 crc=new CRC32();crc.update(item.getValue());entry.setMethod(ZipEntry.STORED);
                    entry.setSize(item.getValue().length);entry.setCrc(crc.getValue());}
                zip.putNextEntry(entry);zip.write(item.getValue());zip.closeEntry();
            }
        }
        return bytes.toByteArray();
    }
    private static byte[] zip(Map<String,byte[]> entries) throws Exception {return zip(entries,Charset.forName("UTF-8"),false);}
    private static byte[] zip64(byte[] ordinary) {
        int end=ordinary.length-22;ByteBuffer old=ByteBuffer.wrap(ordinary).order(ByteOrder.LITTLE_ENDIAN);
        int count=Short.toUnsignedInt(old.getShort(end+10));
        long size=Integer.toUnsignedLong(old.getInt(end+12)),offset=Integer.toUnsignedLong(old.getInt(end+16));
        byte[] result=new byte[ordinary.length+76];System.arraycopy(ordinary,0,result,0,end);
        ByteBuffer out=ByteBuffer.wrap(result).order(ByteOrder.LITTLE_ENDIAN);out.position(end);
        out.putInt(0x06064b50).putLong(44).putShort((short)45).putShort((short)45).putInt(0).putInt(0)
                .putLong(count).putLong(count).putLong(size).putLong(offset);
        out.putInt(0x07064b50).putInt(0).putLong(end).putInt(1);
        System.arraycopy(ordinary,end,result,end+76,22);
        out.putShort(end+76+8,(short)0xffff).putShort(end+76+10,(short)0xffff)
                .putInt(end+76+12,-1).putInt(end+76+16,-1);
        return result;
    }
    private static File stage(File root) throws IOException {
        File stage=new File(root,"stage-"+UUID.randomUUID());if(!stage.mkdir())throw new IOException("fixture mkdir");return stage;
    }
    private static BemImportArchive.Bundle read(byte[] input,File stage) throws Exception {
        return BemImportArchive.read(new ByteArrayInputStream(input),stage,CONTINUE,text->{});
    }
    private static BemImportArchive.Bundle limited(byte[] input,File stage,long bytes,int packages) throws Exception {
        File file=new File(stage,"fixture.zip");Files.write(file.toPath(),input);
        return BemImportArchive.unpack(file,stage,CONTINUE,text->{},bytes,packages);
    }
    public static void main(String[] args) throws Exception {
        File root=Files.createTempDirectory("bem-zip-regression-").toFile();
        try {
            byte[] first=bem(32,1),second=bem(48,2);
            boolean[] closed={false};
            InputStream shortReads=new ByteArrayInputStream(first) {
                int reads;
                @Override public synchronized int read(byte[] data,int offset,int length) {
                    return (++reads%4==0)?0:super.read(data,offset,Math.min(3,length));
                }
                @Override public void close(){closed[0]=true;}
            };
            var raw=BemImportArchive.read(shortReads,stage(root),CONTINUE,text->{});
            check(!raw.zip && raw.packages.size()==1,"Single BEM remains supported");
            check(Arrays.equals(first,Files.readAllBytes(raw.packages.get(0).file.toPath())),"Raw BEM changed");
            check(!closed[0],"Caller stream ownership changed");

            var entries=new LinkedHashMap<String,byte[]>();
            entries.put("a/model.bem",first);entries.put("b/model.BEM",second);
            entries.put("README.txt",new byte[]{1});entries.put("invalid.bem",new byte[32]);
            var bundle=read(zip(entries),stage(root));
            check(bundle.zip && bundle.packages.size()==2 && bundle.issues.size()==1,"Nested mixed collection");
            check(Arrays.equals(first,Files.readAllBytes(bundle.packages.get(0).file.toPath())),"First ZIP payload changed");
            check(Arrays.equals(second,Files.readAllBytes(bundle.packages.get(1).file.toPath())),"Second ZIP payload changed");
            check(!bundle.packages.get(0).file.equals(bundle.packages.get(1).file),"Same basename collided");
            check(bundle.packages.stream().allMatch(i->i.file.getName().equals("installed.bem")),"Archive filenames became disk paths");

            var legacy=read(zip(Map.of("中文/衣服.bem",first),Charset.forName("GBK"),false),stage(root));
            check(legacy.packages.get(0).name.equals("中文/衣服.bem"),"GBK ZIP names unsupported");
            var stored=read(zip(Map.of("plain.bem",first),Charset.forName("UTF-8"),true),stage(root));
            check(Arrays.equals(first,Files.readAllBytes(stored.packages.get(0).file.toPath())),"Stored ZIP unsupported");
            var largeDirectory=read(zip64(zip(Map.of("zip64.bem",first))),stage(root));
            check(largeDirectory.packages.size()==1 && Arrays.equals(first,Files.readAllBytes(largeDirectory.packages.get(0).file.toPath())),"ZIP64 directory unsupported");
            byte[] falseCount=zip(entries);
            ByteBuffer falseDirectory=ByteBuffer.wrap(falseCount).order(ByteOrder.LITTLE_ENDIAN);
            falseDirectory.putShort(falseCount.length-22+8,(short)1).putShort(falseCount.length-22+10,(short)1);
            rejects(()->read(falseCount,stage(root)));
            byte[] excessive=zip(Map.of("small.bem",first));
            ByteBuffer excessiveDirectory=ByteBuffer.wrap(excessive).order(ByteOrder.LITTLE_ENDIAN);
            excessiveDirectory.putShort(excessive.length-22+8,(short)4097).putShort(excessive.length-22+10,(short)4097);
            rejects(()->read(excessive,stage(root)));

            var unsafe=new LinkedHashMap<String,byte[]>();
            unsafe.put("../escape.bem",first);unsafe.put("/absolute.bem",first);unsafe.put("C:\\drive.bem",first);
            unsafe.put("safe.bem",second);
            var filtered=read(zip(unsafe),stage(root));
            check(filtered.packages.size()==1 && filtered.issues.size()==3,"Unsafe paths did not remain invalid items");
            check(!new File(root,"escape.bem").exists(),"ZIP path escaped staging");
            var duplicate=new LinkedHashMap<String,byte[]>();duplicate.put("model.bem",first);duplicate.put("MODEL.BEM",second);
            var samePath=read(zip(duplicate),stage(root));
            check(samePath.packages.size()==1 && samePath.issues.size()==1,"Case-colliding ZIP path accepted");

            byte[] corrupt=zip(Map.of("broken.bem",first),Charset.forName("UTF-8"),true);
            int dataOffset=30+"broken.bem".length();corrupt[dataOffset+first.length-1]^=1;
            var badCrc=read(corrupt,stage(root));
            check(badCrc.packages.isEmpty() && badCrc.issues.get(0).contains("CRC"),"CRC corruption accepted");
            var budgetEntries=new LinkedHashMap<String,byte[]>();budgetEntries.put("a.bem",first);budgetEntries.put("b.bem",first);
            var bounded=limited(zip(budgetEntries),stage(root),32,10);
            check(bounded.packages.size()==1 && bounded.issues.size()==1,"Uncompressed total limit ignored");
            byte[] invalidBudget=zip(budgetEntries,Charset.forName("UTF-8"),true);
            invalidBudget[30+"a.bem".length()+first.length-1]^=1;
            var failedBudget=limited(invalidBudget,stage(root),32,10);
            check(failedBudget.packages.isEmpty() && failedBudget.issues.size()==2,"Invalid CRC bytes escaped total budget");
            rejects(()->limited(zip(budgetEntries),stage(root),4096,1));
            rejects(()->read(zip(Map.of("README.txt",first)),stage(root)));
            rejects(()->read(zip(Map.of()),stage(root)));
            rejects(()->read(new byte[16],stage(root)));
            rejects(()->read(Arrays.copyOf(zip(entries),zip(entries).length-10),stage(root)));

            byte[] valid=zip(Map.of("cancel.bem",bem(150000,7)));
            boolean[] extracting={false};File cancelled=stage(root);
            rejects(()->BemImportArchive.read(new ByteArrayInputStream(valid),cancelled,
                    ()->{if(extracting[0])throw new IOException("cancelled");},text->{if(text.contains("cancel.bem"))extracting[0]=true;}));
            check(!new File(cancelled,"input.zip").exists(),"Cancelled compressed copy was retained");
            check(BemImportArchive.MAX_ENTRIES==4096 && BemImportArchive.MAX_PACKAGES==256,"Collection limits changed");
            System.out.println("PASS BEM/ZIP staging: "+checks+" checks; mixed/nested bundles, byte preservation, CRC, encoding, paths, budgets and cancellation");
        } finally {
            try(var files=Files.walk(root.toPath())) {files.sorted(Comparator.reverseOrder()).forEach(path->path.toFile().delete());}
        }
    }
}
