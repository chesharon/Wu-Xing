using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public void Start() {
        StartCoroutine(Loader());
    }

    protected IEnumerator Loader() {
        Application.LoadLevelAdditive("Persistent");
        yield return null;
        GameObject uiRoot = GameObject.FindGameObjectWithTag("UI");
        DontDestroyOnLoad(uiRoot);
    }

}
