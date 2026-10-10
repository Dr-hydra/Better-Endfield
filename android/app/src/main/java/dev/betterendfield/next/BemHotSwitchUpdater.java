package dev.betterendfield.next;

import android.content.Context;
import android.content.SharedPreferences;
import java.util.function.Consumer;
import java.util.function.Supplier;

/** Experimental selection updates; the validation/experiment modes stay latched at startup. */
final class BemHotSwitchUpdater {
    static volatile boolean running;
    private BemHotSwitchUpdater() {}

    static void start(Context context,Supplier<SharedPreferences> preferences,
            BemInstalledResources.Source source,String initialIndex,
            boolean skipValidation,boolean hotSwitch,boolean fastLoading,boolean cloneSupport,
            Consumer<String> log) {
        if(!hotSwitch) return;
        Thread worker=new Thread(()->{
            BemHotSwitchUpdate transaction=new BemHotSwitchUpdate(initialIndex);
            String lastFailure="";
            while(!Thread.currentThread().isInterrupted()) {
                try {
                    if(RuntimeBootstrap.loaded()) {
                        boolean queued=transaction.update(
                            ()->preferences.get().getString(BemInstaller.INDEX,"[]"),
                            index->BemInstalledResources.prepare(context,index,source,log,
                                skipValidation,true,fastLoading,false,cloneSupport),
                            configuration->{
                                if(!NativeCommandBridge.updateCustomModelConfig(configuration)) return false;
                                BemInstalledResources.configuration=configuration;
                                return true;
                            });
                        if(queued) log.accept("Experimental BEM selection queued for runtime refresh");
                        lastFailure="";
                    }
                } catch(Exception | LinkageError error) {
                    String failure=error.getClass().getSimpleName()+": "+error.getMessage();
                    if(!failure.equals(lastFailure)) {
                        log.accept("Experimental BEM update failed; previous selection retained: "+failure);
                        lastFailure=failure;
                    }
                }
                try {Thread.sleep(500);} catch(InterruptedException stopped) {
                    Thread.currentThread().interrupt();return;
                }
            }
        },"BetterEndfieldNext-BemHotSwitch");
        worker.setDaemon(true);worker.start();running=true;
    }
}
