using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minion : MonoBehaviour {

    //references player health script
    public playerHealth playerVictim;
    //references player animator
    public Animator playerAnim;
    //minion damage
    int damageAmount = 10;
    //minion's movement speed
    float minionSpeed = 5f;
    //player's transform
    public Transform player;
    //minion's minimum distance before chasing player
    public float chaseDistance = 10f;
    //check for minion chasing or not
    private bool chase = false;
    //minion's animator
    public Animator minionAnim;
    //timer restricts damage to once per second
    float damageTimer = 1;
    //references minion sound
    public AudioClip minionRoar;
    float audioDelay = 0;

    // Use this for initialization
    void Start () {
        //gets minion animator
        minionAnim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //start with minion idle animation
        minionAnim.SetBool("IsChasing", false);
        //rotates towards player
        transform.LookAt(player);
        //finds distance between player and minion
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < chaseDistance || chase == true)
        {
            //start chasing player
            transform.position += transform.forward * Time.deltaTime * minionSpeed;
            chase = true;
            //start minion chase animation
            minionAnim.SetBool("IsChasing", true);
            //start minion sound effect
            if(audioDelay <= 0)
            {
                AudioSource.PlayClipAtPoint(minionRoar, transform.position);
                audioDelay = 3f;

            }
            else
            {
                audioDelay -= Time.deltaTime;
            }
        }
        else
        {
            //resets minion idle animation
            minionAnim.SetBool("IsChasing", false);
        }
	}

    //collision with player
    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.name == "player")
        {
            //plays player damaged animation
            playerAnim.SetBool("IsDamaged", true);
            if (damageTimer > 0.5)
            {
                damageTimer -= Time.deltaTime;
            }
            else
            {
                //damages player
                playerVictim.attack(damageAmount);
                damageTimer = 1;
            }
        }
    }

    //when player no longer being damaged
    void OnCollisionExit(Collision col)
    {
        if(col.gameObject.name == "player")
        {
            playerAnim.SetBool("IsDamaged", false);
        }
    }
}
