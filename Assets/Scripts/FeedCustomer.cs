using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedCustomer : InteractableObject
{
    public Sprite[] Eat;
    public Sprite[] Point;
    public Sprite[] Angry;
    public int animState;

    PlayerMovement playerMovementsScript;
    PersistentData persistentDataScript;

    private Inventory inventory;
    private int foodCount = 0;

    // Use this for initialization
    void Start() {
        inventory = FindObjectOfType<Inventory>();
        playerMovementsScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        persistentDataScript = GameObject.Find("PersistentData").GetComponent<PersistentData>();
    }

    // Update is called once per frame
    void Update() {

    }

    public override void ClickEvent() {
        if (inventory == null) {
            inventory = FindObjectOfType<Inventory>();
        }

        if (!inventory.IsEmpty()) {
            if (inventory.PeekFirstItem().isBroccoli) {
                persistentDataScript.fedBroccoli = true;
                setSprite(Angry);
            } else {
                setSprite(Eat);
            }
            inventory.RemoveFirstFoodItem();
            playerMovementsScript.SetAnimation(animState);
            foodCount++;
        } else {
            setSprite(Point);
        }
    }
    
    void setSprite(Sprite[] sprites) {
        if (foodCount == 0) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
        } else if (foodCount <= 2) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
        } else if (foodCount > 2 && foodCount <= 4) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
        } else if (foodCount > 4 && foodCount <= 6) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
        } else if (foodCount > 6 && foodCount <= 8) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[4];
        }
    }

    public override void PlaySound() {

    }
}
