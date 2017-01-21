using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeScript : MonoBehaviour
{
    private GameObject hive;

    private float startPos;
    private float distanceThresh; 
    private float speed;
    [SerializeField]
    private Vector3 randomDir; 

	// Use this for initialization
	void Start ()
    {
        hive = GameObject.FindGameObjectWithTag("Hive");
        startPos = hive.GetComponent<BeeHive>().beeStart; 
        transform.position = Random.onUnitSphere;

        randomDir = new Vector3(0, Random.Range(-1f, 1f), Random.Range(-1f, 1f));

    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.RotateAround(hive.transform.position, randomDir,Random.Range(50, 300) * Time.deltaTime);
	}
}
