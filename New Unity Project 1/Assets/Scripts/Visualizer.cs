using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{

    private GameObject[] platform;
    public int count = 0;
    [SerializeField]
    private GameObject aud_platform;

    private float timetaken = 0;
    [SerializeField]
    private float lerpTime;

    private float[] audio_data;

    [SerializeField]
    private float eqScalar = 5f;

    void Awake()
    {

        platform = new GameObject[count];
        int j = 0;
        for (int i = 0; i < count; i++)
        {

            platform[i] = Instantiate(aud_platform, new Vector3(0.95f * i - 9.5f, -1, -2), Quaternion.identity);


        }
    }

    // Use this for initialization
    void Start()
    {
        audio_data = gameObject.GetComponent<Beater>().data;
    }

    void FixedUpdate()
    {
        timetaken += Time.deltaTime;
        if (timetaken >= lerpTime)
            timetaken = 0;

        audio_data = gameObject.GetComponent<Beater>().data;
        for (int i = 0; i < count; i++)
        {
            float beatHeight;
            beatHeight = audio_data[i];
            

            platform[i].transform.position = new Vector3(platform[i].transform.position.x,
                        Mathf.Lerp(platform[i].transform.position.y,
                                   eqScalar * beatHeight - 2f,
                                   timetaken / lerpTime),
                        platform[i].transform.position.z);

        }
    }
}
 