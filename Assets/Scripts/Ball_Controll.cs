using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball_Controll : MonoBehaviour
{

    public Rigidbody myRigidbody;
    Vector3 oldVel;
    static short turn;
    Random rnd = new Random();
    public Text player_score;
    public Text ai_score;
    int pscore=0, ascore =0;
    public GameObject player;
    void Start()
    {
        
        myRigidbody = GetComponent<Rigidbody>();
        turn = 2;
        myRigidbody.GetComponent<Renderer>().material.color = Color.cyan;
        myRigidbody.AddForce(Random.Range(-10.0f,5.0f), 0, 600);
    }

    void FixedUpdate()
    {
       // oldVel = myRigidbody.velocity;
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "walls") { 
            if(turn == 1)
            {
                myRigidbody.AddForce(Random.Range(-50.0f, 100.0f), 0, -player.transform.position.z + 600);
                ++turn;
                myRigidbody.GetComponent<Renderer>().material.color = Color.cyan;
            }
            else if(turn == 3)
            {
                ++turn;
            }
        }else if(other.tag=="bat"){
                Debug.Log("bat");
                myRigidbody.GetComponent<Renderer>().material.color = Color.cyan;
                myRigidbody.AddForce(Random.Range(-50.0f, 100.0f), 0, 800);
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag=="WallCube") {
            Debug.Log("game");
            if (turn % 2 == 0)
            {
                
                Debug.Log("play");
                pscore++;
                player_score.text = "Player Score: " + pscore + " ";
                myRigidbody.GetComponent<Renderer>().material.color = Color.red;
                myRigidbody.AddForce(Random.Range(-50.0f, 100.0f), 0, 695);
            }
            else
            {
                turn = 1;
                Debug.Log(ascore);
                ascore++;
                ai_score.text = "AI Score: " + ascore + "";
                myRigidbody.GetComponent<Renderer>().material.color = Color.cyan;
                myRigidbody.AddForce(Random.Range(-50.0f + -player.transform.position.x, 100.0f), 0, 695);
            }
          
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "walls")
        {
            if (turn == 4)
            {
                myRigidbody.GetComponent<Renderer>().material.color = Color.red;
            }
            if (turn % 2 == 0)
            {
                ++turn;
                turn %= 4;
            }
        }
        else if (other.tag == "bat")
        {
            myRigidbody.GetComponent<Renderer>().material.color = Color.cyan;
            myRigidbody.AddForce(Random.Range(-50.0f, 100.0f), 0, -player.transform.position.z);
        }
    
    }

}