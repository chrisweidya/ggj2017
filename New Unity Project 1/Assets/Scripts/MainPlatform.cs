using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlatform : MonoBehaviour {
    public float lheight = 0;
    public float rheight = 0;
    public GameObject waveMaker;
	// Use this for initialization
	void Start () {
        waveMaker = transform.GetChild(2).gameObject;
        print(waveMaker);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
