using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerScript : MonoBehaviour
{
    public Transform player;
    //private Animator anim;
    private Rigidbody enemyBody;
    public float maxVelocityChange = 15.0f;
    public float detectionRadius = 20f;
    public float stopDistance = 10f;

    public GameObject spell;

    NavMeshAgent agent;
    Animator animator;

    public float spellSpawnRate;
    public float spellForce = 10f;

    private float spawnTimer = 0;
    void Start()
    {
        //anim = GetComponent<Animator>();
        enemyBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        enemyBody.isKinematic = false;
        agent = GetComponent<NavMeshAgent>();
        spawnTimer = Time.time;
    }

    void Update()
    {


        float distance = Vector3.Distance(transform.position, player.position);
        animator.SetFloat("Speed", agent.speed);
        if (distance < detectionRadius)
        {
            agent.SetDestination(player.position);
            if (distance <= stopDistance)
            {
                agent.isStopped = true;
                transform.LookAt(player.position);

                if (spawnTimer <= Time.time)
                {
                    spawnTimer = spellSpawnRate + Time.time;
                    Shoot();
                }
            }
            else
            {
                agent.isStopped = false;
            }
        }



    }

    void Shoot()
    {
        animator.SetTrigger("IsAttacking");
        GameObject obj = GameObject.Instantiate(spell, transform.position + transform.forward, Quaternion.identity);
        Vector3 direction = player.transform.position - obj.transform.position;
        obj.GetComponent<Rigidbody>().AddForce(direction.normalized * spellForce, ForceMode.Impulse);

    }


}
