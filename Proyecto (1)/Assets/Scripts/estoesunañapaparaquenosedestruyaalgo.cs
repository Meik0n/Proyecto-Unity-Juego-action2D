using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class estoesunañapaparaquenosedestruyaalgo : MonoBehaviour
{
    public static estoesunañapaparaquenosedestruyaalgo instance = null;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
