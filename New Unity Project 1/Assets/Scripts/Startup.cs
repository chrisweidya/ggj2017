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
    public float forceValue = 2.0f;
    public float eqScalar = 1f;
    public float speedScalar = 0.1f;
    public float hiveScaleSpeed = 0.001f;
    public float pulseCooldown = 3f;
    [SerializeField]
    private float[] audio_data;
    [SerializeField]
    private float lerpTime;
    private float timetaken = 0;
    private float sinTime = 0;

    private bool pulseRight = false;
    private bool pulseLeft = false;
    private bool canPulseRight = true;
    private bool canPulseLeft = true;
    public float period = 1f;
    private Vector3 originalHeight;

    [Header("Wave Makers")]
    [SerializeField]  
    private GameObject WaveMakerL;
    [SerializeField]
    private GameObject WaveMakerR;
    
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

      
        originalHeight = platform[0].transform.position;
    }
    // Use this for initialization
    void Start()
    {
        EventManager.onStartWave += pulseSender;
        InvokeRepeating("propagator", 0f, 0.04f);
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
        if(pulseRight)
        {
            sinTime += Time.deltaTime;
            float y = amplitudeScalar * Mathf.Sin(10 * sinTime);
            WaveMakerR.transform.position = new Vector3(platform[count - 2].transform.position.x, 0, 0);
            WaveMakerR.GetComponent<Rigidbody>().velocity = new Vector3(-30, 0, 0);
            MainPlatform pr = platform[count - 1].GetComponent<MainPlatform>();
            
            platform[count - 1].transform.position = new Vector3(platPos(count-1).x, y, platPos(count-1).z);
            
            pr.rheight = y;
            if(16*sinTime >= period && y >- 0.1f && y < 0.1f)
            {
                sinTime = 0;
                platform[count - 1].transform.position = new Vector3(platPos(count - 1).x, 0, platPos(count - 1).z);
                pulseRight = false;
                pr.rheight = 0;
            }
        }
        if (pulseLeft)
        {
            sinTime += Time.deltaTime;
            float y = amplitudeScalar * Mathf.Sin(10 * sinTime);
            WaveMakerL.transform.position = new Vector3(platform[1].transform.position.x, 0, 0);
            WaveMakerL.GetComponent<Rigidbody>().velocity = new Vector3(30, 0, 0);

           
            MainPlatform pr = platform[0].GetComponent<MainPlatform>();
            platform[0].transform.position = new Vector3(platPos(0).x, y, platPos(0).z);
            
            pr.lheight = y;
            if (16 * sinTime >= period && y > -0.1f && y < 0.1f)
            {
                sinTime = 0;
                platform[0].transform.position = new Vector3(platPos(0).x, 0, platPos(0).z);
                pulseLeft = false;
                pr.lheight = 0;
            }
        }
        audio_data = gameObject.GetComponent<Beater>().data;
        for (int i = 1; i < count-1; i++)
        {
            float beatHeight;
            if(i%2 == 0)
            {
                beatHeight = audio_data[count - i - 1] ;
            }
            else
            {
                beatHeight = audio_data[i];
            }
            
            platform[i].transform.position = new Vector3(platform[i].transform.position.x,
                        Mathf.Lerp(platform[i].transform.position.y,
                                   platform[i].transform.position.y + eqScalar * beatHeight,
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
    float calcHeight(float cycle)
    {
        float phase = 2 * Mathf.PI * cycle;
        float height = amplitudeScalar * Mathf.Sin(phase);
        return height;
    }

    void pulseSender(int dir)
    {
        if (dir == 1 && canPulseRight)
        {
            StartCoroutine(pulseRightCooldown(pulseCooldown));
            pulseRight = true;
        }
        else if (dir == -1 && canPulseLeft)
        {
            StartCoroutine(pulseLeftCooldown(pulseCooldown));
            pulseLeft = true;
        }
    }

    void propagator()
    {
        Vector3 currPlatPos;

        MainPlatform pr, prl, prr;
        float lh, rh, ht;
        
        for (int i = count - 2; i > 0; i--)
        {
            currPlatPos = platPos(i);
            pr = platform[i].GetComponent<MainPlatform>();
            prl = platform[i - 1].GetComponent<MainPlatform>();
            prr = platform[i + 1].GetComponent<MainPlatform>();


            pr.lheight = prl.lheight;
            

        }

        for (int i = 1; i < count - 1 ; i++)
        {
            currPlatPos = platPos(i);
            pr = platform[i].GetComponent<MainPlatform>();
            prl = platform[i - 1].GetComponent<MainPlatform>();
            prr = platform[i + 1].GetComponent<MainPlatform>();

            pr.rheight = prr.rheight;
            
        }

        for (int i=1;i<count-1;i++)
        {
            pr = platform[i].GetComponent<MainPlatform>();
            
            lh = pr.lheight; 
            rh = pr.rheight;
            ht = lh + rh;
            
            platform[i].transform.position = new Vector3(platPos(i).x,
                ht,
                platPos(i).z);
        }        
    }

    Vector3 platPos(int i)
    {
        return platform[i].transform.position;
    }
    IEnumerator pulseLeftCooldown(float secs)
    {
        canPulseLeft = false;
        yield return new WaitForSeconds(secs);
        canPulseLeft = true;
        yield return null;
    }
    IEnumerator pulseRightCooldown(float secs)
    {
        canPulseRight = false;
        yield return new WaitForSeconds(secs);
        canPulseRight = true;
        yield return null;
    }

    IEnumerator wavezero(Rigidbody wm, float origY)
    {
        yield return new WaitForSeconds(0.05f);
        wm.velocity = Vector3.zero;
        Vector3 go = wm.gameObject.transform.position;
        wm.gameObject.transform.position = new Vector3(go.x, origY, go.z);

    }


}
