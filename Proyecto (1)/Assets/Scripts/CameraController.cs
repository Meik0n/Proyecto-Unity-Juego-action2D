using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private float xMax;

    [SerializeField]
    private float yMax;

    [SerializeField]
    private float xMin;

    [SerializeField]
    private float yMin;

    private Transform target;

    public float smoothLerp = 0.1f;

    //private Rigidbody2D Target;
    //private CharacterController2D Player;
	
	void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        //Player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
    }
	
	
	void FixedUpdate ()
    {
        var targetPos = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y+5, yMin, yMax), transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothLerp);

        //if (Target.velocity.x > 0 && Player.m_FacingRight)
        //{
        //    var targetPos = new Vector3(Mathf.Clamp(target.position.x + 10, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
        //    transform.position = Vector3.Lerp(transform.position, targetPos, smoothLerp);
        //}
        //else if(Target.velocity.x < 0 && Player.m_FacingRight)
        //{
        //    var targetPos = new Vector3(Mathf.Clamp(target.position.x - 10, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
        //    transform.position = Vector3.Lerp(transform.position, targetPos, smoothLerp);
        //}       
    }
}
