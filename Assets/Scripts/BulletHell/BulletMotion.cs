using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMotion : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletSpeed;
    public bool isEnemy;
    void MoveBullet()
    {
        Vector2 movement = isEnemy ? -Vector2.up * bulletSpeed * Time.deltaTime : Vector2.up * bulletSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
    }
}
