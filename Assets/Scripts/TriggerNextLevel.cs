using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerNextLevel : InteractableObject {

    public GameObject child;
    public GameObject cave;
    public string currentSceneName;
    public float xPos, yPos;

    PlayerMovement playerMovementsScript;
    PersistentData persistentDataScript;

    private Inventory inventory;
    private string nextSceneName = null;
    private bool playerWithinDist = false;
    private float t = 0.0f;

    // Use this for initialization
    void Start () {
        inventory = FindObjectOfType<Inventory>();
        playerMovementsScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        persistentDataScript = GameObject.Find("PersistentData").GetComponent<PersistentData>();
        persistentDataScript.currentScene = currentSceneName;
        StartCoroutine(Wait());
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(5);
    }

    // Update is called once per frame
    void Update () {
        t += Time.deltaTime;
        if (t >= 2.0f) {
            if (persistentDataScript.currentScene == "Tutorial_Level") {
                playerMovementsScript.PlaySound(0);
            } else if (persistentDataScript.currentScene == "Water_Level_Outside_Cave") {
                playerMovementsScript.PlaySound(1);
            }
            t = 0.0f;
        }

        if (persistentDataScript.currentScene == "Fire_Level" && persistentDataScript.fedBroccoli) {
            persistentDataScript.currentScene = nextSceneName = "Tutorial_Level";
            Wait();
            inventory.ClearInventory();
            persistentDataScript.AddOrbs("Fire");

            persistentDataScript.fedBroccoli = false;
            persistentDataScript.backToTutorial = true;
            SceneManager.LoadSceneAsync(nextSceneName);
            playerMovementsScript.transform.position = new Vector2(xPos, yPos + -4.86f);
        }
    }

    public override void ClickEvent() {
        if (playerWithinDist) {
            if (inventory == null) {
                inventory = FindObjectOfType<Inventory>();
            }

            if (persistentDataScript.currentScene == "Tutorial_Level" && persistentDataScript.backToTutorial == false) {
                persistentDataScript.currentScene = nextSceneName = "Water_Level_Outside_Cave";
            } else if (persistentDataScript.currentScene == "Water_Level_Inside_Cave" && this.gameObject == cave) {
                persistentDataScript.currentScene = nextSceneName = "Water_Level_Outside_Cave";
            } else if (persistentDataScript.currentScene == "Water_Level_Outside_Cave" && this.gameObject == cave) {
                persistentDataScript.currentScene = nextSceneName = "Water_Level_Inside_Cave";
            } else if (persistentDataScript.collectedBunny && this.gameObject == child) {
                persistentDataScript.currentScene = nextSceneName = "Fire_Level";
                inventory.ClearInventory();
                persistentDataScript.AddOrbs("Water");
            }

            if (nextSceneName != null) {
                SceneManager.LoadSceneAsync(nextSceneName);
                playerMovementsScript.transform.position = new Vector2(xPos, yPos + -4.86f);
            }
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
