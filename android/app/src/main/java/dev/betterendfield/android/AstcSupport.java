package dev.betterendfield.android;

import android.opengl.*;

/** Conservative EGL capability query on the installation worker, never the game GL context. */
final class AstcSupport {
    static boolean available() {
        EGLDisplay display=EGL14.eglGetDisplay(EGL14.EGL_DEFAULT_DISPLAY);
        EGLContext context=EGL14.EGL_NO_CONTEXT;EGLSurface surface=EGL14.EGL_NO_SURFACE;
        try {
            if(!EGL14.eglInitialize(display,new int[2],0,new int[2],0)) return false;
            EGLConfig[] configs=new EGLConfig[1];int[] count=new int[1];
            int[] attrs={EGL14.EGL_RENDERABLE_TYPE,4,EGL14.EGL_SURFACE_TYPE,EGL14.EGL_PBUFFER_BIT,EGL14.EGL_NONE};
            if(!EGL14.eglChooseConfig(display,attrs,0,configs,0,1,count,0)||count[0]==0) return false;
            context=EGL14.eglCreateContext(display,configs[0],EGL14.EGL_NO_CONTEXT,new int[]{EGL14.EGL_CONTEXT_CLIENT_VERSION,2,EGL14.EGL_NONE},0);
            surface=EGL14.eglCreatePbufferSurface(display,configs[0],new int[]{EGL14.EGL_WIDTH,1,EGL14.EGL_HEIGHT,1,EGL14.EGL_NONE},0);
            if(!EGL14.eglMakeCurrent(display,surface,surface,context)) return false;
            String extensions=GLES20.glGetString(GLES20.GL_EXTENSIONS);
            return extensions!=null && java.util.Arrays.asList(extensions.split(" ")).contains("GL_KHR_texture_compression_astc_ldr");
        } finally {
            EGL14.eglMakeCurrent(display,EGL14.EGL_NO_SURFACE,EGL14.EGL_NO_SURFACE,EGL14.EGL_NO_CONTEXT);
            if(surface!=EGL14.EGL_NO_SURFACE) EGL14.eglDestroySurface(display,surface);
            if(context!=EGL14.EGL_NO_CONTEXT) EGL14.eglDestroyContext(display,context);
            EGL14.eglTerminate(display);EGL14.eglReleaseThread();
        }
    }
}
