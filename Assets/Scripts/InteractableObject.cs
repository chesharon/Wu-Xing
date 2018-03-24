using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour {

    public abstract void ClickEvent();
    public abstract void PlaySound();
    public Sprite sprite;
}
