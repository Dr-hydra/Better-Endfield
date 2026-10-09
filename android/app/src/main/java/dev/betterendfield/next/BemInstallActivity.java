package dev.betterendfield.next;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.widget.ScrollView;

/** External BEM file entry; shares the main tab's package management content. */
public final class BemInstallActivity extends Activity {
    private BemInstallPage page;

    @Override public void onCreate(Bundle state) {
        super.onCreate(state);
        setContentView(R.layout.activity_bem_install);
        ScrollView scroll=findViewById(R.id.bem_scroll);
        scroll.setOnApplyWindowInsetsListener((view,insets)->{
            view.setPadding(insets.getSystemWindowInsetLeft(),insets.getSystemWindowInsetTop(),
                    insets.getSystemWindowInsetRight(),insets.getSystemWindowInsetBottom());
            return insets;
        });
        page=new BemInstallPage(this,findViewById(R.id.bem_content),state);
        if(state==null) page.receiveImport(getIntent());
    }

    @Override protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        setIntent(intent);
        page.receiveImport(intent);
    }

    @Override protected void onSaveInstanceState(Bundle state) {
        page.saveState(state);
        super.onSaveInstanceState(state);
    }

    @Override protected void onActivityResult(int request,int result,Intent data) {
        super.onActivityResult(request,result,data);
        page.onActivityResult(request,result,data);
    }

    @Override protected void onResume(){super.onResume();page.resume();}
    @Override protected void onPause(){page.pause();super.onPause();}
    @Override protected void onDestroy(){page.close();super.onDestroy();}
}
