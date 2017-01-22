using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hivesound : MonoBehaviour {

    private AudioSource aud;


	// Use this for initialization
	void Start () {

        aud = GetComponent<AudioSource>();
	}
	
	void OnCollisionEnter( Collision other)
    {
        aud.Play(0);
    }
}
