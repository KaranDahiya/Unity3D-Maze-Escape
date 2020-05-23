using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{

    //player's initial health points
    public int startHP = 100;
    //player's current health points
    public int currentHP;
    //references player's animator
    public Animator playerAnim;
    //references display texts
    public GameObject gameOverText;
    public GameObject victoryText;
    public GameObject restartText;
    //checks whether player won or lost
    bool defeat = false;
    //references player rigidbody
    public Rigidbody playerRigidbody;
    //references game over walls
    public GameObject gameOverWalls;
    //references all enemies in scene
    public GameObject enemies;
    //references player-related sounds
    public AudioClip victoryJingle;
    public AudioClip lossJingle;
    bool audioPlaying = false;
    //references health bar
    public Slider healthSlider;



    // Use this for initialization
    void Start()
    {
        //sets up
        currentHP = startHP;
        playerAnim = GetComponent<Animator>();
        gameOverText.SetActive(false);
        victoryText.SetActive(false);
        restartText.SetActive(false);
        enemies.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRigidbody.position.y < -5f)
        {
            gameOver();
        }
        healthSlider.value = currentHP;
    }

    //when player is attacked
    public void attack(int damageAmount)
    {
        //reduces player hp
        currentHP -= damageAmount;
        if (currentHP <= 0)
        {
            //player death
            defeat = true;
            playerAnim.SetBool("IsDead", true);
            gameOver();
        }
    }
    
    //gameOver
    public void gameOver()
    {
        if(defeat == true)
        {
            gameOverText.SetActive(true);
            gameOverWalls.SetActive(true);
            GetComponent<AudioSource>().Stop();
            if(audioPlaying == false)
            {
                AudioSource.PlayClipAtPoint(lossJingle, transform.position, 100f);
                audioPlaying = true;
            }
        }
        else
        {
            victoryText.SetActive(true);
            enemies.SetActive(false);
            if (audioPlaying == false)
            {
                AudioSource.PlayClipAtPoint(victoryJingle, transform.position);
                audioPlaying = true;
            }
        }
        restartText.SetActive(true);
    }

    //restarts scene
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

    
