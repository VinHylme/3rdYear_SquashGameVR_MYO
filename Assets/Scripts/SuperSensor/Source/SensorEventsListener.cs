using System.Linq;
using UnityEngine;

public class SensorEventsListener : MonoBehaviour {

    // http://developer.android.com/intl/es/guide/topics/sensors/sensors_overview.html

    // http://developer.android.com/intl/es/reference/android/hardware/SensorEvent.html#values

    void HandleAccelerometer(string value) {
        Sensor.Instance.CallAccelerationChanged(ToVector3(value));
    }

    void HandleAmbientTemperature(string value) {
        Sensor.Instance.CallAmbientTemperatureChanged(ToFloat(value));
    }

    void HandleGameRotationVector(string value) {
        Sensor.Instance.CallGameRotationVectorChanged(ToVector3(value));
    }

    void HandleGeomagneticRotationVector(string value) {
        Sensor.Instance.CallGeomagneticRotationVectorChanged(ToVector3(value));
    }

    void HandleGravity(string value) {
        Sensor.Instance.CallGravityChanged(ToVector3(value));
    }

    void HandleGyroscope(string value) {
        Sensor.Instance.CallGyroscopeChanged(ToVector3(value));
    }

    void HandleGyroscopeUncalibrated(string value) {
        Sensor.Instance.CallGyroscopeUncalibratedChanged(ToFloatArray(value));
    }

    void HandleLight(string value) {
        Sensor.Instance.CallLightChanged(ToFloat(value));
    }

    void HandleLinearAcceleration(string value) {
        Sensor.Instance.CallLinearAccelerationChanged(ToVector3(value));
    }

    void HandleMagneticField(string value) {
        Sensor.Instance.CallMagneticFieldChanged(ToVector3(value));
    }

    void HandleMagneticFieldUncalibrated(string value) {
        Sensor.Instance.CallMagneticFieldUncalibratedChanged(ToFloatArray(value));
    }

    void HandlePressure(string value) {
        Sensor.Instance.CallPressureChanged(ToFloat(value));
    }

    void HandleProximity(string value) {
        Sensor.Instance.CallProximityChanged(ToFloat(value));
    }

    void HandleRelativeHumidity(string value) {
        Sensor.Instance.CallRelativeHumidityChanged(ToFloat(value));
    }

    void HandleRotationVector(string value) {
        Sensor.Instance.CallRotationVectorChanged(ToFloatArray(value));
    }

    float ToFloat(string value) {
        string[] values = value.Split(',');

        return float.Parse(values[0]);
    }

    float[] ToFloatArray(string value) {
        string[] values = value.Split(',');

        return values.Select(v => float.Parse(v)).ToArray();
    }

    Vector3 ToVector3(string value) {
        string[] values = value.Split(',');

        return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
    }
}
