using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveScript : MonoBehaviour {
    private Rigidbody rb;
    private bool isJumping = false;
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
        if (!isJumping) {
            rb.AddForce(new Vector3(0, 0.0005f * Time.deltaTime, 0), ForceMode.Impulse);
            isJumping = true;
        }
    }
    
    void onCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            isJumping = false;
        }
    }
}
