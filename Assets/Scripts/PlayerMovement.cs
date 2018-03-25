using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed = 3f;
    public static bool isChangingElevation;
    public AudioClip[] audioClip;

    Animator anim;
    PersistentData persistentDataScript;

    private bool isMoving = false;
    private bool facingRight = false;
    private Vector3 targetPosition;
    private float timer = 0.0f;
    private float maxTime;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        isChangingElevation = false;
        targetPosition = transform.position;
        persistentDataScript = GameObject.Find("PersistentData").GetComponent<PersistentData>();
        if (persistentDataScript.backToTutorial) {
            gameObject.SetActive(false);
        }
    }

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update() {
        Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();
        float move = Input.GetAxis("Horizontal");
        rigidBody2D.velocity = new Vector2(move * maxSpeed, rigidBody2D.velocity.y);

        //anim.SetFloat("Speed", Mathf.Abs(move));
        /*if (Input.GetMouseButtonDown(0) && persistentDataScript.cannotMove == false) {
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            targetPosition = Camera.main.ScreenToWorldPoint(mousePos);
            targetPosition = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
            isMoving = true;
        }

        if (isMoving && transform.position != targetPosition) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 2.5f);
        }
        else if (isMoving && transform.position == targetPosition) {
            isMoving = false;
        }*/

        if (rigidBody2D.velocity.x != 0) {
            anim.SetInteger("AnimState", 1);
            if (move > 0 && !facingRight) {
                Flip();
            } else if (move < 0 && facingRight) {
                Flip();
            }
        } else {
            if (anim.GetInteger("AnimState") != 0) {
                if (timer >= maxTime) {
                    anim.SetInteger("AnimState", 0);
                } else {
                    timer += Time.deltaTime;
                }
            }
        }
    }

    void Flip() {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public bool isFacingRight() {
        return facingRight;
    }

    public void SetAnimation(int state, float time = 0.5f) {
        maxTime = time;
        anim.SetInteger("AnimState", state);
        timer = 0.0f;
    }

    public void Climb(float rate) {
        gameObject.transform.position += new Vector3(0, rate, 0);
    }

    public void toggleRigidBody() {
        GetComponent<Rigidbody2D>().isKinematic = !GetComponent<Rigidbody2D>().isKinematic;
    }

    public float getY() {
        return gameObject.transform.position.y;
    }

    public Vector3 getPos() {
        return gameObject.transform.position;
    }

    public void PlaySound(int clip) {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0) {
            AudioSource.PlayClipAtPoint(audioClip[clip], transform.position);
        }
    }

}
