using UnityEngine;
using System.Collections;

public class cam_follow : MonoBehaviour
{

    public GameObject player;       


    private Vector3 offset;         //store the offset distance between the player and camera


    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's.
        transform.position = player.transform.position + offset;
    }
}
