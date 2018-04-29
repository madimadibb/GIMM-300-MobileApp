using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    private CharacterController controller;
    private Vector3 moveVector; //start of UI controls
    private float speed = 5.0f;
    private float gravity = 12.0f;
    private float verticalVelocity = 0.0f; // if not falling,
    private float animationDuration = 3.0f;
    private bool isDead = false;

    private float startTime;
  
    void Start () {
        controller = GetComponent<CharacterController>();
        startTime = Time.time; //fixed glitch that after restsart 
        //there was free player movement
	}
	
	// Update is called once per frame
	void Update () {

        if (isDead)
            return;
        //wont move after death
        
        if(Time.time - startTime < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return; //exit 
        }
        moveVector = Vector3.zero; //resets every frame

        if(controller.isGrounded)
        {
            verticalVelocity = -0.5f; //no falling
        }

        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        //calc x(left and right) 
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed; //no grav involvment from editor settings 
        if (Input.GetMouseButton(0)) 
        {
            //holding on right?
            if (Input.mousePosition.x > Screen.width / 2)
                moveVector.x = speed;
            else
                moveVector.x = -speed;
        }
        //y(up and down) 
        moveVector.y = verticalVelocity;

        //and z(forward and backward) values
        moveVector.z = speed; //only forward  movevemnt

        controller.Move(moveVector * Time.deltaTime); //constant movement with seconds and frames based on fps
	}

    public void SetSpeed(float modifier)
    {
        speed = 5.0f + modifier;
    }


    //being called everytime capsule hits objects
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + 0.1f && hit.gameObject.tag == "enemy")
            Death();
    }

    private void Death()
    {
        isDead = true;
        GetComponent<Score>().OnDeath();
    }
}
