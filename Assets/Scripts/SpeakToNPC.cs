using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakToNPC : InteractableObject {

    private Animator anim;
    bool animSet = false;
    bool playerWithinDist = false;
    private float t = 0.0f;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (animSet == true) {
            t += Time.deltaTime;
            //Debug.Log("TIME: t = " + t);
            if (t >= 2.0f) {
                anim.SetBool("Play", false);
                animSet = false;
            }
        }
    }

    public override void ClickEvent() {
        if (playerWithinDist == true) {
            t = 0.0f;
            anim.SetBool("Play", true);
            animSet = true;
        }
    }

    public override void PlaySound() {

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerWithinDist = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerWithinDist = false;
        }
    }
}
