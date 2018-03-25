using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimViaDist : InteractableObject {

    private Animator anim;
    private bool animSet = false;
    private bool playerWithinDist = false;
    private float t = 0.0f;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update () {

    }

    public override void ClickEvent() {

    }

    public override void PlaySound() {

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerWithinDist = true;
            anim.SetInteger("AnimState", 1);
            animSet = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            t = 0.0f;
            playerWithinDist = false;
            anim.SetInteger("AnimState", 0);
        }
    }

}
