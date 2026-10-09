package dev.betterendfield.next;

import android.content.Context;
import android.content.SharedPreferences;
import android.util.Log;

import java.util.Map;
import java.util.Set;
import java.io.FileOutputStream;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import android.os.ParcelFileDescriptor;
import io.github.libxposed.service.XposedService;
import io.github.libxposed.service.XposedServiceHelper;

/** Local UI preferences remain authoritative; framework service publishes snapshots. */
final class FrameworkSettings {
    private static XposedService service;
    private static SharedPreferences local;
    private static XposedService remoteService;
    // SharedPreferences keeps weak references; retain this for the application lifetime.
    private static final SharedPreferences.OnSharedPreferenceChangeListener listener = (prefs, key) -> publish();

    static void initialize(Context context) {
        local = open(context);
        try { OverlayWriteAuthorization.initialize(context); }
        catch (java.io.IOException unavailable) { Log.e("BetterEndfieldNext.Settings", "overlay authorization unavailable", unavailable); }
        local.registerOnSharedPreferenceChangeListener(listener);
        XposedServiceHelper.registerListener(new XposedServiceHelper.OnServiceListener() {
            @Override public void onServiceBind(XposedService connected) {
                synchronized (FrameworkSettings.class) {
                    service = connected; remoteService = connected;
                    FrameworkSettings.class.notifyAll();
                    publish();
                    OverlaySettingsDiagnostics.record(context, "framework=connected authorization_ready="
                            + OverlayWritePolicy.validToken(OverlayWriteAuthorization.ownerToken()));
                }
            }
            @Override public void onServiceDied(XposedService disconnected) {
                synchronized (FrameworkSettings.class) { if (service == disconnected) { service = null; remoteService = null; } }
            }
        });
    }

    static synchronized boolean writeRemoteCommand(String payload) {
        if (remoteService == null || payload == null) return false;
        try (ParcelFileDescriptor descriptor = remoteService.openRemoteFile("command.next");
                FileOutputStream stream = new FileOutputStream(descriptor.getFileDescriptor())) {
            stream.write(payload.getBytes(java.nio.charset.StandardCharsets.UTF_8));
            stream.flush();
            return true;
        } catch (RuntimeException | java.io.IOException error) {
            return false;
        }
    }

    static synchronized boolean writeRemoteStatus(String payload) {
        if (remoteService == null || payload == null) return false;
        try (ParcelFileDescriptor descriptor = remoteService.openRemoteFile("command.status");
                FileOutputStream stream = new FileOutputStream(descriptor.getFileDescriptor())) {
            stream.write(payload.getBytes(java.nio.charset.StandardCharsets.UTF_8));
            stream.flush();
            return true;
        } catch (RuntimeException | java.io.IOException error) {
            return false;
        }
    }

    static synchronized String readRemoteStatus() {
        if (remoteService == null) return "";
        try (ParcelFileDescriptor descriptor = remoteService.openRemoteFile("command.status");
                BufferedReader reader = new BufferedReader(new InputStreamReader(
                        new ParcelFileDescriptor.AutoCloseInputStream(descriptor),
                        java.nio.charset.StandardCharsets.UTF_8))) {
            StringBuilder value = new StringBuilder(); String line;
            while ((line = reader.readLine()) != null) value.append(line).append('\n');
            return value.toString();
        } catch (RuntimeException | java.io.IOException error) { return ""; }
    }

    static SharedPreferences open(Context context) {
        return context.getSharedPreferences("module_settings", Context.MODE_PRIVATE);
    }

    static synchronized boolean isConnected() { return remoteService != null; }

    static void awaitThirdPartyConnection() throws java.io.IOException {
        if(!FrameworkServiceWait.await(FrameworkSettings.class,()->remoteService!=null,10_000,()->{}))
            throw new java.io.IOException("框架服务未连接；模块 ZIP 尚未发布，请启用框架模块后重试");
    }
    static synchronized void publishThirdParty(java.io.File file,String name) throws java.io.IOException {
        if(remoteService==null || !name.matches("tpm-[a-f0-9-]{36}\\.zip") || !file.isFile())throw new java.io.IOException("无效的模块 ZIP 发布请求");
        try(ParcelFileDescriptor descriptor=remoteService.openRemoteFile(name);java.io.FileInputStream input=new java.io.FileInputStream(file);
            FileOutputStream output=new FileOutputStream(descriptor.getFileDescriptor())) {
            output.getChannel().truncate(0);byte[] buffer=new byte[65536];long total=0;int count;
            while((count=input.read(buffer))!=-1){total+=count;if(total>ThirdPartyModulePackage.LIMIT)throw new java.io.IOException("模块 ZIP 过大");output.write(buffer,0,count);}output.getFD().sync();
        }
    }
    static synchronized boolean removeThirdParty(String name) {
        if(remoteService==null || !name.matches("tpm-[a-f0-9-]{36}\\.zip"))return false;
        try {return remoteService.deleteRemoteFile(name)||!java.util.Arrays.asList(remoteService.listRemoteFiles()).contains(name);}
        catch(RuntimeException error){return false;}
    }

    static void awaitConnection() throws java.io.IOException {
        if (!FrameworkServiceWait.await(FrameworkSettings.class, () -> remoteService != null,
                10_000, BemInstaller::checkpoint))
            throw new java.io.IOException("框架服务未连接；请确认已在框架中启用模块后重试");
    }

    static synchronized boolean removeBem(String name) {
        if(remoteService==null || !name.matches("bem-[a-f0-9-]{36}\\.bem")) return false;
        try {
            if(remoteService.deleteRemoteFile(name)) return true;
            return !java.util.Arrays.asList(remoteService.listRemoteFiles()).contains(name);
        } catch(RuntimeException error) {Log.e("BetterEndfieldNext.Install","Removing shared package failed",error);return false;}
    }
    static synchronized String[] listBem() {
        if(remoteService==null) return new String[0];
        try {
            java.util.ArrayList<String> packages=new java.util.ArrayList<>();
            for(String name:remoteService.listRemoteFiles()) if(name.matches("bem-[a-f0-9-]{36}\\.bem")) packages.add(name);
            return packages.toArray(new String[0]);
        } catch(RuntimeException error) {return new String[0];}
    }
    /** Returns -1 when the shared copy is absent; opening would otherwise create an empty file. */
    static synchronized long bemSize(String name) {
        if(remoteService==null || !name.matches("bem-[a-f0-9-]{36}\\.bem")) return -1;
        try {
            if(!java.util.Arrays.asList(remoteService.listRemoteFiles()).contains(name)) return -1;
            try(ParcelFileDescriptor descriptor=remoteService.openRemoteFile(name)) {return descriptor.getStatSize();}
        } catch(RuntimeException | java.io.IOException error) {return -1;}
    }
    static synchronized java.io.InputStream openBem(String name) throws java.io.IOException {
        if(bemSize(name)<=0) throw new java.io.IOException("框架中缺少模型包文件，请重新导入");
        try {return new ParcelFileDescriptor.AutoCloseInputStream(remoteService.openRemoteFile(name));}
        catch(RuntimeException error) {throw new java.io.IOException("模型包读取失败："+error.getMessage(),error);}
    }
    static synchronized void publishBem(java.io.File file,String name) throws java.io.IOException {
        if(remoteService==null) throw new java.io.IOException("框架服务已断开，请重试");
        if(!name.matches("bem-[a-f0-9-]+\\.bem") || !file.isFile())
            throw new java.io.IOException("无效的模型包发布文件");
        try(ParcelFileDescriptor descriptor=remoteService.openRemoteFile(name);
            java.io.FileInputStream in=new java.io.FileInputStream(file);
            FileOutputStream out=new FileOutputStream(descriptor.getFileDescriptor())) {
            out.getChannel().truncate(0);
            BemInstaller.copy(in,out,2L*1024*1024*1024);out.getFD().sync();
        } catch(java.io.IOException | RuntimeException error) {
            Log.e("BetterEndfieldNext.Install","Publishing failed",error);
            throw new java.io.IOException("模型包发布失败："+error.getMessage(),error);
        }
    }

    static boolean validMmdRemote(String name) {
        return name != null && name.length() <= 200
                && name.matches("mmd-[a-f0-9-]{36}-[A-Za-z0-9_.-]+");
    }
    static synchronized boolean publishMmd(java.io.File file, String name) {
        if (remoteService == null || !validMmdRemote(name) || !file.isFile()) return false;
        try (ParcelFileDescriptor descriptor = remoteService.openRemoteFile(name);
             java.io.FileInputStream in = new java.io.FileInputStream(file);
             FileOutputStream out = new FileOutputStream(descriptor.getFileDescriptor())) {
            out.getChannel().truncate(0);
            byte[] buffer = new byte[65536]; long total = 0; int count;
            while ((count = in.read(buffer)) != -1) {
                total += count;
                if (total > 512L * 1024 * 1024) throw new java.io.IOException("作品文件过大");
                out.write(buffer, 0, count);
            }
            out.getFD().sync(); return true;
        } catch (Exception error) {
            Log.e("BetterEndfieldNext.Mmd", "Publishing work failed", error); return false;
        }
    }
    static synchronized boolean removeMmd(String name) {
        if (remoteService == null || !validMmdRemote(name)) return false;
        try {
            return remoteService.deleteRemoteFile(name)
                    || !java.util.Arrays.asList(remoteService.listRemoteFiles()).contains(name);
        } catch (RuntimeException error) { return false; }
    }

    @SuppressWarnings("unchecked")
    private static synchronized void publish() {
        if (service == null || local == null) return;
        try {
            SharedPreferences remote = service.getRemotePreferences("module_settings");
            SharedPreferences.Editor edit = remote.edit().clear();
            for (Map.Entry<String, ?> entry : local.getAll().entrySet()) {
                String key = entry.getKey(); Object value = entry.getValue();
                if (value instanceof String) edit.putString(key, (String) value);
                else if (value instanceof Boolean) edit.putBoolean(key, (Boolean) value);
                else if (value instanceof Integer) edit.putInt(key, (Integer) value);
                else if (value instanceof Long) edit.putLong(key, (Long) value);
                else if (value instanceof Float) edit.putFloat(key, (Float) value);
                else if (value instanceof Set<?>) edit.putStringSet(key, (Set<String>) value);
            }
            // The secret never belongs to the UI preference map or a public provider response.
            String authorization = OverlayWriteAuthorization.ownerToken();
            if (OverlayWritePolicy.validToken(authorization)) edit.putString(OverlayWriteAuthorization.PREFERENCE, authorization);
            else edit.remove(OverlayWriteAuthorization.PREFERENCE);
            edit.putInt("schemaVersion", 1);
            edit.putLong("generation", remote.getLong("generation", 0) + 1);
            if (!edit.commit()) Log.e("BetterEndfieldNext.Settings", "framework snapshot commit failed");
        } catch (RuntimeException error) {
            Log.e("BetterEndfieldNext.Settings", "framework snapshot unavailable; local settings retained", error);
        }
    }
}
