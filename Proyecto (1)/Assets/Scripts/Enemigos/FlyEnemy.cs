using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy {

    private bool facingRight = false;
    public GameObject Projectile;
    public Transform firePoint;
    private float timeBtwShoots;
    public float startTimeBtwShoots;
    private Transform target;
    public float RadioDeteccion;
    public float timeToWalk = 6f;
    public float Speed;
    public float timeToStop = 3f;
    private Vector2 originalPos;
    public LayerMask obstacles;
    public LayerMask player;
    public AudioClip audioToPlay;
    private AudioSource clip;

    protected enum states
    {
        patrolling, stopping ,attacking, running
    }protected states currentState = states.patrolling;

    void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timeBtwShoots = startTimeBtwShoots;
        originalPos = transform.position;
        StartCoroutine(Patrol());
        clip = GetComponent<AudioSource>();
	}
	

	void Update ()
    {

        var distance = target.position-transform.position;

        rendererToDisable.SetActive(Mathf.Abs(distance.x)<renderDistanceX && Mathf.Abs(distance.y)<renderDistanceX);
        
    }

    protected IEnumerator Patrol()
    {
        currentState = states.patrolling;
        float walkingTime = 0.0f;
        //transform.position = Vector2.MoveTowards(transform.position, originalPos, Speed * Time.deltaTime);

        while(currentState == states.patrolling)
        {
            Collider2D detector = Physics2D.OverlapCircle(transform.position, RadioDeteccion, player);
            if (walkingTime >= timeToWalk)
            {
                StartCoroutine(Stop());
            }
            walkingTime += Time.deltaTime;
            transform.Translate(Vector2.left * Speed * Time.deltaTime);

            if (detector)
            {
                StartCoroutine(Attack());
            }

            if (facingRight)
            {
                if (Physics2D.Raycast(transform.position, Vector2.right, 1.5f, obstacles))
                {
                    Flip();
                }
            }
            else if (!facingRight)
            {
                if (Physics2D.Raycast(transform.position, Vector2.left, 1.5f, obstacles))
                {
                    Flip();
                }
            }
            yield return null;
        }
    }

    protected IEnumerator Stop()
    {
        currentState = states.stopping;
        float timeStopping = 0f;
        float timeCounter = 0f;

        while(currentState == states.stopping)
        {
            Collider2D detector = Physics2D.OverlapCircle(transform.position, RadioDeteccion, player);
            if (timeStopping >= timeToStop)
            {
                StartCoroutine(Patrol());
            }
            timeStopping += Time.deltaTime;

            timeCounter += Time.deltaTime * Speed;
            float x = Mathf.Cos(timeCounter)/10;
            float y = Mathf.Sin(timeCounter)/10;

            transform.Translate(new Vector2(x, y));


            if (detector)
            {
                StartCoroutine(Attack());
            }
            yield return null;
        }
        Flip();
    }

    protected IEnumerator Attack()
    {
        currentState = states.attacking;

        while(currentState == states.attacking)
        {
            if (timeBtwShoots <= 0)
            {
                Instantiate(Projectile, firePoint.position, Quaternion.identity);
                clip.PlayOneShot(audioToPlay);
                timeBtwShoots = startTimeBtwShoots;
            }
            else
            {
                timeBtwShoots -= Time.deltaTime;
            }
            
            if(Vector2.Distance(transform.position, target.position) > RadioDeteccion)
            {
               
                transform.position = Vector2.MoveTowards(transform.position, target.position, Speed*Time.deltaTime);
            }
            
            if(Vector2.Distance(transform.position, target.position) > (RadioDeteccion + 10))
            {
                StartCoroutine(Patrol());
            }
            yield return null;
        }
    }

    protected IEnumerator Run()
    {
        yield return null;
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadioDeteccion);

        if (facingRight)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector2.right * 1.5f);

        }
        else if (!facingRight)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector2.left * 1.5f);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position,new Vector3( renderDistanceX,renderDistanceY,1));
    }


    [Header("Apaño para mejorar rendimiento")]
    public float renderDistanceX = 25;
    public float renderDistanceY = 25;
    public GameObject rendererToDisable;
}
