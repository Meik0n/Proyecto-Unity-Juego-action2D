﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lluvia : MonoBehaviour {

    //ParticleSystem ps;
    //public GameObject particulasLluvia;

    //// these lists are used to contain the particles which match
    //// the trigger conditions each frame.
    //List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    //List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        //ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        //// get the particles which matched the trigger conditions this frame
        //int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        //// iterate through the particles which entered the trigger and make them red
        //for (int i = 0; i < numEnter; i++)
        //{
        //    ParticleSystem.Particle p = enter[i];
        //    Instantiate(particulasLluvia, transform.position, Quaternion.identity);
        //}

        ///*
        //// iterate through the particles which exited the trigger and make them green
        //for (int i = 0; i < numExit; i++)
        //{
        //    ParticleSystem.Particle p = exit[i];
        //    p.startColor = new Color32(0, 255, 0, 255);
        //    exit[i] = p;
        //}
        //*/
        //// re-assign the modified particles back into the particle system
        //ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }
}
