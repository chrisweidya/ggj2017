using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeScript : MonoBehaviour
{
    private GameObject hive;

    [SerializeField]
    private AudioClip[] clips;
    private AudioSource aud;

    private float startPos;
    private float distanceThresh;
    [SerializeField]
    private Vector3 randomDir;

    void Awake()
    {
        aud = GetComponent<AudioSource>();
        aud.clip = clips[(int)Random.Range(0f, 3.99f)];
        aud.loop = true;
        aud.Play();
    }

	// Use this for initialization
	void Start ()
    {
        hive = transform.parent.gameObject;
        startPos = hive.GetComponent<HiveScript>().beeStart; 
        transform.position = Random.onUnitSphere;
        transform.position = new Vector3(transform.position.x * startPos, transform.position.y * startPos, transform.position.z * startPos); 
        randomDir = new Vector3(0, Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        aud = GetComponent<AudioSource>();
        aud.clip = clips[(int)Random.Range(0f, 3.99f)];
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
