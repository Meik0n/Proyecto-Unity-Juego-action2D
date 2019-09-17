using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingMouse : MonoBehaviour
{

    //private bool facingRight = GameObject.Find("Player").GetComponent<CharacterController2D>().andeMira;
    CharacterController2D player;
    Weapon arma;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<CharacterController2D>();
        arma = FindObjectOfType<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        FaceMouse();

    }

    void FaceMouse()
    {
        if (Time.timeScale != 0)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = transform.position.z;


            //Vector2 direction = new Vector2(
            //    mousePosition.x - transform.position.x,       
            //    mousePosition.y - transform.position.y).normalized;

            var direction = (mousePosition - transform.position).normalized;
            Debug.Log("direction "+direction);
  

            if (direction.x < -0.4f && player.m_FacingRight == true)
            {
                player.Flip();
                arma.transform.localScale = new Vector3(1, arma.transform.localScale.y * -1, 1);
            }
            else if (direction.x > 0.4f && player.m_FacingRight == false)
            {
                player.Flip();
                arma.transform.localScale = new Vector3(1, arma.transform.localScale.y * -1, 1);
            }
            transform.right = direction;                      
        }

    }
}
