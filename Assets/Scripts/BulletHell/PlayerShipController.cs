using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerShipController : MonoBehaviour
{
    public float shipSpeed = 10f;
    bool isInBounds = true;

    public float horizontalExtent = 2;
    public float verticalExtent = 4;

    public float bulletSpeed = 5f;

    public float shootRate = 0.25f;

    public AudioClip shootSound;
    GameObject laserBolt;
    AudioSource audioSource;
    float timeOfNextShot;
    private void Awake()
    {
        laserBolt = Resources.Load<GameObject>("Entities/LaserBolt");
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = shootSound;
        timeOfNextShot = Time.time + shootRate;
    }



    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            ShootLaser();
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 position = transform.position;
        position.x = Mathf.Clamp(position.x, -horizontalExtent, +horizontalExtent);
        position.y = Mathf.Clamp(position.y, -verticalExtent, +verticalExtent);
        transform.SetPositionAndRotation(position, Quaternion.identity);

        Vector2 movement = new Vector2(horizontal, vertical) * shipSpeed * Time.deltaTime;


        transform.Translate(movement);



    }

    void ShootLaser()
    {
        if (timeOfNextShot < Time.time)
        {
            GameObject laser = GameObject.Instantiate(laserBolt, transform.position, Quaternion.identity);
            audioSource.Play();

            timeOfNextShot = shootRate + Time.time;

        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject.Destroy(other.gameObject);
            GameObject.Destroy(gameObject);
            SceneManager.LoadScene("SampleScene");
        }
    }
}
