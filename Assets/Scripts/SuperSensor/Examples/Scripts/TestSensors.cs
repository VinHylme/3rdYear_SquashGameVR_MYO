using System;
using UnityEngine;

public class TestSensors : MonoBehaviour {

    [SerializeField]
    private Transform container;

    [SerializeField]
    private GameObject prefab;
    
    void Awake() {
        foreach(Sensor.Type type in Enum.GetValues(typeof(Sensor.Type))) {
            TestSensorItem sensorItem = Instantiate(prefab).GetComponent<TestSensorItem>();
            sensorItem.transform.SetParent(container);
            sensorItem.transform.localPosition = Vector3.zero;
            sensorItem.transform.localRotation = Quaternion.identity;
            sensorItem.transform.localScale = Vector3.one;

            sensorItem.Init(type);
        }
    }

    void OnDestroy() {
        foreach (Sensor.Type type in Sensor.Instance.GetDeviceSensors()) {
            if (Sensor.Instance.IsRunning(type)) {
                Sensor.Instance.Stop(type);
            }
        }
    }
}
