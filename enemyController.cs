using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    // motion variables
    public float alertRange;
    public float attackRange;
    Transform player;
    Transform objective;
    playerController playerController;
    NavMeshAgent nav;

    // animation variables
    public Animator enemyAnimator;

    // combat variables
    public float enemyHealth;
    public float damage;
    bool isDead = false;

    // Start is called before the first frame update
    void Awake()
    {
        // Set up the references.
        objective = GameObject.FindGameObjectWithTag("objective").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<playerController>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // distance from player & objective
        float playerDistance = Vector3.Distance(transform.position, player.position);
        float objectiveDistance = Vector3.Distance(transform.position, objective.position);

        // if player alive & in alert range
        if (playerController.playerHealth > 0 && playerDistance < alertRange)
        {
            // set destination of nav mesh agent to player
            nav.SetDestination(player.position);

            // if player is close enough to attack
            if (playerDistance < attackRange)
            {
                // play attack animation
                enemyAnimator.SetTrigger("isAttacking");
            }
            else
                enemyAnimator.SetBool("isWalking", true);
        }
        // if player not alive or not in alert range
        else
        {
            // set destination of nav mesh agent to objective
            nav.SetDestination(objective.position);

            // if objective is close enough to attack
            if (playerDistance < attackRange)
            {
                // play attack animation
                enemyAnimator.SetTrigger("isAttacking");
            }
            else
                enemyAnimator.SetBool("isWalking", true);
        }
    }

    // detects attacks received
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "playerWeapon")
        {
            // lose health
            enemyHealth -= damage;
        }
        // enemy dies
        if (enemyHealth <= 0 && !isDead)
        {
            isDead = true;

            // play death animation
            enemyAnimator.SetBool("isDead", true);

            // destroy enemy
            Destroy(gameObject, 1.5f);
        }
    }
}
