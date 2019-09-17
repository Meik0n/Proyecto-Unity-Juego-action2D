using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudioExplosion : MonoBehaviour {

    private AudioSource clip;
    public AudioClip soundToPlay;

	void Start ()
    {
        clip = GetComponent<AudioSource>();
        clip.PlayOneShot(soundToPlay);
	}
	

	void Update ()
    {
		
	}
}
