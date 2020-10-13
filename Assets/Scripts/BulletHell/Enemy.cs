using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float enemySpeed;

    public float shootRate;

    public MovementPattern movementPattern;
    GameObject bullet;

    float timeOfNextShot;
    private void Awake()
    {
        bullet = Resources.Load<GameObject>("Entities/EnergyBullet");
        timeOfNextShot = Time.time + shootRate;
    }
    void MoveShip()
    {
        transform.Translate(-Vector2.up * enemySpeed * Time.deltaTime);
    }

    void SineMoveShip()
    {
        Vector2 movement = new Vector2(Mathf.Sin(transform.position.y), -1 * enemySpeed) * Time.deltaTime;
        transform.Translate(movement);
    }

    void TriangleMoveShip()
    {
        float amplitude = 3;
        float period = 3;
        float x = (2 * amplitude / Mathf.PI) * Mathf.Asin(Mathf.Sin((2 * Mathf.PI * transform.position.y) / period));
        Vector2 movement = new Vector2(x, -1 * enemySpeed) * Time.deltaTime;
        transform.Translate(movement);
    }
    void ShootLaser()
    {
        if (timeOfNextShot < Time.time)
        {
            GameObject laser = GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
            timeOfNextShot = shootRate + Time.time;

        }

    }

    void Update()
    {
        switch (movementPattern)
        {
            case MovementPattern.SineWave:
            SineMoveShip();
            break;
            case MovementPattern.TriangleWave:
            TriangleMoveShip();
            break;
            default:
            MoveShip();
            break;
        }
        ShootLaser();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Enemy")
        {
            GameObject.Destroy(other.gameObject);
            GameObject.Destroy(gameObject);
        }
    }
}
