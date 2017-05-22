using System;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class Plugin<T> where T : class, new() {

    #region Singleton
    private static T instance = new T();

    public static T Instance {
        get { return instance; }
    }
    #endregion

    public string ListenerName {
        get { return string.Format("{0}EventsListener", GetType().Name); }
    }

    public Plugin() { }

    protected Plugin (Type listenerType) : this() {
        CreateListener(listenerType);
    }

    private void CreateListener(Type type) {
        GameObject listener = new GameObject(ListenerName);
        listener.hideFlags = HideFlags.HideInHierarchy;
        listener.AddComponent(type);

        Object.DontDestroyOnLoad(listener);
    }
}
