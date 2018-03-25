using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPress : InteractableObject {

    public float topElevation, bottomElevation;
    public float climbTime;

    //Animator anim;
    //Rigidbody2D playerRigidBody;

    [SerializeField]
    private Collider2D platformCollider;

    private PlayerMovement playerMovementsScript;
    private float climbSpeed = 3f;
    private bool isClimbing = false;
    private bool playerWithinDist = false;
    private bool isUp = true;

    // Use this for initialization
    void Start () {
        //anim = GetComponent<Animator>();
        //playerRigidBody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        playerMovementsScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
	
	// Update is called once per frame
    void Update () {
        if (isClimbing == true) {
            if (isUp) {
                playerMovementsScript.Climb(climbSpeed * Time.deltaTime);
                isClimbing = playerMovementsScript.getY() <= topElevation;
                if (isClimbing == false) {
                    playerMovementsScript.toggleRigidBody();
                }
            } else {
                playerMovementsScript.Climb(-climbSpeed * Time.deltaTime);
                isClimbing = playerMovementsScript.getY() >= bottomElevation;
                if (isClimbing == false) {
                    playerMovementsScript.toggleRigidBody();
                }
            }
        }
    }

    /*void OnMouseDown() {
        if (playerWithinDist == true) {
            pressed = !pressed;
            Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();
            if (playerMovementsScript.isFacingRight() == true) {
                rigidBody2D.velocity = new Vector2(1.0f * maxSpeed, rigidBody2D.velocity.y);
            }
            else {
                rigidBody2D.velocity = new Vector2(-1.0f * maxSpeed, rigidBody2D.velocity.y);
            }
            playerMovementsScript.SetAnimation(3, time:1.0f);
        }
    }*/

    public override void ClickEvent() {
        if (playerWithinDist == true) {
            isUp = playerMovementsScript.getY() < (topElevation - bottomElevation) / 2.0f + bottomElevation;
            isClimbing = !isClimbing;
            playerMovementsScript.toggleRigidBody();
            playerMovementsScript.SetAnimation(3, time: climbTime);
        }
    }

    public override void PlaySound() {

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerWithinDist = true;
            //Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), platformCollider, false);
        }
    }

    /*void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player") && other.transform.position.y < -1.0f) {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, climbSpeed);
        }
       if (other.CompareTag("Player") && other.transform.position.y > -1.0f) {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -climbSpeed);
        }
        //Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), platformCollider, false);
    }*/

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isClimbing = false;
            playerWithinDist = false;
            //Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), platformCollider, false);
        }
    }

}
