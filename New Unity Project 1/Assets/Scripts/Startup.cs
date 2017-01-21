using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour {
    public GameObject l_platform;
    public GameObject r_platform;
    public int count = 0;
    void Awake() {
        for(int i=0-count/2; i<count/2; i+=2) {
            Instantiate(l_platform, new Vector3(0.47f*i, 0, 0), Quaternion.identity);
        }
        for (int i = 1 - count / 2; i < count / 2; i += 2)
        {
            Instantiate(r_platform, new Vector3(0.47f * i, 0, 0), Quaternion.identity);
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
