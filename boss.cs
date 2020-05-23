using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour {

    //references player health script
    public playerHealth playerVictim;
    //references player animator
    public Animator playerAnim;
    //boss damage
    int damageAmount = 30;
    //boss's movement speed
    float bossSpeed = 5f;
    //player's transform
    public Transform player;
    //boss's minimum distance before chasing player
    public float chaseDistance = 10f;
    //check for boss chasing or not
    private bool chase = false;
    //boss's animator
    public Animator bossAnim;
    //timer restricts damage to once per second
    float damageTimer = 1;
    //references boss audio
    public AudioClip bossRoar;
    float audioDelay = 0;

    // Use this for initialization
    void Start()
    {
        //gets boss animator
        bossAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //start with boss idle animation
        bossAnim.SetBool("bossChasing", false);
        //rotates towards player
        transform.LookAt(player);
        //finds distance between player and boss
        float distance = Vector3.Distance(player.position, transform.position);
        if ((distance < chaseDistance || chase == true) && distance > 2.5f)
        {
            //start chasing player
            transform.position += transform.forward * Time.deltaTime * bossSpeed;
            chase = true;
            //start boss chase animation
            bossAnim.SetBool("bossChasing", true);
            //starts boss sound effect
            if (audioDelay <= 0)
            {
                AudioSource.PlayClipAtPoint(bossRoar, transform.position);
                audioDelay = 5f;

            }
            else
            {
                audioDelay -= Time.deltaTime;
            }
        }
        else
        {
            //resets boss idle animation
            bossAnim.SetBool("bossChasing", false);
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
        if (col.gameObject.name == "player")
        {
            playerAnim.SetBool("IsDamaged", false);
        }
    }
}
