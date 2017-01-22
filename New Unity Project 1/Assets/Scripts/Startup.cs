using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Startup : MonoBehaviour
{
    public GameObject platforms;
    public GameObject l_platform;
    public GameObject r_platform;
    public GameObject hive;
    public int count = 0;
    private GameObject[] platform;
    public float amplitudeScalar = 1.0f;
    public float forceValue = 2.0f;

    public float speedScalar = 0.1f;
    public float hiveSpawnInterval = 30f;
    public float pulseCooldown = 3f;
    
    private float sinTime = 0;
    
    private bool pulseRight = false;
    private bool pulseLeft = false;
    private bool canPulseRight = true;
    private bool canPulseLeft = true;
    public float period = 1f;
    private Vector3 originalHeight;
    private float timeElapsed = 0f;

    [SerializeField]
    private AudioSource aud;
    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip whooshup;
    [SerializeField]
    private AudioClip whooshdown;
    
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
            platform[j - 1].transform.parent = platforms.transform;
        }

      
        originalHeight = platform[0].transform.position;

       
    }
    // Use this for initialization
    void Start()
    {
        EventManager.onUnsub += unsubscribe;
        EventManager.onStartWave += pulseSender;
        InvokeRepeating("propagator", 0f, 0.04f);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > hiveSpawnInterval)
        {
            Instantiate(hive, new Vector3(0, 10, 0), Quaternion.identity);
            timeElapsed = 0;
        }   
    }

    void FixedUpdate()
    {
        
       
        if(pulseRight)
        {
            sinTime += Time.deltaTime;
            float y = amplitudeScalar * Mathf.Sin(10 * sinTime);
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
            aud.PlayOneShot(whooshup);
        }
        else if (dir == -1 && canPulseLeft)
        {
            StartCoroutine(pulseLeftCooldown(pulseCooldown));
            pulseLeft = true;
            aud.PlayOneShot(whooshdown);
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
            if(lh > rh + 0.1f)
                pr.turn(50);
            else if( rh > lh + 0.1f)
                pr.turn(-50);
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
    public void unsubscribe()
    {
        EventManager.onStartWave -= pulseSender;
        EventManager.onUnsub -= unsubscribe;
    }


}
