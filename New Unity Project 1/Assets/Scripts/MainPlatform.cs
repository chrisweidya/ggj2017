using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlatform : MonoBehaviour {
    public float lheight = 0;
    public float rheight = 0;
    public float turnAmt = 0;
    public GameObject waveMaker;
    public Quaternion rotation;
    public Rigidbody wmrb;	// Use this for initialization
	void Start () {
        waveMaker = transform.GetChild(2).gameObject;
        rotation = waveMaker.transform.rotation;
        wmrb = waveMaker.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void turn(float amt)
    {
        wmrb.angularVelocity = -transform.forward * amt;
        StartCoroutine(stopRotating());
    }
    IEnumerator stopRotating()
    {
        yield return new WaitForSeconds(1);
        waveMaker.transform.rotation = rotation;
        wmrb.angularVelocity = Vector3.zero;
    }
}
