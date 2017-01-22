using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bear : MonoBehaviour
{
    [SerializeField]
    private bool isGrounded;
    private Animator anim;
    private float health = 50; 
    [SerializeField]
    private bool player1;
    [SerializeField]
    private float honeyAmt = 0; 

    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Text honeyText;
    [SerializeField]
    private GameObject otherBear;
    [SerializeField]
    private InputScript input;
    private bool canInput = true;

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip death1;
    [SerializeField]
    private AudioClip death2;
    [SerializeField]
    private AudioClip ow1, ow2;
    private AudioSource aud;

    void OnCollisionEnter(Collision other)
    {
        /*if (other.gameObject.tag == "ground")
        {
            isGrounded = true;
        }*/

        if (other.gameObject.tag == "Hive")
        {
            health -= other.gameObject.GetComponent<HiveScript>().totalBees * other.gameObject.transform.localScale.x;
            healthSlider.value = health;
            anim.SetTrigger("hit");
            float rand = Random.value;
            AudioClip ow = (rand > 0.5f) ? ow1 : ow2;
            aud.PlayOneShot(ow);
            honeyAmt += 1;
            honeyText.text = "Honey: " + honeyAmt;
            if (health <= 0)
            {
                StartCoroutine("EndGame");
            }
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
        healthSlider.maxValue = health;
        aud = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        if (player1 == true && Input.GetButtonDown("P1Jump") && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && canInput == true)
        {
            anim.SetTrigger("jump");
        }

        if (player1 == false && Input.GetButtonDown("P2Jump") && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && canInput == true)
        {
            anim.SetTrigger("jump");
        }

    }

    private IEnumerator EndGame()
    {
        print("die");
        float rand = Random.value;
        AudioClip death = (rand > 0.5f) ? death1 : death2;
        aud.PlayOneShot(death);
        input.enabled = false;
        canInput = false; 
        anim.SetTrigger("die");
        otherBear.GetComponent<Animator>().SetTrigger("dance");
        BoxCollider collider = GetComponent<BoxCollider>();
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        collider.enabled = false;
        yield return new WaitForSeconds(5);
        EventManager.fireOnUnsub();
        if (player1 == true)
        {
            SceneManager.LoadScene("Player2Win");
        }

        else
        {
            SceneManager.LoadScene("Player1Win");
        }

        
    }
}
