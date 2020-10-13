using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCharacterController : MonoBehaviour
{

    public float speed = 20;
    public float jumpHeight = 2;

    Rigidbody rigidBody;

    public GameObject gameOverScreen;

    public Animator animator;


    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    private bool grounded = false;

    private float gravity = 9.8f;




    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        animator.SetBool("Grounded", grounded);
        if (grounded)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");

            Vector3 targetVelocity = new Vector3(horizontalMovement, 0, verticalMovement);
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            animator.SetFloat("HorizontalMovement", horizontalMovement);
            animator.SetFloat("VerticalMovement", verticalMovement);


            Vector3 velocity = rigidBody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            rigidBody.AddForce(velocityChange, ForceMode.VelocityChange);


            if (canJump && Input.GetKey(KeyCode.Space))
            {
                rigidBody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
        }



        grounded = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Spell")
        {
    //        gameOverScreen.SetActive(true);
        }
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {

        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }


}


