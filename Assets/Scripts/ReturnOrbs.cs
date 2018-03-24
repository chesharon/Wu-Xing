using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnOrbs : InteractableObject {

    public GameObject behindScreen;
    public Sprite[] orbs;
    PersistentData persistentDataScript;

    // Use this for initialization
    void Start () {
        persistentDataScript = GameObject.Find("PersistentData").GetComponent<PersistentData>();

    }

    // Update is called once per frame
    void Update () {

    }

    public override void ClickEvent() {
        Debug.Log(persistentDataScript.TotalOrbs());
        if (persistentDataScript.TotalOrbs() == 1) {
            behindScreen.GetComponent<SpriteRenderer>().sprite = orbs[0];
        } else if (persistentDataScript.TotalOrbs() == 2) {
            behindScreen.GetComponent<SpriteRenderer>().sprite = orbs[1];
        }
    }

    public override void PlaySound() {

    }
}
