#if !UNITY_EDITOR && UNITY_ANDROID
using UnityEngine;
#endif

public abstract class AndroidPlugin : PlatformPlugin {

    protected abstract string JavaClassName { get; }

#if !UNITY_EDITOR && UNITY_ANDROID
    private const string UnityPackageName = "com.unity3d.player.UnityPlayer";
    private const string PluginPackageName = "com.bdovaz.supersensor.Sensor";

    protected AndroidJavaObject unityActivity;
    private AndroidJavaObject plugin;
#endif

    public AndroidPlugin(string listenerName) : base(listenerName) {
#if !UNITY_EDITOR && UNITY_ANDROID
        AndroidJNI.AttachCurrentThread();

        unityActivity = new AndroidJavaClass(UnityPackageName).GetStatic<AndroidJavaObject>("currentActivity");
        
        plugin = new AndroidJavaObject(PluginPackageName, unityActivity);

        SetListenerName(this.listenerName);
#endif
    }

    protected override void SetListenerName(string name) {
        Call("setListenerName", name);
    }

    protected void Call(string method, params object[] args) {
#if !UNITY_EDITOR && UNITY_ANDROID
        plugin.Call(method, args);
#endif
    }

    protected T Call<T>(string method, params object[] args) {
#if !UNITY_EDITOR && UNITY_ANDROID
        return plugin.Call<T>(method, args);
#else
        return default(T);
#endif
    }

    protected void CallOnUiThread(string method, params object[] args) {
#if !UNITY_EDITOR && UNITY_ANDROID
        unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
            Call(method, args);
        }));
#endif
    }

}