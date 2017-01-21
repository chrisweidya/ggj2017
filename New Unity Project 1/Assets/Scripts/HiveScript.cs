using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveScript : MonoBehaviour {
    private Rigidbody rb;
    private bool isJumping = false;
    public bool grounded = true;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        EventManager.onStartHiveJump += jump;
    }
	
	// Update is called once per frame
	void Update () {
    }

    void jump()
    {
        rb.AddForce(new Vector3(0, 0.10f * Time.deltaTime, 0), ForceMode.Impulse);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }
    void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}
