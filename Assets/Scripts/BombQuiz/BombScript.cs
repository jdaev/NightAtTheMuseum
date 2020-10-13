using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BombScript : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;

    

    public float range = 50f;
    public float force = 5f;

    public int fuseTime = 5;

    public LayerMask objects;

    public AudioClip fizzSound;
    public AudioClip explodeSound;

    AudioSource audioSource;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonClick(){
        StartCoroutine(Explode());
    }
    IEnumerator Explode()
    {
        Pulsate();
        yield return new WaitForSeconds(fuseTime);
        audioSource.clip = explodeSound;
        audioSource.Play();
        ApplyExposionForce(GetSurroundingObjects());
        
        Destroy(gameObject);
    }

    Collider[] GetSurroundingObjects()
    {
        return Physics.OverlapSphere(transform.position, range, objects);
    }

    void Pulsate()
    {
        animator.SetBool("Primed", true);
        gameObject.AddComponent<ParticleSystem>();
        audioSource.clip = fizzSound;
        audioSource.Play();
    }
    void ApplyExposionForce(Collider[] colliders)
    {
        foreach (Collider collider in colliders)
        {
            Rigidbody body = collider.gameObject.GetComponent<Rigidbody>();
            body.AddForce(CalculateForce(body.transform), ForceMode.Impulse);
        }
    }
    Vector3 CalculateForce(Transform objectTransform)
    {
        Vector3 vectorBetween = new Vector3(
            transform.position.x - objectTransform.position.x, transform.position.y - objectTransform.position.y, transform.position.z - objectTransform.position.z
        );

        return -vectorBetween.normalized * (Mathf.Pow(force,2)/vectorBetween.sqrMagnitude);
    }
}
