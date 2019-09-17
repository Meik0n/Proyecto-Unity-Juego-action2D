using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformActivatorOnlyGo : MonoBehaviour
{
    private PlatformXOnlyGo platform2;
    private SpriteRenderer sprite;
    public string PlatformTag;

    void Start()
    {
        platform2 = GameObject.FindGameObjectWithTag(PlatformTag).GetComponent<PlatformXOnlyGo>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            platform2.activeMove = true;
            sprite.color = Color.green;
        }
    }
}

