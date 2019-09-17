using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{


    public Transform firePoint;
    public GameObject bulletPrefab;
    private float timeToShoot;
    private bool isShooting = false;
    public float shootRate = .25f;
    public float shootAperture = 10;
    public float knockBackForce;
    private Rigidbody2D playerrb;
    private CharacterController2D player;
    private CameraShake shake;
    public AudioClip shootAudio;
    private AudioSource clip;

    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
        player = GetComponentInParent<CharacterController2D>();
        playerrb = GetComponentInParent<Rigidbody2D>();
        clip = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            //Shoot();
            isShooting = true;
        }
        else { isShooting = false; }

        if (isShooting)
        {
            if (Time.fixedTime >= timeToShoot)
            {
                if (Time.timeScale != 0)
                {
                    Shoot();
                    Shake();
                    timeToShoot = Time.fixedTime + shootRate;
                }

            }
        }


        //FaceMouse();
    }

    void Shoot()
    {
        var angles = new Vector3(firePoint.rotation.eulerAngles.x, firePoint.rotation.eulerAngles.y, Random.Range(firePoint.rotation.eulerAngles.z - shootAperture, firePoint.rotation.eulerAngles.z + shootAperture));
        var angles2 = new Vector3(firePoint.rotation.eulerAngles.x, firePoint.rotation.eulerAngles.y, Random.Range(firePoint.rotation.eulerAngles.z - shootAperture + 45, firePoint.rotation.eulerAngles.z + shootAperture));
        var angles3 = new Vector3(firePoint.rotation.eulerAngles.x, firePoint.rotation.eulerAngles.y, Random.Range(firePoint.rotation.eulerAngles.z - shootAperture - 45, firePoint.rotation.eulerAngles.z + shootAperture));

        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(angles));
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(angles2));
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(angles3));
        clip.PlayOneShot(shootAudio);

        //Instantiate(bulletPrefab, firePoint.position, new Quaternion(Random.Range(transform.rotation.x -.3f,transform.rotation.x + .3f ),
        //Random.Range(transform.rotation.y -.3f,transform.rotation.y + .3f ),transform.rotation.z,transform.rotation.w));

        if (player.m_FacingRight)
        {
            playerrb.AddForce(new Vector2(-knockBackForce, 0));
        }
        else if (!player.m_FacingRight)
        {
            playerrb.AddForce(new Vector2(knockBackForce, 0));
        }
    }

    public void Shake()
    {
        shake.shakeDuration = 0.2f;
        shake.shakeAmount = 0.05f;
        shake.decreaseFactor = 1f;
    }
}
