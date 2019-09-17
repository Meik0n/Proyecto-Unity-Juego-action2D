using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour {

    public GameObject bala;
    public GameObject originShoot;
    private float timeBtwShoots;
    public float startTimeBtwShoots;
    private AudioSource source;
    public AudioClip audioToPlay;

    void Start ()
    {
        source = GetComponent<AudioSource>();
	}
	

	void Update ()
    {
        if (timeBtwShoots <= 0)
        {
            Instantiate(bala, originShoot.transform.position, Quaternion.identity);
            source.PlayOneShot(audioToPlay);
            timeBtwShoots = startTimeBtwShoots;
        }
        else
        {
            timeBtwShoots -= Time.deltaTime;
        }
        
	}
}
