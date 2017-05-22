#if !UNITY_EDITOR && UNITY_ANDROID
using UnityEngine;
#endif

public class AndroidSensor : AndroidPlugin {

    protected override string JavaClassName {
        get { return "Sensor"; }
    }

    public AndroidSensor(string listenerName) : base(listenerName) {
        Call("init");

        SetAccelerometerCallback(Sensor.AccelerometerHandleMethodName);
        SetAmbientTemperatureCallback(Sensor.AmbientTemperatureHandleMethodName);

        SetGameRotationVectorCallback(Sensor.GameRotationVectorHandleMethodName);
        SetGeomagneticRotationVectorCallback(Sensor.GeomagneticRotationVectorHandleMethodName);
        SetGravityCallback(Sensor.GravityHandleMethodName);
        SetGyroscopeCallback(Sensor.GyroscopeHandleMethodName);
        SetGyroscopeUncalibratedCallback(Sensor.GyroscopeUncalibratedHandleMethodName);

        SetLightCallback(Sensor.LightHandleMethodName);
        SetLinearAccelerationCallback(Sensor.LinearAccelerationHandleMethodName);

        SetMagneticFieldCallback(Sensor.MagneticFieldHandleMethodName);
        SetMagneticFieldUncalibratedCallback(Sensor.MagneticFieldUncalibratedHandleMethodName);

        SetPressureCallback(Sensor.PressureHandleMethodName);
        SetProximityCallback(Sensor.ProximityHandleMethodName);

        SetRelativeHumidityCallback(Sensor.RelativeHumidityHandleMethodName);
        SetRotationVectorCallback(Sensor.RotationVectorHandleMethodName);
    }

    private void SetAccelerometerCallback(string callbackName) {
        Call("setAccelerometerCallback", callbackName);
    }

    private void SetAmbientTemperatureCallback(string callbackName) {
        Call("setAmbientTemperatureCallback", callbackName);
    }

    private void SetGameRotationVectorCallback(string callbackName) {
        Call("setGameRotationVectorCallback", callbackName);
    }

    private void SetGeomagneticRotationVectorCallback(string callbackName) {
        Call("setGeomagneticRotationVectorCallback", callbackName);
    }

    private void SetGravityCallback(string callbackName) {
        Call("setGravityCallback", callbackName);
    }

    private void SetGyroscopeCallback(string callbackName) {
        Call("setGyroscopeCallback", callbackName);
    }

    private void SetGyroscopeUncalibratedCallback(string callbackName) {
        Call("setGyroscopeUncalibratedCallback", callbackName);
    }

    private void SetLightCallback(string callbackName) {
        Call("setLightCallback", callbackName);
    }

    private void SetLinearAccelerationCallback(string callbackName) {
        Call("setLinearAccelerationCallback", callbackName);
    }

    private void SetMagneticFieldCallback(string callbackName) {
        Call("setMagneticFieldCallback", callbackName);
    }

    private void SetMagneticFieldUncalibratedCallback(string callbackName) {
        Call("setMagneticFieldUncalibratedCallback", callbackName);
    }

    private void SetPressureCallback(string callbackName) {
        Call("setPressureCallback", callbackName);
    }

    private void SetProximityCallback(string callbackName) {
        Call("setProximityCallback", callbackName);
    }

    private void SetRelativeHumidityCallback(string callbackName) {
        Call("setRelativeHumidityCallback", callbackName);
    }

    private void SetRotationVectorCallback(string callbackName) {
        Call("setRotationVectorCallback", callbackName);
    }

    public void Start(Sensor.Type type, Sensor.Sampling sampling) {
        Call("start", (int)type, (int)sampling);
    }

    public bool IsRunning(Sensor.Type type) {
        return Call<bool>("isRunning", (int)type);
    }

    public void Stop(Sensor.Type type) {
        Call("stop", (int)type);
    }

    public Sensor.Type[] GetDeviceSensors() {
        int[] sensors = Call<int[]>("getDeviceSensors");
        Sensor.Type[] result;

        result = new Sensor.Type[sensors.Length];

        int i = 0;

        foreach (int sensor in sensors) {
            result[i++] = (Sensor.Type)sensor;
        }

        return result;
    }

    public float GetMaximumRange(Sensor.Type type) {
        return Call<float>("getMaximumRange", (int)type);
    }

    public float GetPower(Sensor.Type type) {
        return Call<float>("getPower", (int)type);
    }

    public float GetResolution(Sensor.Type type) {
        return Call<float>("getResolution", (int)type);
    }

    public string GetVendor(Sensor.Type type) {
        return Call<string>("getVendor", (int)type);
    }

    public int GetVersion(Sensor.Type type) {
        return Call<int>("getVersion", (int)type);
    }
}
