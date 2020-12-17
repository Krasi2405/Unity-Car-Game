using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MusicPlayer : MonoBehaviour {

    public static MusicPlayer Instance { get; private set; }

    public List<AudioClip> audioClips = new List<AudioClip>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start () {

        AudioClip audioClip = audioClips[SceneManager.GetActiveScene().buildIndex];
        AudioSource.PlayClipAtPoint(audioClip, Vector3.zero);
	}
}
