using UnityEngine;
using System.Collections;

public class pointAt : MonoBehaviour
{
    /* 
    Klaudijus Miseckas 
    SID:1334116 
    */

    [Header("Settings")]
    public Transform target;
    public Transform palmLoc;

    private bool lookAt = true;
    private float defaultY = 0;

    private Vector3 newRot;

    void Start()
    {

       // PoseCheck.onUseLightning += LookAtTarget;
     //   PoseCheck.onStopLightning += StopLookAtTarget;

        defaultY = palmLoc.localEulerAngles.y;
        newRot.x = palmLoc.localEulerAngles.x;
        newRot.z = palmLoc.localEulerAngles.z;
    }

    void Update()
    {
        if (lookAt)
        {
            palmLoc.LookAt(target);
            newRot.y = palmLoc.localEulerAngles.y + 60;
            palmLoc.localEulerAngles = newRot;
        }
    }

    void LookAtTarget()
    {
        lookAt = true;
    }

    void StopLookAtTarget()
    {
        lookAt = false;

        newRot.y = defaultY;
        palmLoc.localEulerAngles = newRot;

    }
}
