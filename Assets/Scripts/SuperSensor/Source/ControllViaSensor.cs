/*using UnityEngine;
using System.Collections;
using UnityEngine.UI;    
    public class ControllViaSensor : MonoBehaviour
    {
        public Text Status_Text;
        float yval;
        float zval;

    // Use this for initialization
    void Start()
        {
            bool supported = Sensor.Instance.IsSupported(Sensor.Type.ACCELEROMETER);
            Sensor.Instance.Start(Sensor.Type.LINEAR_ACCELERATION, Sensor.Sampling.SENSOR_DELAY_FASTEST);

        }

        // Update is called once per frame
        void Update()
        {
            float speed = 1.0f;

            Sensor.Instance.OnLinearAccelerationChanged += (value) =>
            {
                yval = value.y;
                zval = value.z;
                // Debug.Log("Acceleration: " + value.x + " " + value.y + " " + value.z);

                if (yval > -1.5 && yval < 1.5 || zval > -1.5 && zval < 1.5)
                {
                    Status_Text.text = "Idle";

                }

                if (yval >= 1.5)
                {
                    Status_Text.text = "Left";
                    //transform.Translate(Vector3.left * Time.deltaTime * speed );
                }
                if (yval <= -1.5)
                {
                    Status_Text.text = "Right";
                    //transform.Translate(Vector3.right * Time.deltaTime * speed );
                }
                if (zval >= 1.5)
                {
                    Status_Text.text = "forward";
                   // Vector3 move = transform.forward * m_MoveDir.z * Time.deltaTime * speed;
                }
                if (zval <= -1.5)
                  {
                      Status_Text.text = "backwards";
                      //transform.Translate(Vector3.back * Time.deltaTime * speed  );
                  }

               
                // Status_Text.text = value.z.ToString();
                //  Status_Text.text = ("Acceleration: " + value.x + " " + value.y + " " + value.z);
            };
        }
    }
*/