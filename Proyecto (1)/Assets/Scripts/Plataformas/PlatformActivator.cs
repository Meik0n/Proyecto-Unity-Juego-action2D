using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformActivator : MonoBehaviour
{

    private PlatformX platform;
    private SpriteRenderer sprite;
    public string PlatformTag;
    private AudioSource source;
    public AudioClip soundToPlay;
    void Start()
    {
        platform = GameObject.FindGameObjectWithTag(PlatformTag).GetComponent<PlatformX>();
        sprite = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            platform.activeMove = true;
            sprite.color = Color.green;
            source.PlayOneShot(soundToPlay);
        }
    }
}
