using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public CharacterController2D Controller;
    private float horizontalMove = 0f;
    public float runSpeed = 40f;
    private bool jump;
    [HideInInspector] public bool doubleJump = false;
    //private bool crouch = false;
    public Transform firePoint;
    //public GameObject bulletPrefab;
    private Animator anim;

    //vida
    [Header("Vida")]
    public Image healthbar;
    // public List<GameObject> Heart = new List<GameObject>();
    //public GameObject HeartPrefab;
    public float MaxLife = 8;
    public float CurrentLife;
    [HideInInspector] public bool Invulnerability = false;
    private float BlinkingTime = 0;

    //better jump
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    Rigidbody2D rb;

    public bool jumpPowerup = false;

    protected float lastTimeHit = Mathf.NegativeInfinity;

    [Header("Audio")]
    private AudioSource clip;
    public AudioClip audioToPlayOnHit;
    public AudioClip audioToPlayOnDeath;
    private Scene escena;


    public IEnumerator StartInvulnerability()
    {
        Invulnerability = true;
        yield return new WaitForSeconds(1);
        Invulnerability = false;
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        CurrentLife = MaxLife;
        clip = GetComponent<AudioSource>();
        // FillLife();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            if (!jumpPowerup)
            {
                if (gameObject.GetComponent<CharacterController2D>().m_Grounded)
                {
                    jump = true;
                }
            }
            if (jumpPowerup)
            {
                if (gameObject.GetComponent<CharacterController2D>().m_Grounded)
                {
                    jump = true;
                    doubleJump = true;
                }
                else if (doubleJump)
                {
                    jump = true;
                    doubleJump = false;
                }
            }
        }
        /*
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        */

        if (Invulnerability)
        {
            if (Time.fixedTime >= BlinkingTime)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                BlinkingTime = Time.fixedTime + 0.10f;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if (Invulnerability == false)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (rb.velocity.y < 0) //caes mas rápido de lo que subes
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) //si mantienes pulsado saltas un poquico más
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        SetAnimationState();
        SetHealthColor();
    }

    void FixedUpdate()
    {
        Controller.Move(horizontalMove * Time.fixedDeltaTime,/* crouch,*/ jump);
        jump = false;
    }

    /* 
        private void FillLife()
        {
            for (var i = 0; i < MaxLife; i++)
            { 
                var newHeart = Instantiate(HeartPrefab,Vector3.zero, Quaternion.identity);
                newHeart.transform.SetParent(GameObject.FindGameObjectWithTag("Healthbar").transform, false);
                Heart.Add(newHeart);         
            }
        }
    */
    public void LooseLife(int LifeToLoose)
    {
        if (Time.time - lastTimeHit > 1)
        {
            lastTimeHit = Time.time;
            if (CurrentLife - LifeToLoose > 0)
            {
                CurrentLife -= LifeToLoose;

                /* 
                for (var i = 0; i < LifeToLoose; i++)
                {
                    Destroy(Heart.Last());
                    Heart.Remove(Heart.Last());
                }
                */
                clip.PlayOneShot(audioToPlayOnHit);
                StartCoroutine(StartInvulnerability());
            }
            else
            {
                CurrentLife = 0;
                /* 
                foreach (GameObject c in Heart)
                {
                    Destroy(c);
                }
                Heart.Clear();
                */
            }
        }

        if (CurrentLife <= 0)
        {
            clip.PlayOneShot(audioToPlayOnDeath);
            StartCoroutine(RestartGame());
            // Destroy(gameObject);
        }
        healthbar.fillAmount = (CurrentLife / MaxLife);
        // SetAnimationState();

    }

    private void SetAnimationState()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {

                anim.SetBool("Correr", true);
            }
            else
            {
                anim.SetBool("Correr", false);
                //Debug.Log("Estoy en idle");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {

                anim.SetBool("Jump", true);
            }
            else
            {
                anim.SetBool("Jump", false);
                //Debug.Log("Estoy en idle");
            }
            if (rb.velocity.y < 0)
            {

                anim.SetBool("Caer", true);

            }
            else
            {
                anim.SetBool("Caer", false);
                // Debug.Log("Estoy en idle");
            }
        }
    }

    private void SetHealthColor()
    {
        if (CurrentLife <= 3)
        {
            healthbar.GetComponent<Image>().color = Color.red;
        }
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1f);
        escena = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escena.name);
        //yield break;
    }

}
