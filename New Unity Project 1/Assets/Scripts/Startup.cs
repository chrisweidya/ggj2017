﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour {
    public GameObject l_platform;
    public GameObject r_platform;
    public int count = 0;
    private GameObject[] platform;
    void Awake() {
        platform = new GameObject[count];
        int j = 0;
        for(int i=0-count/2; i<count/2; i++) {
            float y = Mathf.Sin(i*1.0f / count * 2 * Mathf.PI);
            if (i % 2 == 0)
                platform[j++] = Instantiate(l_platform, new Vector3(0.95f * i, 0, 0), Quaternion.identity);
            else
                platform[j++] = Instantiate(r_platform, new Vector3(0.95f * i, 0, 0), Quaternion.identity);
        }
    }
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        for(int i=0; i<count; i++)
        {
            platform[i].transform.position = new Vector3(platform[i].transform.position.x, Mathf.Sin(Time.time*(0.1f*i)), platform[i].transform.position.z);

        }
    }
}
