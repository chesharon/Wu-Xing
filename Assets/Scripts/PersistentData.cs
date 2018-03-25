using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour {

    public Interactable teleportationStone;
    public bool collectedCarrot = false;
    public bool collectedBunny = true;
    public bool freedBunny = false;
    public bool threwRockAtFarmer = false;
    public bool cannotMove = false;
    public bool fedBroccoli = false;
    public bool backToTutorial = false;
    public Vector3 bunnyLastPos;
    public Vector3 bunnyRandTargetPos;
    public Vector3 rockLastPos;
    public float rockRotation;
    public string currentScene = null;

    private static List<string> orbs = new List<string>();

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddOrbs(string orb) {
        orbs.Add(orb);
    }

    public int TotalOrbs() {
        return orbs.Count;
    }

}
