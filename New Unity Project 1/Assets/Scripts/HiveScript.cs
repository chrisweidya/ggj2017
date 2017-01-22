using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveScript : MonoBehaviour {
    private Rigidbody rb;
    private bool isJumping = false;
    public bool grounded = true;
    public float scaleSpeed = 0.001f;

    [SerializeField]
    private GameObject bee;
    public float beeStart;
    [SerializeField]
    private float sphereIncrease;
    public int totalBees;
    private Vector3 startPos; 

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        transform.localScale = new Vector3(1, 1, 1);
        rb.velocity = Vector3.zero;
        beeStart = 1;
        startPos = transform.position; 

    }

    // Update is called once per frame
    void Update () {

        transform.localScale = new Vector3(transform.localScale.x + scaleSpeed,
            transform.localScale.y + scaleSpeed, transform.localScale.z + scaleSpeed);
    }

    void jump()
    {
        rb.AddForce(new Vector3(0, 0.10f * Time.deltaTime, 0), ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "L_Platform" || other.gameObject.tag == "R_Platform")
        {
            SpawnBee(); 
        }
    }

    private void SpawnBee()
    {
        GameObject newBee = Instantiate(bee, transform.position, Quaternion.identity);
        newBee.transform.parent = this.transform; 
        totalBees++;
        beeStart += sphereIncrease; 
    }

    public void respawn()
    {
        transform.position = startPos;
        rb.velocity = Vector3.zero;
        transform.localScale = new Vector3(1, 1, 1);
        foreach (Transform child in transform)
        {
            if (child.CompareTag("bee"))
            {
                Destroy(child.gameObject);
                print("destroyed");
            }

        }

        totalBees = 0;
        beeStart = 1; 
    }

}
