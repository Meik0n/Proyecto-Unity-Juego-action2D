using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 20f;
    public int damage = 30;
    [HideInInspector]public Rigidbody2D rb;
    public GameObject impactEffect;
    public float DestroyTime = 1f;
    // public GameObject bloodEffect;

   // private CameraShake shake;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, DestroyTime); //pa marco con amor
       // shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        
        if(hitInfo.tag != "Player")
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
           // Shake();
            Destroy(gameObject);
        }

        //if(hitInfo.tag == "Enemy")
        //{
        //    Instantiate(bloodEffect, transform.position, Quaternion.identity);
        //}
        
    }
    /*
    private void Shake()
    {
        shake.shakeDuration = 0.2f;
        shake.shakeAmount = 0.05f;
        shake.decreaseFactor = 1f;
    }
    */
}
