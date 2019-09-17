using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particulas chjocaron contra algo");
    }
}
