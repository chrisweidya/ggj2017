using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_JumperL : MonoBehaviour
{
    public GameObject hive;
    public HiveScript hiveScript;
    public static float maxJumpHeight = 0.5f;
    public static float jumpSpeed = 6.0f;
    public static float fallSpeed = 20.0f;
    private bool grounded = true;
    private bool inputJump = false;
    private Vector3 groundPos;
    private bool inContact = false;
	// Use this for initialization
	void Start () {

        hiveScript = hive.GetComponent<HiveScript>();
        EventManager.onStartJumpL += jump;
        groundPos = transform.localPosition;
     //   groundHeight = transform.position.y;
        maxJumpHeight = transform.localPosition.y + maxJumpHeight;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.localPosition == groundPos)
            grounded = true;
        else
            grounded = false;
    }

    private void jump() {
        if(grounded)
        {
            groundPos = transform.localPosition;
            inputJump = true;
            StartCoroutine("Jump");
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hive"))
            inContact = true;
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hive"))
            inContact = false;
    }
    IEnumerator Jump()
    {
        while (true)
        {
            if (transform.localPosition.y >= maxJumpHeight)
                inputJump = false;
            if (inputJump)
            {
                transform.Translate(Vector3.up * jumpSpeed * Time.smoothDeltaTime);
                if (hiveScript.grounded && inContact)
                    EventManager.fireOnStartHiveJump();
            }
            else if (!inputJump)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, groundPos, fallSpeed * Time.smoothDeltaTime);
                if (transform.localPosition == groundPos)
                    StopAllCoroutines();
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
