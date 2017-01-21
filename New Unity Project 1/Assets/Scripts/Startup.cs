using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    public GameObject l_platform;
    public GameObject r_platform;
    public GameObject hive;
    public int count = 0;
    private GameObject[] platform;
    public float amplitudeScalar = 1.0f;
    public float eqScalar = 1f;
    public float speedScalar = 0.1f;
    public float hiveScaleSpeed = 0.001f;
    [SerializeField]
    private float[] audio_data;
    [SerializeField]
    private float lerpTime;
    private float timetaken = 0;
    
    void Awake()
    {

        platform = new GameObject[count];
        int j = 0;
        for (int i = 0 - count / 2; i < count / 2; i++)
        {
            if (i % 2 == 0)
                platform[j++] = Instantiate(l_platform, new Vector3(0.95f * i, 0, 0), Quaternion.identity);
            else
                platform[j++] = Instantiate(r_platform, new Vector3(0.95f * i, 0, 0), Quaternion.identity);
        }
    }
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        audio_data = gameObject.GetComponent<Beater>().data;
        hive.transform.localScale = new Vector3(hive.transform.localScale.x + (hiveScaleSpeed), 
            hive.transform.localScale.y + (hiveScaleSpeed), hive.transform.localScale.z + (hiveScaleSpeed));
    }

    void FixedUpdate()
    {
        timetaken += Time.deltaTime;
        if (timetaken >= lerpTime)
            timetaken = 0;

        /*for (int i=0; i<count; i++)
        {
            platform[i].transform.position = new Vector3(platform[i].transform.position.x, 
                amplitudeScalar*Mathf.Sin(Time.time*(speedScalar*i)), platform[i].transform.position.z);

        }
        */
        audio_data = gameObject.GetComponent<Beater>().data;
        for (int i = 0; i < count; i++)
        {
            float beatHeight;
            if(i%2 == 0)
            {
                beatHeight = audio_data[count - i];
            }
            else
            {
                beatHeight = audio_data[i];
            }
            platform[i].transform.position = new Vector3(platform[i].transform.position.x,
                        Mathf.Lerp(amplitudeScalar * Mathf.Sin(Time.time * (speedScalar * i)),
                                   amplitudeScalar * Mathf.Sin(Time.time * (speedScalar * i)) + eqScalar * beatHeight,
                                   timetaken / lerpTime),
                        platform[i].transform.position.z);
        /*}

            for (int i = 0; i < count / 2; i++)
            {
                platform[i].transform.position = new Vector3(platform[i].transform.position.x,
                        Mathf.Lerp(platform[i].transform.position.y,
                                   platform[i].transform.position.y + eqScalar * audio_data[i],
                                   timetaken / lerpTime),
                        platform[i].transform.position.z);

                /*platform[count - 1 - i].transform.position = new Vector3(platform[count - 1 - i].transform.position.x,
                        Mathf.Lerp(platform[i].transform.position.y,
                                   platform[i].transform.position.y + eqScalar * audio_data[i],
                                   timetaken / lerpTime),
                        platform[count - 1 - i].transform.position.z);*/

            }
        }

}
