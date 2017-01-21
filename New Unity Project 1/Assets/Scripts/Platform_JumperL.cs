using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_JumperL : MonoBehaviour {
    public static float maxJumpHeight = 0.6f;
    public static float jumpSpeed = 10.0f;
    public static float fallSpeed = 12.0f;
    private bool grounded = true;
    private bool inputJump = false;
    private Vector3 groundPos;
	// Use this for initialization
	void Start () {
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
    IEnumerator Jump()
    {
        while (true)
        {
            if (transform.localPosition.y >= maxJumpHeight)
                inputJump = false;
            if (inputJump)
                transform.Translate(Vector3.up * jumpSpeed * Time.smoothDeltaTime);
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
