using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeBunny : InteractableObject {

    public Item carrot;
    public Item bunny;
    public Interactable interactable;

    PlayerMovement playerMovementScript;
    PersistentData persistentDataScript;
    private Animator anim;
    private Inventory inventory;
    private bool playerWithinDist = false;
    private bool isFree = false;
    private bool facingRight = false;
    private float t = 0.0f;
    private float movementTime = 3.5f;
    private float speed = 2.0f;
    private Vector3 randTargetPos;


    // Use this for initialization
    void Start () {
        inventory = FindObjectOfType<Inventory>();
        persistentDataScript = GameObject.Find("PersistentData").GetComponent<PersistentData>();
        anim = GetComponent<Animator>();
        playerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update () {
        if (persistentDataScript.collectedBunny) {
            interactable.setInteractable(false);
            interactable.setSprite(false);
        }

        if (persistentDataScript.freedBunny && !persistentDataScript.collectedBunny) {
            t += Time.deltaTime;
            transform.position = persistentDataScript.bunnyLastPos;
            Move();
        }
	}

    public void Move() {
        if (persistentDataScript.bunnyRandTargetPos.x < transform.position.x && facingRight) {
            Flip();
        } else if (persistentDataScript.bunnyRandTargetPos.x > transform.position.x && !facingRight) {
            Flip();
        }

        if (t <= movementTime) {
            transform.position = Vector3.MoveTowards(transform.position, persistentDataScript.bunnyRandTargetPos, speed * Time.deltaTime);
            persistentDataScript.bunnyLastPos = transform.position;
            if (transform.position != persistentDataScript.bunnyRandTargetPos) {
                anim.SetInteger("AnimState", 1); }
            else {
                anim.SetInteger("AnimState", 2);
            }
        }  else {
            t = 0.0f;
            persistentDataScript.bunnyRandTargetPos = new Vector3(Random.Range(-11.0f, 11.0f), transform.position.y, 0.0f);
        }
    }
    public override void ClickEvent() {
        if (inventory == null) {
            inventory = FindObjectOfType<Inventory>();
        }

        if (playerWithinDist && persistentDataScript.freedBunny == false) {
            transform.position = new Vector3(transform.position.x, -2.9f, 0.0f);
            persistentDataScript.bunnyLastPos = transform.position;
            persistentDataScript.bunnyRandTargetPos = new Vector3(Random.Range(-11.0f, 11.0f), transform.position.y, 0.0f);
            playerMovementScript.SetAnimation(4, 0.5f);
            anim.SetInteger("AnimState", 1);
            persistentDataScript.freedBunny = true;
        } else if (playerWithinDist && persistentDataScript.freedBunny) {
            if (carrot.isCollected) {
                Debug.Log("carrot is collected: " + carrot.isCollected);
                persistentDataScript.collectedCarrot = true;
                inventory.RemoveItem(carrot);
                if (!inventory.IsFull()) {
                    anim.enabled = false;
                    playerMovementScript.SetAnimation(4);
                    inventory.AddItem(bunny);
                    persistentDataScript.collectedBunny = true;
                    interactable.setInteractable(false);
                    interactable.setSprite(false);
                }
            }
        }
    }

    public override void PlaySound() {

    }

    void Flip() {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            playerWithinDist = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            playerWithinDist = false;
        }
    }
}
