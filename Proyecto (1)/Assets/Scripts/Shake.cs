﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator camAnim;

    public void CamShake()
    {
        int rand = Random.Range(0, 3);
        if(rand == 0)
        {
            camAnim.SetTrigger("Shake");
        }else if(rand == 1)
        {
            camAnim.SetTrigger("Shake02");
        }
        else if (rand == 2)
        {
            camAnim.SetTrigger("Shake03");
        }

    }	
}
