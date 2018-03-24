using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScreen : MonoBehaviour {

    public Transform tf;
    public AudioClip clip;
    public GameObject behindScreen;
    public Sprite orbs;
    private Animator anim;
    private bool animSet = false;
    private bool move = false;
    private Vector2 startPosition;
    private Vector2 target;
    float t;
    float timeToReachTarget;
    PersistentData persistentDataScript;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        startPosition = target = tf.position;
        persistentDataScript = GameObject.Find("PersistentData").GetComponent<PersistentData>();
        if (persistentDataScript.TotalOrbs() == 2) {
            behindScreen.GetComponent<SpriteRenderer>().sprite = orbs;
        }
    }

    // Update is called once per frame
    void Update() {
        if (animSet == true) {
             t += Time.deltaTime / timeToReachTarget;
             tf.position = Vector3.Lerp(startPosition, target, t);
        }
    }

    public void SetAnimation(bool state)
    {
        anim.SetBool("Play", state);
        animSet = true;
    }

    public void Move(int direction, float time = 1.5f) {
        t = 0;
        startPosition = tf.position;
        timeToReachTarget = time;
        if (direction == 0) {
            target = new Vector2(-7.09f, tf.position.y);
        }
        else {
            target = new Vector2(-0.39f, tf.position.y);
        }
    }

    public void PlaySound() {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

}
