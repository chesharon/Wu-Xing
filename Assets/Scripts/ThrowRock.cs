using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRock : InteractableObject {

    public Item item;
    public Interactable interactable;
    public Interactable farmerInteractable;
    public Interactable farmerBubbleInteractable;
    public GameObject target;
    public Animator animator;

    PlayerMovement playerMovementsScript;
    PersistentData persistentDataScript;

    [SerializeField]
    private Collider2D collider;
    [SerializeField]
    private Collider2D farmerCollider;

    private Inventory inventory;
    private bool canThrow = false;
    private Vector3 lastPos;
    private float angle = 45.0f;
    private float gravity = 9.8f;
    private float movementTime = 0.95f;
    private float speed = 10.0f;
    private float t = 0.0f;

    // Use this for initialization
    void Start () {
        inventory = FindObjectOfType<Inventory>();
        persistentDataScript = GameObject.Find("PersistentData").GetComponent<PersistentData>();
        playerMovementsScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update () {
        if (persistentDataScript.threwRockAtFarmer) {
            if (collider) {
                collider.enabled = false;
            }
            farmerInteractable.setInteractable(false);
            farmerBubbleInteractable.setInteractable(false);
            animator.SetInteger("AnimState", 3);

            transform.position = persistentDataScript.rockLastPos;
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, persistentDataScript.rockLastPos.z);
            interactable.setInteractable(false);
            canThrow = false;
        }
        else if (canThrow) {
            if (collider) {
                collider.enabled = false;
            }
            farmerInteractable.setInteractable(false);
            farmerBubbleInteractable.setInteractable(false);

            t += Time.deltaTime;

            if (t <= movementTime) {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position + new Vector3(-0.75f, 0.5f, 0.0f), speed * Time.deltaTime);
                float randRot = Random.Range(0.8f, 1.2f);
                transform.Rotate(0.0f, 0.0f, randRot);
                persistentDataScript.rockRotation = randRot;
            } else {
                if (t <= movementTime + 0.45f) {
                    Vector3 targetPos = target.transform.position + new Vector3(Random.Range(-1.30f, -1.20f), -1.1f, 0.0f);
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                    persistentDataScript.rockLastPos = transform.position;
                    if (transform.position == targetPos) {
                        animator.SetInteger("AnimState", 2);
                        persistentDataScript.threwRockAtFarmer = true;
                    }
                } 
            }
        }
    }

    public override void ClickEvent() {
        if (inventory == null) {
            inventory = FindObjectOfType<Inventory>();
        }

        if (item.isCollected) {
            inventory.RemoveItem(item);
            transform.position = playerMovementsScript.getPos();
            interactable.setSprite(true);
            canThrow = true;
        }
    }

    public override void PlaySound() {

    }
}
