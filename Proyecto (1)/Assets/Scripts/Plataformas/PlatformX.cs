using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformX : MonoBehaviour {

    public float Speed=5f;
    private  Vector3 originalPos;
    public GameObject target;
    [HideInInspector]public bool movingToTarget = true;
    [HideInInspector] public bool activeMove = false;
    private AudioSource source;
    public AudioClip audioToPlay;

    private void Start()
    {
        originalPos = transform.position;
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (activeMove)
        {
            source.PlayOneShot(audioToPlay);
            if (movingToTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
            }
            else if (!movingToTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPos, Speed * Time.deltaTime);
            }

            if (transform.position == originalPos)
            {
                movingToTarget = true;

            }
            else if (transform.position == target.transform.position)
            {
                movingToTarget = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Blood")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
