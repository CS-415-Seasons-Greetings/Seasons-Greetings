using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    public AudioClip JumpSound;
    //public AudioClip SnowLanding;
    public AudioSource JumpSource;
   // public AudioSource SnowLandingSource;

	// Use this for initialization
	void Start () {
        JumpSource.clip = JumpSound;
      //  SnowLandingSource.clip = SnowLanding;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            JumpSource.Play();
		
	}
}
