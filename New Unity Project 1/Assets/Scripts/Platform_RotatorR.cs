using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_RotatorR : MonoBehaviour
{
    //  public GameObject hive;
//    public HiveScript hiveScript;
//    public static float maxJumpHeight = 0.5f;
//    public static float jumpSpeed = 6.0f;
//    public static float fallSpeed = 20.0f;
//    private bool grounded = true;
//    private bool inputJump = false;
    private Vector3 groundPos;
 //   private bool inContact = false;
 //   private bool isJumping = false;
    private Rigidbody rb;
    private Quaternion originalRotation;
    private Vector3 originalPos;

    //let and right height offsets
    public float lheight = 0f;
    public float rheight = 0f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(-0.5f, 0, 0);
        //      hiveScript = hive.GetComponent<HiveScript>();
        EventManager.onStartJumpR += jump;
        groundPos = transform.localPosition;
        //   groundHeight = transform.position.y;
//        maxJumpHeight = transform.localPosition.y + maxJumpHeight;
        originalRotation = transform.rotation;
        originalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
 //       if (transform.localPosition == groundPos)
//            grounded = true;
 //       else
 //           grounded = false;
    }
    void FixedUpdate()
    {
    }

    private void jump(float charge)
    {
        StartCoroutine(turn(charge));
    }
    void OnCollisionStay(Collision collision)
    {
   //     if (collision.gameObject.CompareTag("Hive"))
  //          inContact = true;
    }
    void OnCollisionExit(Collision collision)
    {
  //      if (collision.gameObject.CompareTag("Hive"))
   //         inContact = false;
    }
    /*
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
    */
    IEnumerator turn(float charge)
    {
        float v = charge * 150 * Time.deltaTime;
        rb.AddTorque(transform.forward * v, ForceMode.Impulse);

        rb.constraints &= (~RigidbodyConstraints.FreezePositionY & ~RigidbodyConstraints.FreezePositionX);
     //   rb.AddForce(Quaternion.AngleAxis(45, Vector3.forward) * Vector3.up * 5f , ForceMode.Impulse);
        Debug.DrawRay(transform.position, Quaternion.AngleAxis(45, Vector3.forward) * Vector3.up * 2f);
        yield return new WaitForSeconds(0.1f);
        rb.angularVelocity = Vector3.zero;

        yield return new WaitForSeconds(0.15f);

        transform.rotation = originalRotation;
        transform.localPosition = originalPos;
        rb.constraints |= (RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX);
        yield return new WaitForEndOfFrame();
    }
}
