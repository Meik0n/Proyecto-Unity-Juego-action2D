using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    private GameObject pauseMenu;

    private Text timerText;
    // private float initTime;
    private float marcador = 0;
    private Scene scene;

    void Awake()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        timerText = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        pauseMenu.SetActive(false);

        //initTime = Time.time;
        marcador = PlayerPrefs.GetFloat("marcador");

        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        StartCoroutine(Timer());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1.0f)
            {
                StopTime();
            }
            else if (Time.timeScale == 0f)
            {
                ReanudeTime();
            }
        }

        // marcador = Time.time - initTime;

        string minutes = ((int)marcador / 60).ToString();
        string seconds = (marcador % 60).ToString("f2");

        GuardaMovidas();
        timerText.text = minutes + ":" + seconds;
    }

    private void GuardaMovidas()
    {
        PlayerPrefs.SetFloat("marcador", marcador);
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ReanudeTime()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void ReiniciarLevel()
    {
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    public void ReiniciarJuego()
    {
        SceneManager.LoadScene("FranchiScene");
        marcador = 0f;
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    public IEnumerator Timer()
    {
        while (true)
        {
            marcador += .01f;
            yield return new WaitForSeconds(.01f);
        }
    }
}
