using UnityEngine;
using System.Collections;

public partial class Sensor {
    
    public const string AccelerometerHandleMethodName = "HandleAccelerometer";
    public const string AmbientTemperatureHandleMethodName = "HandleAmbientTemperature";

    public const string GameRotationVectorHandleMethodName = "HandleGameRotationVector";
    public const string GeomagneticRotationVectorHandleMethodName = "HandleGeomagneticRotationVector";
    public const string GravityHandleMethodName = "HandleGravity";
    public const string GyroscopeHandleMethodName = "HandleGyroscope";
    public const string GyroscopeUncalibratedHandleMethodName = "HandleGyroscopeUncalibrated";

    public const string LightHandleMethodName = "HandleLight";
    public const string LinearAccelerationHandleMethodName = "HandleLinearAcceleration";

    public const string MagneticFieldHandleMethodName = "HandleMagneticField";
    public const string MagneticFieldUncalibratedHandleMethodName = "HandleMagneticFieldUncalibrated";

    public const string PressureHandleMethodName = "HandlePressure";
    public const string ProximityHandleMethodName = "HandleProximity";

    public const string RelativeHumidityHandleMethodName = "HandleRelativeHumidity";
    public const string RotationVectorHandleMethodName = "HandleRotationVector";

    public delegate void AccelerometerHandler(Vector3 acceleration);
    public event AccelerometerHandler OnAccelerationChanged;

    public delegate void AmbientTemperatureHandler(float degrees);
    public event AmbientTemperatureHandler OnAmbientTemperatureChanged;

    public delegate void GameRotationVectorHandler(Vector3 rotation);
    public event GameRotationVectorHandler OnGameRotationVectorChanged;

    public delegate void GeomagneticRotationVectorHandler(Vector3 rotation);
    public event GeomagneticRotationVectorHandler OnGeomagneticRotationVectorChanged;

    public delegate void GravityHandler(Vector3 gravity);
    public event GravityHandler OnGravityChanged;

    public delegate void GyroscopeHandler(Vector3 angularSpeed);
    public event GyroscopeHandler OnGyroscopeChanged;

    public delegate void GyroscopeUncalibratedHandler(float[] angularSpeed);
    public event GyroscopeUncalibratedHandler OnGyroscopeUncalibratedChanged;

    public delegate void LightHandler(float luxUnits);
    public event LightHandler OnLightChanged;

    public delegate void LinearAccelerationHandler(Vector3 acceleration);
    public event LinearAccelerationHandler OnLinearAccelerationChanged;

    public delegate void MagneticFieldHandler(Vector3 microTeslas);
    public event MagneticFieldHandler OnMagneticFieldChanged;

    public delegate void MagneticFieldUncalibratedHandler(float[] microTeslas);
    public event MagneticFieldUncalibratedHandler OnMagneticFieldUncalibratedChanged;

    public delegate void PressureHandler(float millibars);
    public event PressureHandler OnPressureChanged;

    public delegate void ProximityHandler(float centimeters);
    public event ProximityHandler OnProximityChanged;

    public delegate void RelativeHumidityHandler(float percent);
    public event RelativeHumidityHandler OnRelativeHumidityChanged;

    public delegate void RotationVectorHandler(float[] orientation);
    public event RotationVectorHandler OnRotationVectorChanged;

    public void CallAccelerationChanged(Vector3 value) {
        if (OnAccelerationChanged != null) {
            OnAccelerationChanged(value);
        }
    }

    public void CallAmbientTemperatureChanged(float value) {
        if (OnAmbientTemperatureChanged != null) {
            OnAmbientTemperatureChanged(value);
        }
    }

    public void CallGameRotationVectorChanged(Vector3 value) {
        if (OnGameRotationVectorChanged != null) {
            OnGameRotationVectorChanged(value);
        }
    }

    public void CallGeomagneticRotationVectorChanged(Vector3 value) {
        if (OnGeomagneticRotationVectorChanged != null) {
            OnGeomagneticRotationVectorChanged(value);
        }
    }

    public void CallGravityChanged(Vector3 value) {
        if (OnGravityChanged != null) {
            OnGravityChanged(value);
        }
    }

    public void CallGyroscopeChanged(Vector3 value) {
        if (OnGyroscopeChanged != null) {
            OnGyroscopeChanged(value);
        }
    }

    public void CallGyroscopeUncalibratedChanged(float[] value) {
        if (OnGyroscopeUncalibratedChanged != null) {
            OnGyroscopeUncalibratedChanged(value);
        }
    }

    public void CallLightChanged(float value) {
        if (OnLightChanged != null) {
            OnLightChanged(value);
        }
    }

    public void CallLinearAccelerationChanged(Vector3 value) {
        if (OnLinearAccelerationChanged != null) {
            OnLinearAccelerationChanged(value);
        }
    }

    public void CallMagneticFieldChanged(Vector3 value) {
        if (OnMagneticFieldChanged != null) {
            OnMagneticFieldChanged(value);
        }
    }

    public void CallMagneticFieldUncalibratedChanged(float[] value) {
        if (OnMagneticFieldUncalibratedChanged != null) {
            OnMagneticFieldUncalibratedChanged(value);
        }
    }

    public void CallPressureChanged(float value) {
        if (OnPressureChanged != null) {
            OnPressureChanged(value);
        }
    }

    public void CallProximityChanged(float value) {
        if (OnProximityChanged != null) {
            OnProximityChanged(value);
        }
    }

    public void CallRelativeHumidityChanged(float value) {
        if (OnRelativeHumidityChanged != null) {
            OnRelativeHumidityChanged(value);
        }
    }

    public void CallRotationVectorChanged(float[] value) {
        if (OnRotationVectorChanged != null) {
            OnRotationVectorChanged(value);
        }
    }
}
