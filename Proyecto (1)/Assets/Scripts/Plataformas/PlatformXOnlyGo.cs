using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformXOnlyGo : MonoBehaviour
{

    public float Speed = 5f;
    public GameObject target;
    [HideInInspector] public bool activeMove = false;

    private void Start()
    {
    }

    void FixedUpdate()
    {
        if (activeMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}