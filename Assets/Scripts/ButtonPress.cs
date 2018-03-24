using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : InteractableObject {

    Animator anim;
    public Interactable teleportationStone;
    PersistentData persistentDataScript;
    PlayerMovement playerMovementsScript;
    PlayScreen screenScript;
    public AudioClip audioClip;
    bool pressed = false;
    bool playerWithinDist = false;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        playerMovementsScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        screenScript = GameObject.Find("Screen").GetComponent<PlayScreen>();
        persistentDataScript = GameObject.Find("PersistentData").GetComponent<PersistentData>();
        teleportationStone.setInteractable(false);
    }

    // Update is called once per frame
    void Update () {

    }

    public override void ClickEvent() {
        if (playerWithinDist == true) {
            if (pressed == false) {
                anim.SetBool("Pressed", true);
                screenScript.SetAnimation(true);
                PlaySound();
                screenScript.Move(0);
                screenScript.PlaySound();
                teleportationStone.setInteractable(true);
            }
            else {
                anim.SetBool("Pressed", false);
                screenScript.SetAnimation(false);
                PlaySound();
                screenScript.Move(1);
                screenScript.PlaySound();
                teleportationStone.setInteractable(false);
            }
            pressed = !pressed;
            playerMovementsScript.SetAnimation(2);
        }
    }

    public override void PlaySound() {
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            playerWithinDist = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  {
            playerWithinDist = false;
        }
    }
}
