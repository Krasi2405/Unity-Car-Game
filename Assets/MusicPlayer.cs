using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MusicPlayer : MonoBehaviour {
    
    public List<AudioClip> audioClips = new List<AudioClip>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start () {
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer != this)
        {
            Destroy(musicPlayer);
        }

        AudioClip audioClip = audioClips[SceneManager.GetActiveScene().buildIndex];
        AudioSource.PlayClipAtPoint(audioClip, Vector3.zero);
	}
}
