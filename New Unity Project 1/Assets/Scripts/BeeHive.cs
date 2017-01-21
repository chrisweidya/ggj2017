using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHive : MonoBehaviour
{
    [SerializeField]
    private GameObject bee; 
    public float beeStart;
    [SerializeField]
    private float sphereIncrease;
    [SerializeField]
    private int beeSpawnAmount; 

	// Use this for initialization
	void Start ()
    {
        beeStart = 1;	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.tag == insert tag)
        {
            beeStart += sphereIncrease;
            for (int i = 0; i < beeSpawnAmount; i++)
            {
                Instantiate(bee, transform.position, Quaternion.identity);
            }
            beeSpawnAmount ++; 
        }
    }
}
