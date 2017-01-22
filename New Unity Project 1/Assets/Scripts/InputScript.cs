using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour {
    // Use this for initialization
    private float chargeR = 0.5f;
    private float chargeL = 0.5f;
    public float chargeSpeed = 0.01f;

    
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {/*
        if (Input.GetButton("Fire1"))
        {
            chargeR += Time.deltaTime * chargeSpeed;
            chargeR = Mathf.Min(1f, chargeR);
        }
        */
        if (Input.GetButtonDown("Fire1"))
        {
            EventManager.fireOnStartJumpR(1);
            //    chargeR = 0.5f;
        }
        if (Input.GetButtonDown("PulseRight"))
        {
            //bearR.GetComponent<Animator>().Play("bear_Jump");
            EventManager.fireOnStartWave(1);
            //    chargeR = 0.5f;
        }
        /*
        if (Input.GetButton("Fire2"))
        {
            chargeL += Time.deltaTime * chargeSpeed;
            chargeL = Mathf.Min(1f, chargeL);
        }*/
        if (Input.GetButtonDown("Fire2"))
        {
            EventManager.fireOnStartJumpL(1);
            //   chargeL = 0.5f;
        }
        if (Input.GetButtonDown("PulseLeft"))
        {
            EventManager.fireOnStartWave(-1);
            //    chargeR = 0.5f;
        }
    }
    void FixedUpdate()
    {
        
    }
}
