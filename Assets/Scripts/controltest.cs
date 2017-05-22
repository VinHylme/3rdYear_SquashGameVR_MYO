using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class controltest : MonoBehaviour {

public Animator anime;

	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

	}

   void disableBat()
    {
        anime.SetBool("isSwing1", false);
        anime.SetBool("isSwing2", false);
    }

}
