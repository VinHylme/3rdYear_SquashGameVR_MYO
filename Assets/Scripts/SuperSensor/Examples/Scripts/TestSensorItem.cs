using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TestSensorItem : MonoBehaviour {

    [SerializeField]
    private Dropdown dropdown;

    [SerializeField]
    private Button button;

    [SerializeField]
    private Text buttonText;

    [SerializeField]
    private Text sensorText;

    private Sensor.Type type;

    private Sensor.Sampling selectedSampling;

    private bool isEnabled;

    void Awake() {
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        Sensor.Sampling[] samplings = (Sensor.Sampling[])Enum.GetValues(typeof(Sensor.Sampling));

        foreach (Sensor.Sampling sampling in samplings) {
            options.Add(new Dropdown.OptionData(sampling.ToString()));
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(options);

        dropdown.onValueChanged.AddListener((index) => {
            selectedSampling = (Sensor.Sampling)index;
        });

        dropdown.value = (int)Sensor.Sampling.SENSOR_DELAY_GAME;

        button.onClick.AddListener(Toggle);
    }

    public void Init(Sensor.Type type) {
        this.type = type;

        button.interactable = Sensor.Instance.IsSupported(type);

        if (!button.interactable) {
            buttonText.text = string.Format("{0} is not supported", type);

            SetValue();
        } else {
            string sensorInfo = string.Format("{0} - MaximumRange = {1} - Power = {2} - Resolution = {3} - Vendor = {4} - Version = {5}", type.ToString(),
                Sensor.Instance.GetMaximumRange(type),
                Sensor.Instance.GetPower(type),
                Sensor.Instance.GetResolution(type),
                Sensor.Instance.GetVendor(type),
                Sensor.Instance.GetVersion(type));

            Debug.Log(sensorInfo);

            SetButtonLabel();
            SetValue();

            switch (type) {
                case Sensor.Type.ACCELEROMETER:
                    Sensor.Instance.OnAccelerationChanged += (value) => {
                        SetValue("Acceleration", value);
                    };
                    break;
                case Sensor.Type.AMBIENT_TEMPERATURE:
                    Sensor.Instance.OnAmbientTemperatureChanged += (value) => {
                        SetValue("Celsius degrees", value);
                    };
                    break;
                case Sensor.Type.GAME_ROTATION_VECTOR:
                    Sensor.Instance.OnGameRotationVectorChanged += (value) => {
                        SetValue("Rotation", value);
                    };
                    break;
                case Sensor.Type.GEOMAGNETIC_ROTATION_VECTOR:
                    Sensor.Instance.OnGeomagneticRotationVectorChanged += (value) => {
                        SetValue("Rotation", value);
                    };
                    break;
                case Sensor.Type.GRAVITY:
                    Sensor.Instance.OnGravityChanged += (value) => {
                        SetValue("Gravity", value);
                    };
                    break;
                case Sensor.Type.GYROSCOPE:
                    Sensor.Instance.OnGyroscopeChanged += (value) => {
                        SetValue("Angular Speed", value);
                    };
                    break;
                case Sensor.Type.GYROSCOPE_UNCALIBRATED:
                    Sensor.Instance.OnGyroscopeUncalibratedChanged += (value) => {
                        SetValue("Angular Speed", value);
                    };
                    break;
                case Sensor.Type.LIGHT:
                    Sensor.Instance.OnLightChanged += (value) => {
                        SetValue("Lux Units", value);
                    };
                    break;
                case Sensor.Type.LINEAR_ACCELERATION:
                    Sensor.Instance.OnLinearAccelerationChanged += (value) => {
                        SetValue("Acceleration", value);
                    };
                    break;
                case Sensor.Type.MAGNETIC_FIELD:
                    Sensor.Instance.OnMagneticFieldChanged += (value) => {
                        SetValue("Micro Teslas", value);
                    };
                    break;
                case Sensor.Type.MAGNETIC_FIELD_UNCALIBRATED:
                    Sensor.Instance.OnMagneticFieldUncalibratedChanged += (value) => {
                        SetValue("Micro Teslas", value);
                    };
                    break;
                case Sensor.Type.PRESSURE:
                    Sensor.Instance.OnPressureChanged += (value) => {
                        SetValue("Millibars", value);
                    };
                    break;
                case Sensor.Type.PROXIMITY:
                    Sensor.Instance.OnProximityChanged += (value) => {
                        SetValue("Centimeters", value);
                    };
                    break;
                case Sensor.Type.RELATIVE_HUMIDITY:
                    Sensor.Instance.OnRelativeHumidityChanged += (value) => {
                        SetValue("Percent", value);
                    };
                    break;
                case Sensor.Type.ROTATION_VECTOR:
                    Sensor.Instance.OnRotationVectorChanged += (value) => {
                        SetValue("Orientation", value);
                    };
                    break;
            }
        }
    }

    void Toggle() {
        isEnabled = !isEnabled;

        SetButtonLabel();
        
        if (isEnabled) {
            Sensor.Instance.Start(type, selectedSampling);
        } else {
            Sensor.Instance.Stop(type);

            SetValue();
        }
    }

    void SetButtonLabel() {
        buttonText.text = string.Format("{0} \nis {1}", type.ToString(), isEnabled ? "Enabled" : "Disabled");
    }

    private void SetValue() {
        SetValue("Not initialized", -1);
    }

    private void SetValue(string preffix, float value) {
        sensorText.text = string.Format("{0}: {1}", preffix, value);
    }

    private void SetValue(string preffix, float[] value) {
        int i = 0;

        sensorText.text = string.Format("{0}: {1}", preffix, string.Join("\n", value.Select(v => string.Format("[{0}] Value = {1}", i, value[i++])).ToArray()));
    }

    private void SetValue(string preffix, Vector3 value) {
        sensorText.text = string.Format("{0}: {1}", preffix, string.Format("\nX = {0} \nY = {1} \nZ = {2}", value.x, value.y, value.z));
    }
}
