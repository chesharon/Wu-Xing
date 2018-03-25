using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    private static MusicManager instance = null;

    public static MusicManager Instance {
        get { return instance; }
    }

    void Awake() {
        if (instance != null && instance != this) {
            this.gameObject.GetComponent<AudioSource>().Stop();
            if (instance.GetComponent<AudioSource>().clip != this.gameObject.GetComponent<AudioSource>().clip) {
                instance.GetComponent<AudioSource>().clip = this.gameObject.GetComponent<AudioSource>().clip;
                instance.GetComponent<AudioSource>().volume = this.gameObject.GetComponent<AudioSource>().volume;
                instance.GetComponent<AudioSource>().Play();
            }
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        this.gameObject.GetComponent<AudioSource>().Play();
        DontDestroyOnLoad(this.gameObject);
    }

}
