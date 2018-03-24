using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public InteractableObject obj;
    private SpriteRenderer sprite;
    private bool isInteractable = true;

    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public InteractableObject getObject() {
        return obj;
    }

    public void setInteractable(bool interactable) {
        isInteractable = interactable;
    }

    public void setSprite(bool enabled) {
        sprite.enabled = enabled;
        obj.GetComponent<SpriteRenderer>().enabled = isInteractable && enabled;
    }

    void OnMouseDown() {
        if (isInteractable) {
            obj.ClickEvent();
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
       if (isInteractable && col.gameObject.tag == "Player") {
            sprite.enabled = true;
       }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            sprite.enabled = false;
        }
    }
}
