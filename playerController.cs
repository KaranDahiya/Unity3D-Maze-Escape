using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    // motion variables
    public CharacterController playerCharacterController;
    public float moveSpeed;
    public float rotateSpeed;

    // animation variables
    public Animator playerAnimator;

    // combat variables
    public RectTransform playerHealthBar;
    public float playerHealth;
    public float damage;
    public float regenRate;

    // gameover variables
    public Camera thirdPersonCamera;
    public Camera overheadCamera;
    public Collider castleCollider;
    public GameObject objective;
    public Text gameOverText;
    public bool gameIsOver = false;

    // timer variables
    public Text timerText;
    float startTime;

    // spawn variables
    public GameObject enemy;
    public float spawnTime;
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        // beginning settings
        thirdPersonCamera.enabled = true;
        overheadCamera.enabled = false;
        gameOverText.enabled = false;

        // gets time at start
        startTime = Time.time;

        // spawns enemies repeatedly after a delay of spawnTime
        InvokeRepeating("spawn", spawnTime, spawnTime);

        // player heals over time
        InvokeRepeating("regenerate", regenRate, regenRate);
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
        playerAttack();

        // displays current player health bar
        if (playerHealth < 0)
            playerHealth = 0;
        playerHealthBar.localScale = new Vector3(1f, playerHealth, 1f);

        // displays current time
        if (!gameIsOver)
        {
            float t = Time.time - startTime;
            string seconds = ((int)t % 60).ToString();
            timerText.text = seconds;
        }

        if (gameIsOver)
        {
            nextLevel();
        }
    }

    void playerMove()
    {
        // check if player IS WALKING
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            // starts walking animation
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            // ends walking animation
            playerAnimator.SetBool("isWalking", false);
        }

        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);    // key for moving in terms of object axis instead of global axis
        float currentSpeed = moveSpeed * Input.GetAxis("Vertical");
        playerCharacterController.SimpleMove(forward * currentSpeed);
    }

    void playerAttack()
    {
        // check if player IS ATTACKING
        if (Input.GetKey(KeyCode.Space))
        {
            // starts attacking animation
            playerAnimator.SetTrigger("isAttacking");
        }
    }

    // detects received attacks
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemyWeapon")
        {
            // lose health
            playerHealth -= damage;
        }
        // player dies
        if (playerHealth <= 0 && !gameIsOver)
        {
            gameIsOver = true;
            gameOver();
        }
    }

    // game ends
    public void gameOver()
    {
        print("gameover");
        // plays death animation
        playerAnimator.SetBool("isDead", true);

        // switch camera
        Invoke("switchCameras", 5f);

        // enemies pass through castle gates
        castleCollider.enabled = false;
        Vector3 objectivePosition = objective.transform.position;
        objectivePosition += new Vector3(0, 0, -20);
        objective.transform.position = objectivePosition;

        // changes timer text to gameover
        timerText.text = "SCORE: " + timerText.text;
        gameOverText.enabled = true;
    }

    void switchCameras()
    {

        // switches cameras after delay for animation to complete
        thirdPersonCamera.enabled = false;
        overheadCamera.enabled = true;
    }

    // spawns enemies
    void spawn()
    {
        // game is over
        if (playerHealth <= 0)
        {
            // stop spawning
            return;
        }

        // get random spawn point
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // spawn enemy prefab at the random spawn point
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    void regenerate()
    {
        if (!gameIsOver && playerHealth < 1)
        {
            playerHealth += 0.01f;
        }
    }

    // player decided what to do after gameover
    void nextLevel()
    {
        // restart game if 'space' pressed
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // restart game if 'space' pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
