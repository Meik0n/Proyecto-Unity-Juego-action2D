using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour {

    public float speed;
    public int damage;
    public int x;
    public int y;
    private Vector2 DirectionShoot;
    private Rigidbody2D rb;
    private AudioSource sonido;
    //public AudioClip sond;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        DirectionShoot = new Vector2(x, y);
        sonido = GetComponent<AudioSource>();
        sonido.Play();
    }

    private void Update()
    {
        rb.velocity += DirectionShoot * speed *Time.deltaTime;
      
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerController player = hitInfo.GetComponent<PlayerController>();
        if (hitInfo.tag == "Player")
        {
            player.LooseLife(damage);
            Destroy(gameObject);
        }

        if (hitInfo.tag != "Enemy" && hitInfo.tag != "PlayerBullet" && hitInfo.tag != "TorretaBala")
        {
            Destroy(gameObject);
        }

    }
}
