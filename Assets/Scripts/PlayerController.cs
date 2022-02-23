using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Inspiration for movement controls from the following tutorial series: https://www.youtube.com/watch?v=LqnPeqoJRFY
//Modified by Carter Moore
public class PlayerController : MonoBehaviour
{
    float playerHeight = 2f;

    [Header("Movement")]
    public float speed = 6f;
    public float movementMultiplier = 10f;
    public float airMultiplier = 0.4f;
    public float jumpForce = 10.0f;
    [Header("Drag")]
    public float groundDrag = 6f;
    public float airDrag = 2f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    float groundDistance = 0.45f;

    [Header("Ground Detection")]

    RaycastHit slopeHit;
    Vector3 slopeMoveDirection;
    Vector3 moveDirection;

    bool isDead;
    bool dying;
    bool canFire; //can the player fire

    Rigidbody rb;
    [SerializeField] Transform orientation;

    public GameObject SpeedBullet;
    public GameObject SlowBullet;

    public GameObject Camera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Make sure player doesn't spin from forces
        rb.freezeRotation = true;
        isDead = false;
        dying = false;
        canFire = true;
    }

    private void Update()
    {
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (dying)
            {

            }
            return;
        }
        //Check if the player is touching the ground
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1.1f, 0), groundDistance, groundMask);

        handleInput();
        //Keep setting drag to make sure the player doesn't slide with normal movement
        setDrag();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    //Handles keyboard input from the player
    private void handleInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        //Use orientation.forward and orientation.right so direction is relative to direction player is facing
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;


        //fire speed bullet
        if(Input.GetButtonDown("Fire1") && canFire)
        {
            GameObject b = Instantiate(SpeedBullet, new Vector3(0f, 0f, 0f), Quaternion.identity);

            //offset it, then give initial velocity (the second argument of the initprojectile is the speed)
            b.GetComponent<FastBulletController>().InitProjectile(transform.position + Camera.transform.forward*2.0f, Camera.transform.forward*4f);
            StartCoroutine(PlayerCanFireAgain());
            canFire = false;

        }

        //fire slow bullet
        if (Input.GetButtonDown("Fire2") && canFire)
        {
            GameObject b = Instantiate(SlowBullet, new Vector3(0f, 0f, 0f), Quaternion.identity);

            b.GetComponent<SlowBulletController>().InitProjectile(transform.position + Camera.transform.forward * 2.0f, Camera.transform.forward * 4f);
            StartCoroutine(PlayerCanFireAgain());
            canFire = false;

        }
    }

    //Control drag for in-air vs on-ground movement
    private void setDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        //Less drag is applied when in the air
        else
        {
            rb.drag = airDrag;
        }
    }

    //Handle moving the player
    private void movePlayer()
    {
        if(isGrounded && !onSlope())
        {
            rb.AddForce(moveDirection.normalized * speed * movementMultiplier, ForceMode.Acceleration);
        }
        //Make it so player moves perpendicular to slope if on a slope
        else if(isGrounded && onSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * speed * movementMultiplier, ForceMode.Acceleration);
        }
        //Make it so the player moves slower while in the air
        else if(!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * speed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    //Handles jumping
    private void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    //Check if the player is on a slope
    private bool onSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight/2 + 0.55f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    //Handle player death
    public void TriggerDeath()
    {
        isDead = true;
        dying = true;
        GetComponent<CameraController>().TriggerDeath();
        //Stop movement after death
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public bool IsDead(){ return isDead; }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Slow"))
        {
            TriggerDeath();
        }
    }

    IEnumerator PlayerCanFireAgain()
    {
        //this will pause the execution of this method for 1 seconds without blocking
        yield return new WaitForSecondsRealtime(1);
        canFire = true;
    }

}
