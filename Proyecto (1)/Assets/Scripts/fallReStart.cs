using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fallReStart : MonoBehaviour
{
    private Scene scene;
    // Use this for initialization
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag== "Player")
        {
            SceneManager.LoadScene(scene.name);
        }
    }
}
