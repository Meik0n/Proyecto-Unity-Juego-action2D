using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollsionBlood : MonoBehaviour
{

    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;
    public List<GameObject> bloodSprites;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (other)
            {
                Vector3 pos = collisionEvents[i].intersection;
                int n = Random.Range(0, 51);
                if (n == 1)
                {
                    Instantiate(bloodSprites[Random.Range(0, bloodSprites.Count)], pos, Quaternion.identity);
                }
            }
            i++;
        }
    }
}