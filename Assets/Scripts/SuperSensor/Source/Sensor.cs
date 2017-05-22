public partial class Sensor : Plugin<Sensor> {

    public enum Type {
        ACCELEROMETER = 1,
        AMBIENT_TEMPERATURE = 13,
        GAME_ROTATION_VECTOR = 15,
        GEOMAGNETIC_ROTATION_VECTOR = 20,
        GRAVITY = 9,
        GYROSCOPE = 4,
        GYROSCOPE_UNCALIBRATED = 16,
        LIGHT = 5,
        LINEAR_ACCELERATION = 10,
        MAGNETIC_FIELD = 2,
        MAGNETIC_FIELD_UNCALIBRATED = 14,
        PRESSURE = 6,
        PROXIMITY = 8,
        RELATIVE_HUMIDITY = 12,
        ROTATION_VECTOR = 11
    }

    public enum Sampling {
        SENSOR_DELAY_FASTEST = 0,
        SENSOR_DELAY_GAME = 1,
        SENSOR_DELAY_NORMAL = 3,
        SENSOR_DELAY_UI = 2
    }

#if UNITY_ANDROID
    private AndroidSensor plugin;
#endif

    public Sensor() : base(typeof(SensorEventsListener)) {
#if UNITY_ANDROID
        plugin = new AndroidSensor(ListenerName);
#endif
    }

    public void Start(Type type, Sampling sampling) {
#if UNITY_ANDROID
        plugin.Start(type, sampling);
#else
		throw new PluginException(this);
#endif
    }

    public bool IsRunning(Type type) {
#if UNITY_ANDROID
        return plugin.IsRunning(type);
#else
		throw new PluginException(this);
#endif
    }

    public void Stop(Type type) {
#if UNITY_ANDROID
        plugin.Stop(type);
#else
		throw new PluginException(this);
#endif
    }

    public bool IsSupported(Type type) {
#if UNITY_ANDROID
        foreach (Type availableType in plugin.GetDeviceSensors()) {
            if (availableType == type) {
                return true;
            }
        }

        return false;
#else
		throw new PluginException(this);
#endif
    }

    public Type[] GetDeviceSensors() {
#if UNITY_ANDROID
        return plugin.GetDeviceSensors();
#else
		throw new PluginException(this);
#endif
    }

    public float GetMaximumRange(Type type) {
#if UNITY_ANDROID
        return plugin.GetMaximumRange(type);
#else
		throw new PluginException(this);
#endif
    }

    public float GetPower(Type type) {
#if UNITY_ANDROID
        return plugin.GetPower(type);
#else
		throw new PluginException(this);
#endif
    }

    public float GetResolution(Type type) {
#if UNITY_ANDROID
        return plugin.GetResolution(type);
#else
		throw new PluginException(this);
#endif
    }

    public string GetVendor(Type type) {
#if UNITY_ANDROID
        return plugin.GetVendor(type);
#else
		throw new PluginException(this);
#endif
    }

    public int GetVersion(Type type) {
#if UNITY_ANDROID
        return plugin.GetVersion(type);
#else
		throw new PluginException(this);
#endif
    }
}