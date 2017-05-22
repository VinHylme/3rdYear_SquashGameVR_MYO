using UnityEngine;
using System.Collections;
using MyoUnity;

public class MyoPluginDemo : MonoBehaviour 
{
	//public Transform objectToRotate;
	
	private Quaternion myoRotation;
	private MyoPose myoPose = MyoPose.UNKNOWN;
    [Header("Arm Objects")]
    public GameObject foreArmBone;
    public GameObject arm05;

    [Header("Arm Rotation Smoothing")]
    public float rollSmoothing = 2;
    public float armXYSmoothing = 2;

    [Header("Arm Roll Settings")]
    public float maxNegArmRoll = 90;
    public float maxPosArmRoll = 90;
    public float maxNegMyoRoll = 0;
    public float maxPosMyoRoll = 35;

    private float myoMinRollAngle = 0;
    private float myoMaxRollAngle = 0;
    private float myoRollRange = 0;
    private float armRollRange = 0;
    private float rollMultiplier = 0;

    private float currentMyoRoll = 0;

    private float normalBoneRoll = 0;
    private float myoCalibrationRoll = 0;
    private float currentArmRoll = 0;
    private float nextposecheck = 0f;
    public float checkNewPoserate = 1f;
    [Header("Arm Up/Down (XY) Rotation Settings")]
    public float maxYRot = 0;
    public float minYRot = 0;
    public float maxXRot = 0;
    public float minXRot = 0;

    private float maxMyoYAngle = 0;
    private float minMyoYAngle = 0;
    private float maxMyoXAngle = 0;
    private float minMyoXAngle = 0;

    private float normalBoneX = 0;
    private float normalBoneY = 0;

    private float myoCalibrationX = 0;
    private float myoCalibrationY = 0;

    private float currentMyoX = 0;
    private float currentMyoY = 0;

    private float currentArmHolderX = 0;
    private float currentArmHolderY = 0;
    private float normalArmHolderX = 0;
    private float normalArmHolderY = 0;
    private float normalArmHolderZ = 0;
  
    bool isHitOn = false;
    bool isSwingon = false;
    bool isBatOn = false;
    Animator anime;
    MyoPose lastmyopose;
    Vector3 newArmRotationLocal = Vector3.zero;
    Vector3 newArmHolderRotationLocal = Vector3.zero;
    void Start () 
	{
        anime = arm05.GetComponent<Animator>();
        MyoManager.Initialize ();
        MyoManager.PoseEvent += OnPoseEvent;
	}

	void OnPoseEvent( MyoPose pose )
	{
		myoPose = pose;
	}
	
	void Update()
	{
		if (MyoManager.GetIsAttached()) {
            //myoRotation = MyoManager.GetQuaternion ();
            //objectToRotate.rotation = myoRotation;

                getCurrentPose();
                getPower();

                UpdateRoll();
                UpdateXYRotation();
                SetArmRotation();
         
        }
	}
    float GetCurrentMyoRollWithinMaxRange()
    {
        float roll = 0;
        
        if (MyoManager.GetQuaternion().eulerAngles.z <= 360 && MyoManager.GetQuaternion().eulerAngles.z >= 180)
        {
            if (MyoManager.GetQuaternion().eulerAngles.z >= myoMaxRollAngle)
            {
                roll = myoRollRange;
            }
            else if (MyoManager.GetQuaternion().eulerAngles.z <= myoMinRollAngle)
            {
                roll = 0;
            }
            else
            {
                roll = MyoManager.GetQuaternion().eulerAngles.z - myoMinRollAngle;
            }
        }

        return roll;
    }
    void UpdateRoll()
    {
        currentMyoRoll = GetCurrentMyoRollWithinMaxRange();

        //Roll the arm based on currentMyoRoll X rollmutliplier
        currentArmRoll = (normalBoneRoll + 90) - (currentMyoRoll * rollMultiplier);

        //Set new arm roll (With added smoothing)
        float tempFloat = newArmRotationLocal.x;
        newArmRotationLocal.x = Mathf.Lerp(tempFloat, currentArmRoll, Time.deltaTime * rollSmoothing);
    }

    void UpdateXYRotation()
    {
        UpdateCurrentMyoXYRotationsWithinRange();

        //Set new arm x rotation
        newArmHolderRotationLocal.x = Mathf.Lerp(newArmHolderRotationLocal.x, currentMyoX + currentArmHolderX, Time.deltaTime * armXYSmoothing);

        newArmHolderRotationLocal.y = Mathf.Lerp(newArmHolderRotationLocal.y, currentMyoY + currentArmHolderY, Time.deltaTime * armXYSmoothing);

    }
    void SetArmRotation()
    {
        foreArmBone.transform.localEulerAngles = newArmRotationLocal;
        arm05.transform.localEulerAngles = newArmHolderRotationLocal;
    }
    void UpdateCurrentMyoXYRotationsWithinRange()
    {
        Vector3 currentMyoRotations = MyoManager.GetQuaternion().eulerAngles;

        if (currentMyoRotations.x <= 360 && currentMyoRotations.x >= 270)
        {
            currentMyoX = currentMyoRotations.x - 360;
            currentMyoX -= myoCalibrationX;
        }
        else
        {
            currentMyoX = currentMyoRotations.x - myoCalibrationX;
        }

        if (currentMyoX <= -maxXRot)
        {
            currentMyoX = -maxXRot;
            //Debug.Log ("MaxAngle Reached");
        }
        else if (currentMyoX >= minXRot)
        {
            currentMyoX = minXRot;
            //Debug.Log ("MinAngle Reached");
        }



        //currentMyoY = currentMyoRotations.y - myoCalibrationY;

        if (currentMyoRotations.y <= 360 && currentMyoRotations.y >= 270)
        {
            currentMyoY = currentMyoRotations.y - 360;
            currentMyoY -= myoCalibrationY;
        }
        else
        {
            currentMyoY = currentMyoRotations.y - myoCalibrationY;
        }

        if (currentMyoY >= maxYRot)
        {
            currentMyoY = maxYRot;
        }
        else if (currentMyoY <= -minYRot)
        {
            currentMyoY = -minYRot;
        }
    }
    void getCurrentPose()
    {
        if (Time.time > nextposecheck)
        {
            lastmyopose = myoPose;
            nextposecheck = Time.time + checkNewPoserate;
        }
    }
    void getPower()
    {

        switch (myoPose)
        {
            case MyoPose.WAVE_IN:
                isHitOn = true;
                anime.SetBool("isSwing2", true);
                StopSwing();
                break;
            case MyoPose.WAVE_OUT:
                isSwingon = true;
                anime.SetBool("isSwing1", true);
                StopHit();
                break;
            case MyoPose.FINGERS_SPREAD:
                stopBat();
                break;
            case MyoPose.FIST:
                isBatOn = true;
                anime.SetBool("isBatOn", true);
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
          //  bat.GetComponent<Rigidbody>().isKinematic = false;
            isBatOn = false;
            anime.SetBool("isBatOn", false);
        }
    }

    void StopSwing()
    {
        if (isSwingon)
        {
            isSwingon = false;
            anime.SetBool("isSwing1", false);
        }
    }
    void StopHit()
    {
        if (isHitOn)
        {
            isHitOn = false;
            anime.SetBool("isSwing2", false);
        }
    }

        void OnGUI()
	{
		GUI.BeginGroup( new Rect( 100, 100, 300, 500 ) );
		
		if (GUILayout.Button ( "Attach to Adjacent" , GUILayout.MinWidth(300), GUILayout.MinHeight(50) ) )
		{
            MyoManager.AttachToAdjacent();
           
		}
		
		if (GUILayout.Button ( "Vibrate Short" , GUILayout.MinWidth(300), GUILayout.MinHeight(50) ) )
		{
            MyoManager.VibrateForLength( MyoVibrateLength.SHORT );
		}
		
		if (GUILayout.Button ( "Vibrate Medium" , GUILayout.MinWidth(300), GUILayout.MinHeight(50) ) )
		{
            MyoManager.VibrateForLength( MyoVibrateLength.MEDIUM );
		}
		
		if (GUILayout.Button ( "Vibrate Long" , GUILayout.MinWidth(300), GUILayout.MinHeight(50) ) )
		{
            MyoManager.VibrateForLength( MyoVibrateLength.LONG );
		}

		/*if (!myo[0].GetIsInitialized()) {
			if (GUILayout.Button ("Initialize MyoPlugin", GUILayout.MinWidth (300), GUILayout.MinHeight (50))) {
                myo[0].Initialize ();
			}
		} else {
			if (GUILayout.Button ("Uninitialize MyoPlugin", GUILayout.MinWidth (300), GUILayout.MinHeight (50))) {
                myo[0].Uninitialize ();
			}
		}*/
		
	/*	GUILayout.Label ( "Myo Quaternion: " + myoRotation.ToString(), GUILayout.MinWidth(300), GUILayout.MinHeight(30) );
		
		GUILayout.Label ( "Myo Pose: " + myoPose.ToString(), GUILayout.MinWidth(300), GUILayout.MinHeight(30) );

		GUILayout.Label ( "Initialized: " + MyoManager.GetIsInitialized(), GUILayout.MinWidth(300), GUILayout.MinHeight(30) );

		GUILayout.Label ( "Attached: " + MyoManager.GetIsAttached(), GUILayout.MinWidth(300), GUILayout.MinHeight(30) );
		
		GUI.EndGroup();
	*/
    }
}