using UnityEngine;
using UnityEngine.Events;


public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private float doubleJumpForce = 40f;
    //[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck1;
    [SerializeField] private Transform m_GroundCheck2; 
   // [SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	//[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching
    private PlayerController player1;

    const float k_GroundedRadius = .3f; // Radius of the overlap circle to determine if grounded
    [HideInInspector] public bool m_Grounded;            // Whether or not the player is grounded.
	//const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
    [HideInInspector] public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;


    /*
	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;
    */
    private bool spawnDust;
    /*
	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
    */

    /*
	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;
    */

    [Header("Efectos")]
    [Space]

    public GameObject dustEffect;

    [Header("Audio")]
    [Space]
    private AudioSource clip;
    public AudioClip audioToPlayOnJump;
   

    private void Start()
    {
        player1 = GetComponent<PlayerController>();
        clip = GetComponent<AudioSource>();
    }

    private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
        /*

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
            */
        /*
		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
            */
	}

    private void Update()
    {
        if (m_Grounded)
        {
            if (spawnDust == true)
            {
                Instantiate(dustEffect, m_GroundCheck1.position, Quaternion.identity);
                spawnDust = false;
            }
        }
        else
        {
            spawnDust = true;
        }
    }

    private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
        RaycastHit2D ray1 = Physics2D.Raycast(m_GroundCheck1.position, Vector2.down, 1, m_WhatIsGround);
        RaycastHit2D ray2 = Physics2D.Raycast(m_GroundCheck2.position, Vector2.down, 1, m_WhatIsGround);
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        /*
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck1.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
            
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
        */
        if(ray1 || ray2)
        {
            m_Grounded = true;
        }
    }


	public void Move(float move,/* bool crouch,*/ bool jump)
	{
        /*
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
                */
			//}
            

            //if (move1)
            //{
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                // And then smoothing it out and applying it to the character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing/*, 100f*/);
            //}
            /*
            
			// If the input is moving the player right and the player is facing left...
            /*
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
            */
		//}
        // If the player should jump...
        
        if (jump)
        {
            if (player1.doubleJump || (!player1.doubleJump && !player1.jumpPowerup))
            {               
                m_Grounded = false;
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
                clip.PlayOneShot(audioToPlayOnJump);
            }
            if (!player1.doubleJump && player1.jumpPowerup)
            {
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, doubleJumpForce);
            }
        }
    }


	public void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

        /*
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        */
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(m_GroundCheck1.position, Vector2.down * 1f);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(m_GroundCheck2.position, Vector2.down * 1f);
    }
}
