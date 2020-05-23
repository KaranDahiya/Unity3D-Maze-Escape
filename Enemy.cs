using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float damage = 2f;
    public float cooldown = 0.5f;
    public float startingHealth = 6f;

    [SerializeField]
    private float currentHealth = 6f;

    private float range;
    private float currentDistance;
    private float minDistance = 0.5f;
    private NavMeshAgent nav;
    private bool collided = false;

    // Use this for initialization
    void Start ()
    {
        nav = GetComponent<NavMeshAgent>();
        currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // calculates distance bewteen player & enemy, decided whether or not to move
        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        if (currentDistance > minDistance)
        {
            ChasePlayer();
        }
	}

    // moves towards player
    void ChasePlayer()
    {
        nav.SetDestination(player.transform.position);
    }

    //detects collision with player
    void OnCollisionStay(Collision collision)
    {
        if(collided == true)
        {
            return;
        }
        else if(collision.gameObject.tag == "Player")
        {
            collided = true;
            Debug.Log("collided w/Player");
            StartCoroutine(Attack(collision.gameObject));
        }
    }

    //attacks player
    IEnumerator Attack(GameObject player)
    {
        yield return new WaitForSeconds(cooldown);
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
        collided = false;
    }

    // takes damage
    public void enemyTakeDamage(float damageDealt)
    {
        currentHealth -= damageDealt;
        if (currentHealth <= 0)
        {
            EnemyDeath();
        }
    }

    //enemy dies
    void EnemyDeath()
    {
        //destroys enemy
        Destroy(gameObject);
        Debug.Log("Enemy Death");
    }
}
