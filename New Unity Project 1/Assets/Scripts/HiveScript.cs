using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveScript : MonoBehaviour {
    private Rigidbody rb;
    private bool isJumping = false;
    public bool grounded = true;

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
        //      EventManager.onStartHiveJump += jump;

        beeStart = 1;
        startPos = transform.position; 

    }

    // Update is called once per frame
    void Update () {
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
        GameObject[] bees =GameObject.FindGameObjectsWithTag("bee");
        for (int i = 0; i < bees.Length; i++)
        {
            Destroy(bees[i]);

            totalBees = 0;
        }
    }
}
