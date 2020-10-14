using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D playerBody;
    Animator ballAnimator;
    float playerSpeed = 10f;
    float playerRadius = 1f;
    float jumpForce = 0.5f;
    int score;


    public LayerMask groundLayer;
    public LayerMask obstacles;
    public LayerMask hoops;

    public AudioClip ballPop;
    public AudioClip hoopHit;
    public AudioClip jumpSound;

    AudioSource audioSource;
    void Start()
    {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
        ballAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        MovePlayer();
    }


    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, playerRadius, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) | obstacles) == obstacles)
        {
            PopBall();
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (((1 << collider.gameObject.layer) | hoops) == hoops)
        {
            ScoreHoop(collider.gameObject);
        }
    }
    void MovePlayer()
    {

        float horizontalMovement = Input.GetAxis("Horizontal");
        Vector2 targetVelocity = new Vector2(horizontalMovement * playerSpeed, playerBody.velocity.y);
        playerBody.velocity = targetVelocity;
        SetHorizontalAnimator(horizontalMovement);
        if (Input.GetKey(KeyCode.W))

        {
            if (IsGrounded()) Jump();
        }

    }

    void SetHorizontalAnimator(float val)
    {
        if (val > 0)
        {
            ballAnimator.SetInteger("HorizontalAxis", 1);
        }
        if (val < 0)
        {
            ballAnimator.SetInteger("HorizontalAxis", -1);
        }
        else
        {
            ballAnimator.SetInteger("HorizontalAxis", 0);
        }
    }

    void Jump()
    {
        // audioSource.clip = jumpSound;
        // audioSource.Play();
        playerBody.AddForce(-Vector2.down * jumpForce, ForceMode2D.Impulse);
    }

    void PopBall()
    {
        Debug.Log("You died!");
        audioSource.clip = ballPop;
        audioSource.Play();
        SceneManager.LoadScene("SampleScene");

    }

    void ScoreHoop(GameObject gameObject)
    {
        gameObject.SetActive(false);
        audioSource.clip = hoopHit;
        audioSource.Play();
        score++;
        UIManager uIManager = GameLinks.instance.uIManager;
        uIManager.Score();
        Debug.Log(string.Format("Score {0}", score));
    }
}
