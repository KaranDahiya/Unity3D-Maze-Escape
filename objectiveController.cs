using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectiveController : MonoBehaviour
{
    // combat variables
    public playerController playerController;
    public RectTransform objectiveHealthBar;
    public float objectiveHealth;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // displays current player health bar
        if (objectiveHealth < 0)
            objectiveHealth = 0;
        objectiveHealthBar.localScale = new Vector3(1f, objectiveHealth, 1f);
    }

    // detects attacks received
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemyWeapon")
        {
            // lose health
            objectiveHealth -= damage;
        }
        // objective destroyed
        if (objectiveHealth <= 0 && !playerController.gameIsOver)
        {
            playerController.gameIsOver = true;
            playerController.gameOver();
        }
    }
}
