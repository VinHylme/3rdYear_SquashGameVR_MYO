using UnityEngine;
using System.Collections;
using MyoUnity;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.VR;

public class ArmController : MonoBehaviour
{
    private Quaternion myoRotation;
    private MyoPose myoPose = MyoPose.UNKNOWN;

    [Header("Arm Objects")]
    public GameObject foreArmBone;
    public GameObject rightArm;
    public GameObject leftArm;

    [Header("Arm Rotation")]
    public float rollSmooth = 7;
    public float armXYSmooth = 7;

    [Header("Arm Settings")]
    public float maxNegativeArm = 90;
    public float maxPositiveArm = 90;
    public float maxNegativeMyo = 0;
    public float maxPositiveMyo = 35;

    private float minAngle = 0;
    private float maxAngle = 0;
    private float range = 0;
    private float armRange = 0;
    private float myoMultiplier = 0;

    private float currentRot = 0;

    private float normalPalmRota = 0;
    private float caliMyoRota = 0;
    private float currentMyoRota = 0;
    private float poseCheck = 0f;
    public float newPose = 1f;


    [Header("Arm Up/Down (XY) Rotation Settings")]
    public float maxY = 0;
    public float minY = 0;
    public float maxX = 0;
    public float minX = 0;

    private float maxYAngle = 0;
    private float minYAngle = 0;
    private float maxXAngle = 0;
    private float minXAngle = 0;

    private float normalBoneX = 0;
    private float normalBoneY = 0;

    private float caliX = 0;
    private float caliY = 0;

    private float currentX = 0;
    private float currentY = 0;

    private float currentArmHolderX = 0;
    private float currentArmHolderY = 0;
    private float normalArmHolderX = 0;
    private float normalArmHolderY = 0;
    private float normalArmHolderZ = 0;

    public delegate void PoseAction();
    public static event PoseAction onUseGrip;
    public static event PoseAction onStopGrip;

    private bool isGripped = false;
    private bool isCalibrated = false;
    private bool isGripOn = false;
    bool isHitOn = false;
    bool isSwingon = false;
    bool isBatOn = false;

 
    public GameObject playerObj;
    public GameObject cam;

    [Header("Grip Settings")]
    public GameObject starthand;
    public GameObject pointer;
    public float gripRange;
    public float gripRadius;
    public LayerMask checkGrip;

    
    private List<GameObject> object_hold = new List<GameObject>();
    private GameObject centre;


    [Header ("Animators and Rotational Movements")]
    Animator animeR,animeL;
    MyoPose lastmyopose;
    Vector3 newArmRotationLocal = Vector3.zero;
    Vector3 newArmHolderRotationLocal = Vector3.zero;

    [Header ("UI")]
    public Button button_sync;
    public Button button_unsync;

    void Start()
    {
       // VRSettings.enabled = false;
      //  Button btn = button_sync.GetComponent<Button>();
       // btn.onClick.AddListener(SyncFunction);
       
        animeR = rightArm.GetComponent<Animator>();
        animeL = leftArm.GetComponent<Animator>();

        MyoManager.Initialize();
        MyoManager.PoseEvent += OnPoseEvent;

        centre = new GameObject();
        centre.name = "Grip Centre";

        onUseGrip += Use_Grip;
        onStopGrip += StopUsing_Grip;

        centre.transform.parent = pointer.transform;
    }
    void SyncFunction()
    {
        Debug.Log("synced");
        MyoManager.AttachToAdjacent();
    }

    void OnPoseEvent(MyoPose pose)
    {
        myoPose = pose; 
    }

    void Update()
    {
        if (MyoManager.GetIsAttached())
        {
            //previously implemented a button to sync
           // button_sync.GetComponent<Button>().interactable = false;
           // button_sync.gameObject.SetActive(false);


            //get the current pose
            getPose();
            //get the action to play animation
            getAction();
            //update the arm rotational values
            UpdateRota();
            UpdateRotationValues();
            SetRotationForArms();
            
            //isCalibrated = true;

            //VRSettings.LoadDeviceByName("daydream");
            //VRSettings.enabled = true;
            // Calibrate();

        }else if (!MyoManager.GetIsAttached()){
            MyoManager.AttachToAdjacent();
        }

        //if (isCalibrated)
       // {
         //  Debug.Log("iscali");
     //   }
    }
    void FixedUpdate()
    {
        if (isGripped)
        {
            foreach (GameObject obj in object_hold)
            {
                //set ball new position = to the palm location
                obj.transform.position = centre.transform.position;
                obj.GetComponent<Rigidbody>().transform.position = starthand.transform.position;
            }
        }
    }
    void Use_Grip()
    {
        if (!isGripped)
        {
            RaycastHit hit_col;

            if (Physics.SphereCast(starthand.transform.position, gripRadius, GetDir(), out hit_col, gripRange, checkGrip))
            {

                centre.transform.position = hit_col.point;

                Collider[] colliders = Physics.OverlapSphere(hit_col.point, 0.6f, checkGrip);

                foreach (Collider col in colliders)
                {
                    GameObject cl_obj = col.gameObject;
                    object_hold.Add(cl_obj);

                    cl_obj.GetComponent<Rigidbody>().useGravity = false;
                }

                if (object_hold.Count != 0)
                {
                    isGripped = true;
                }
            }
        }
    }

    void StopUsing_Grip()
    {
        isGripped = false;
        float force = 5f;
        foreach (GameObject obj in object_hold)
        {
            obj.GetComponent<Rigidbody>().useGravity = true;
            obj.GetComponent<Rigidbody>().AddForce(GetDir(centre.transform.position, obj.transform.position) * force);
        }

        object_hold.Clear();
    }

    Vector3 GetDir()
    {
        Vector3 dir = pointer.transform.position - starthand.transform.position;
        return dir;
    }

    Vector3 GetDir(Vector3 s, Vector3 t)
    {
        Vector3 dir = s - t;
        return dir;
    }
    
    

    float GetMaxRangeMyo()
    {
        float roll = 0;

        if (MyoManager.GetQuaternion().eulerAngles.z <= 360 && MyoManager.GetQuaternion().eulerAngles.z >= 180)
        {
            if (MyoManager.GetQuaternion().eulerAngles.z >= maxAngle)
            {
                roll = range;
            }
            else if (MyoManager.GetQuaternion().eulerAngles.z <= minAngle)
            {
                roll = 0;
            }
            else
            {
                roll = MyoManager.GetQuaternion().eulerAngles.z - minAngle;
            }
        }

        return roll;
    }
    void UpdateRota()
    {
        currentRot = GetMaxRangeMyo();

        //Roll the arm based on currentMyoRoll X rollmutliplier
        currentMyoRota = (normalPalmRota + 90) - (currentRot * myoMultiplier);

        //Set new arm roll (With added smoothing)
        float tempFloat = newArmRotationLocal.x;
        newArmRotationLocal.x = Mathf.Lerp(tempFloat, currentMyoRota, Time.deltaTime * rollSmooth);
    }

    void UpdateRotationValues()
    {
        UpdateCurrentMyo();

        //Set new arm x rotation
        newArmHolderRotationLocal.x = Mathf.Lerp(newArmHolderRotationLocal.x, currentX + currentArmHolderX, Time.deltaTime * armXYSmooth);

        newArmHolderRotationLocal.y = Mathf.Lerp(newArmHolderRotationLocal.y, currentY + currentArmHolderY, Time.deltaTime * armXYSmooth);

    }
    void SetRotationForArms()
    {
        foreArmBone.transform.localEulerAngles = newArmRotationLocal;
        rightArm.transform.localEulerAngles = newArmHolderRotationLocal;
        leftArm.transform.localEulerAngles = newArmHolderRotationLocal;
    }
    void UpdateCurrentMyo()
    {
        Vector3 current_MyoRotations = MyoManager.GetQuaternion().eulerAngles;      //Convert Myo rotational value to vector 3 

        if (current_MyoRotations.x <= 360 && current_MyoRotations.x >= 270)
        {
            currentX = current_MyoRotations.x - 360;
            currentX -= caliX;
        }
        else
        {
            currentX = current_MyoRotations.x - caliX;
        }

        if (currentX <= -maxX)
        {
            currentX = -maxX;
        }
        else if (currentX >= minX)
        {
            currentX = minX;
        }

        if (current_MyoRotations.y <= 360 && current_MyoRotations.y >= 270)
        {
            currentY = current_MyoRotations.y - 360;
            currentY -= caliY;
        }
        else
        {
            currentY = current_MyoRotations.y - caliY;
        }

        if (currentY >= maxY)
        {
            currentY = maxY;
        }
        else if (currentY <= -minY)
        {
            currentY = -minY;
        }
    }
    void getPose()
    {
        if (Time.time > poseCheck)
        {
            lastmyopose = myoPose;
            poseCheck = Time.time + newPose;
        }
    }
    void getAction()
    {

        switch (myoPose)
        {
            case MyoPose.WAVE_OUT:
                isHitOn = true;
                animeR.SetBool("isSwing2", true);
                
                StopSwing();
                break;
            case MyoPose.DOUBLE_TAB:
                isSwingon = true;
                animeR.SetBool("isSwing1", true);
                StopHit();
                break;
            case MyoPose.WAVE_IN:
                stopBat();
                break;
            case MyoPose.FIST:
                onUseGrip ();
                isGripOn = true;
                isBatOn = true;
                animeR.SetBool("isBatOn", true);
                animeL.SetBool("isBatOn", true);
                // bat.GetComponent<Rigidbody>().isKinematic = true;
                StopHit();
                StopSwing();
                break;
            case MyoPose.REST:
            case MyoPose.UNKNOWN:
                Invoke("StopHit", 2);
                Invoke("StopSwing", 2);
                //StopHit();
                //  stopSwing();
                break;
        }
    }


    void stopBat()
    {
        if (isBatOn)
        {
            isBatOn = false;
            animeR.SetBool("isBatOn", false);
            animeL.SetBool("isBatOn", false);
            onStopGrip ();
            isGripOn = false;
            
        }
    }

    void StopSwing()
    {
        if (isSwingon)
        {
            isSwingon = false;
            animeR.SetBool("isSwing1", false);
        }
    }
    void StopHit()
    {
        if (isHitOn)
        {
            isHitOn = false;
            animeR.SetBool("isSwing2", false);
        }
    }
  
}