using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : InteractableObject {

    public Item item;
    public Interactable interactable;
    public int animState;
    private Inventory inventory;
    PlayerMovement playerMovementsScript;

    // Use this for initialization
    void Start () {
        inventory = FindObjectOfType<Inventory>();
        item.obj = this.gameObject;
        playerMovementsScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public override void ClickEvent() {
        if (inventory == null) {
            inventory = FindObjectOfType<Inventory>();
        }
        if (!inventory.IsFull()) {
            playerMovementsScript.SetAnimation(animState);
            inventory.AddItem(item); 
            item.isCollected = true;
            interactable.setInteractable(false);
            interactable.setSprite(false);
        }
    }

    public override void PlaySound() {

    }

}
