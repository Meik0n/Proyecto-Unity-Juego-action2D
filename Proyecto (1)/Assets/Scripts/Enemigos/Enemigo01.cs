using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo01 : Enemy {

    private bool facingRight = false;
    public float Speed;
    public float ChaseSpeed;
    public float jumpForce = 1000f;
    public float jumpAngles = 45f;
    public float distanceToFollow = 30f;
    public float distanceToTurn;
    public float distanceToAttack = 3f;
    public LayerMask obstacles;
    public LayerMask player;
   // public LayerMask enemigos;
    public Transform detector;
    private Transform target;
    public float radioDeEscucha;


    private Rigidbody2D rb;

    [Header("Scale on attack")]
    public float scaleFactorOnAttack = 1;
    protected Vector3 initialScale;
    public float attackScaleTime = 1f;
    public AnimationCurve growAnim = new AnimationCurve(new Keyframe(0,0), new Keyframe(0.5f,1), new Keyframe(1,0));

    protected enum states
    {
        regular, growing, chasing, charging, jumping
    }
    protected states currentState = states.regular;

    void  Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
        initialScale = transform.localScale;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Regular());
    }


    void Update()
    {
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().LooseLife(ContactDamage);
        }
    }

    protected IEnumerator Grow()
    {
        //Debug.Log(gameObject.name+" Grow started");
        currentState = states.growing;
        var maxScale = initialScale;
        maxScale.x = initialScale.x * scaleFactorOnAttack;

        var accumTime = 0.0f;
        while (accumTime <= 1)
        {
            accumTime += Time.deltaTime / attackScaleTime;

            transform.localScale = Vector3.Lerp(initialScale, maxScale, growAnim.Evaluate(accumTime));

            yield return null;
        }
        transform.localScale = initialScale;
        StartCoroutine(Chase());
      
    }  
    
    protected IEnumerator Chase()
    {
        yield return null;
        //Debug.Log(gameObject.name + " Chase started");
        currentState = states.chasing;

        while(currentState == states.chasing)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), ChaseSpeed * Time.deltaTime);

            if (target.position.x > transform.position.x)
            {
                if (!facingRight)
                {
                    Flip();
                }
            }
            else if (target.position.x < transform.position.x)
            {
                if (facingRight)
                {
                    Flip();
                }
            }
            if(target.position.y > transform.position.y + 7 || target.position.y < transform.position.y - 7)
            {
                StartCoroutine(Regular());
                yield break;
            }

            if (facingRight)
            {
                if (Physics2D.Raycast(detector.position, Vector2.right, distanceToTurn, obstacles))
                {
                    Flip();
                    StartCoroutine(Regular());
                    yield break;
                }
            }
            else if (!facingRight)
            {
                if (Physics2D.Raycast(detector.position, Vector2.left, distanceToTurn, obstacles))
                {
                    Flip();
                    StartCoroutine(Regular());
                    yield break;
                }
            }

            transform.Translate(Vector2.left * Speed * Time.deltaTime);
            RaycastHit2D groundDetector = Physics2D.Raycast(detector.position, Vector2.down, distanceToTurn, obstacles);
            if (!groundDetector)
            {
                StartCoroutine(jump());
                
                /* 
                Flip();
                StartCoroutine(Regular());
                */
            }
            if ((Vector2.Distance(transform.position, target.position) < distanceToAttack) && groundDetector)
            {
                StartCoroutine(Grow());
                yield break;
            }
            yield return null;
        }       
    }


    protected IEnumerator jump()
    {
        Debug.Log("ENEMIGO: entro al salto");
        currentState = states.jumping;
        if (facingRight)
        {
            rb.AddForce(new Vector2(Mathf.Cos(jumpAngles * (Mathf.PI/180)), Mathf.Sin(jumpAngles * (Mathf.PI / 180))) * jumpForce, ForceMode2D.Impulse);
        }
        else if (!facingRight)
        {
            rb.AddForce(new Vector2(-Mathf.Cos(jumpAngles * (Mathf.PI / 180)), Mathf.Sin(jumpAngles * (Mathf.PI / 180))) * jumpForce, ForceMode2D.Impulse);
            
        }
        yield return new WaitForSeconds(0.5f);


        while (currentState == states.jumping)
        {
            RaycastHit2D groundDetector = Physics2D.Raycast(detector.position, Vector2.down, distanceToTurn, obstacles);
            

            if (groundDetector)
            {
                StartCoroutine(Regular());
            }
            yield return null;
        }
    }

    protected IEnumerator Regular()
    {
        currentState = states.regular;
        yield return null;
        
       // Debug.Log(gameObject.name + " Regular started");
        while (currentState == states.regular)
        {
            // transform.Translate(Vector2.left * Speed * Time.deltaTime);
            RaycastHit2D groundDetector = Physics2D.Raycast(detector.position, Vector2.down, distanceToTurn, obstacles);
            Collider2D escucha = Physics2D.OverlapCircle(transform.position, radioDeEscucha, player);

            if (!groundDetector)
            {
               // Debug.Log("Giro sin suelo");
                Flip();
            }

            if (facingRight)
            {
                if (Physics2D.Raycast(detector.position, Vector2.right, distanceToTurn, obstacles))
                {
                    Flip();
                }
                if (Physics2D.Raycast(detector.position, Vector2.right, distanceToFollow, player))
                {
                    StartCoroutine(Charging());
                    yield break;
                }
            }
            else if (!facingRight)
            {
                if (Physics2D.Raycast(detector.position, Vector2.left, distanceToTurn, obstacles))
                {
                    Flip();
                }

                if (Physics2D.Raycast(detector.position, Vector2.left, distanceToFollow, player))
                {
                   StartCoroutine(Charging());
                    yield break;
                }

                /*
                var distance = target.position - transform.position;
                if (distance.sqrMagnitude < distanceToFollow * distanceToFollow && Mathf.Abs(distance.y) < 1)
                {
                    var hit = Physics2D.Raycast(detector.position, (target.position - transform.position), distanceToFollow, obstacles);
                    if (!hit)
                    {
                        StartCoroutine(Chase());
                    }
                    else
                    {
                        Debug.Log("Collision against: " + hit.transform.name);
                    }
                }
                */
            }

            if (escucha)
            {
                StartCoroutine(Charging());
                yield break;
            }

            GetComponent<Rigidbody2D>().MovePosition(transform.position + (facingRight ? Vector3.right : Vector3.left) * Speed * Time.deltaTime);
            yield return null;
        }
    }

    [Header("Warning particles")]
    public ParticleSystem warningParticles;
    //public float chargeTime = 1;
    protected IEnumerator Charging()
    {
        currentState = states.charging;
        yield return null;
       // int counter = 1;

        if (warningParticles == null)
        {
            yield break;
        }
        warningParticles.Play();

        yield return new WaitForSeconds(warningParticles.main.duration);

        while(warningParticles.particleCount>0)
        {
            yield return null;


            //if(counter == 10)
            //{
            //    StartCoroutine(Chase());    
            //    yield break;    
            //}
            //else if(counter%2 != 0)
            //{
            //    transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
            //    counter++;
            //    yield return new WaitForSeconds(.01f);
            //}    
            //else if(counter%2 == 0)
            //{
            //    transform.position = new Vector3(transform.position.x -1f, transform.position.y, transform.position.z);
            //    counter++;
            //    yield return new WaitForSeconds(.01f);
            //}     


        }

        StartCoroutine(Chase());
       
    }
         
    void Flip()
    {
       // Debug.Log(gameObject.name + " Flip started");
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(detector.position, Vector2.down * distanceToTurn);
        if (facingRight)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(detector.position, Vector2.right * distanceToTurn);

            Gizmos.color = Color.white;
            Gizmos.DrawRay(new Vector2(detector.position.x, detector.position.y + 0.5f), Vector2.right * distanceToFollow);
        }
        else if (!facingRight)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(detector.position, Vector2.left * distanceToTurn);


            Gizmos.color = Color.white;
            // Gizmos.DrawRay(new Vector2(detector.position.x, detector.position.y + 0.5f), Vector2.left * distanceToFollow);

            if (target != null)
            {
                Gizmos.DrawRay(detector.position, (target.position - detector.position).normalized * distanceToFollow);
            }
        }

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, radioDeEscucha);
    }
}