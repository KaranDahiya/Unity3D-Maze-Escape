using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float lookSensitivity = 3f;

    private PlayerMotor motor;

	// Use this for initialization
	void Start ()
    {
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //player input movement
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        //calculates vertical & horizontal velocities
        Vector3 horizontalVelocity = transform.right * xMove;
        Vector3 verticalVelocity = transform.forward * zMove;
        //calculates final velocity
        Vector3 _velocity = (horizontalVelocity + verticalVelocity).normalized * speed;
        //calls motor to apply movement
        motor.Move(_velocity);

        //player input rotation Vector
        float yRotate = Input.GetAxisRaw("Mouse X");
        //calculates final rotation
        Vector3 _rotation = new Vector3(0f, yRotate, 0f) * lookSensitivity;
        //calls motor to apply rotation
        motor.Rotate(_rotation);

        //player input camera rotation Vector
        float xRotate = Input.GetAxisRaw("Mouse Y");
        //calculates final rotation
        Vector3 _cameraRotation = new Vector3(xRotate, 0f, 0f) * lookSensitivity;
        //calls motor to apply rotation
        motor.RotateCamera(_cameraRotation);
    }
}
