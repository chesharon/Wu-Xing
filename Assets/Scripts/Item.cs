using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject {

    public Sprite sprite;
    public GameObject obj;
    public bool isCollected;
    public bool isFood;
    public bool isBroccoli;

}
