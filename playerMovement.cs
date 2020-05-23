using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    //player movement speed
    public float moveSpeed = 10f;
    //player rotation speed
    public float rotateSpeed = 10f;
    //player animator
    public Animator playerAnim;

	// Use this for initialization
	void Start () {
        //gets player animator
        playerAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        //movement & rotation of player
        float playerMove = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float playerRotate = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        transform.Translate(playerMove, 0f, 0f);
        transform.Rotate(0f, playerRotate, 0f);
        //player walking & idle animations 
        playerAnim.SetBool("isWalking", false);
        if (playerMove != 0 || playerRotate != 0)
        {
            playerAnim.SetBool("isWalking", true);
        }
        else
        {
            playerAnim.SetBool("isWalking", false);
        }
    }
}
