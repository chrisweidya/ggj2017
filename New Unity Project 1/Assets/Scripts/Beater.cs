using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beater : MonoBehaviour {
    public float[] data;
    [SerializeField]
    private int channels;
    AudioSource al;
	// Use this for initialization
	void Start () {
        data = AudioListener.GetSpectrumData(channels, 0, FFTWindow.Hamming);
    }
	
	// Update is called once per frame
	void Update () {
        data = AudioListener.GetSpectrumData(channels, 0, FFTWindow.Hamming);

    }
}
