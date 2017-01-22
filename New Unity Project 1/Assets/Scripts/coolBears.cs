using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coolBears : MonoBehaviour
{
    private Animator anim; 
	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        string coolPose = string.Format("Cool{0}", Random.Range(1, 3));
        anim.Play(coolPose);
    }
	
	// Update is called once per frame
	void Update ()
    {
       
            
    }
}
