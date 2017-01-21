using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Bear : MonoBehaviour
{
    [SerializeField]
    private bool isGrounded;
    private Animator anim;
    private float health = 100; 
    [SerializeField]
    private bool player1;
    [SerializeField]
    private float honeyAmt = 0; 

    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text honeyText; 


    void OnCollisionEnter(Collision other)
    {
        /*if (other.gameObject.tag == "ground")
        {
            isGrounded = true;
        }*/

        if (other.gameObject.tag == "Hive")
        {
            health -= other.gameObject.GetComponent<HiveScript>().totalBees;
            healthText.text = "Health: " + health;
            honeyAmt += 1;
            honeyText.text = "Honey: " + honeyAmt; 
            other.gameObject.GetComponent<HiveScript>().respawn(); 
        }
    }
    void OnCollisionExit(Collision other)
    {
       /* if (other.gameObject.tag == "ground")
        {
            isGrounded = false;
        }*/
    }

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>(); 
	}

    // Update is called once per frame
    void Update()
    {
        if (player1 == true && Input.GetButtonDown("P1Jump") && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger("jump");
        }

        if (player1 == false && Input.GetButtonDown("P2Jump") && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger("jump");
        }

    }
}
