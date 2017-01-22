using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMaker : MonoBehaviour {
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
 //       float v = 150 * Time.deltaTime;
 //       rb.AddTorque(-transform.forward * v, ForceMode.Impulse);

    }
	
	// Update is called once per frame
	void Update () {
	}
}
