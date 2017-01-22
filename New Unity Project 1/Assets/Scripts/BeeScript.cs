using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeScript : MonoBehaviour
{
    private GameObject hive;

    private float startPos;
    private float distanceThresh;
    [SerializeField]
    private Vector3 randomDir;

	// Use this for initialization
	void Start ()
    {
        hive = transform.parent.gameObject;
        startPos = hive.GetComponent<HiveScript>().beeStart; 
        transform.position = Random.onUnitSphere;
        transform.position = new Vector3(transform.position.x * startPos, transform.position.y * startPos, transform.position.z * startPos); 
        randomDir = new Vector3(0, Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.RotateAround(hive.transform.position, randomDir,Random.Range(50, 300) * Time.deltaTime);
        Vector3 relativePos = hive.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;

    }
}
