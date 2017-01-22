using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour {
    public Slider slider;
    public enum Type { Left, Right};
    public Type type;
    private bool filled = true;
	// Use this for initialization
	void Start () {
        EventManager.onStartWave += fillSlider;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void fillSlider(int dir)
    {
        if (filled)
        {            
            if (type == Type.Right && dir == 1)
                StartCoroutine(fill(2.5f));
            else if (type == Type.Left && dir == -1)
                StartCoroutine(fill(2.5f));
        }

    }
    IEnumerator fill(float seconds)
    {
        filled = false;
        float timeElapsed = 0;
        while(timeElapsed < seconds)
        {            
            slider.value = timeElapsed / seconds * 100;
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        filled = true;
    }
}
