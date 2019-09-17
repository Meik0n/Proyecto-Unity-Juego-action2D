using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmbientAudio : MonoBehaviour
{

    private AudioSource source;
    public AudioClip clipAudio;
    private Scene scene;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clipAudio;
        source.Play();
    }

    void Update()
    {

    }
}
