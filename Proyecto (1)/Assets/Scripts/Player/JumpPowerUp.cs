using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour {

    //private PlayerController player;
    private AudioSource clip;
    public AudioClip audioToPlay;

	void Start ()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        clip = GetComponent<AudioSource>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            player.jumpPowerup = true;
            Destroy(gameObject,.3f);
            clip.PlayOneShot(audioToPlay);
        }
    }
}
