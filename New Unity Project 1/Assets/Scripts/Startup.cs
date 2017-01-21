using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour {
    public GameObject platform;
    public int count = 0;
    void Awake()
    {
        for(int i=0-count/2; i<count/2; i++)
        {
            Instantiate(platform, new Vector3(0.2f*i, 0, 0), Quaternion.identity);
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
