using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour {

    public Interactable teleportationStone;
    private static List<string> orbs = new List<string>();
    public bool collectedCarrot = false;
    public bool collectedBunny = true;
    public bool freedBunny = false;
    public Vector3 bunnyLastPos;
    public Vector3 bunnyRandTargetPos;
    public bool threwRockAtFarmer = false;
    public Vector3 rockLastPos;
    public float rockRotation;
    public bool cannotMove = false;
    public bool fedBroccoli = false;
    public bool backToTutorial = false;
    public string currentScene = null;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddOrbs(string orb) {
        orbs.Add(orb);
    }

    public int TotalOrbs() {
        Debug.Log("ORB COUNT: " + orbs.Count);
        return orbs.Count;
    }


}
