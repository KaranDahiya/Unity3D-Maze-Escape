using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private Rigidbody rb;

    [SerializeField]
    private Camera cam;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// FixedUpdate is called once per physics iteration
	void FixedUpdate ()
    {
        PerformMovement();
        PerformRotation();
	}

    //gets a vector for the movement from player controller
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //gets a vector for the rotation from player controller
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //gets a vector for the camera rotation from player controller
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    //actually moves player
    void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    //actually rotates 
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }
}
