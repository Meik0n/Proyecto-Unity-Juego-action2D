using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 100;

    public GameObject deathEffect;
    public GameObject deathEffect2;
    public int ContactDamage = 1;
    protected CameraShake shake;
    private AudioSource source;

    public void Start()
    {
        source = GetComponent<AudioSource>();
        source.Play();
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().LooseLife(ContactDamage);
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Shake();
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect2, transform.position, Quaternion.identity);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Shake()
    {
        shake.shakeDuration = 0.2f;
        shake.shakeAmount = 0.2f;
        shake.decreaseFactor = 1f;
    }
}
