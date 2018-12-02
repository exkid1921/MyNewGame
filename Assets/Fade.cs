using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    BirdController birdController;

    GameObject fadeObj;

    float startTime;

    public int fadeTime = 1;

    private float a;

    private string fadeStart;

    private string sceneName;

    private bool isGameOver;

    // Use this for initialization
    void Start () {
        birdController = GameObject.Find("BirdCentering").GetComponent<BirdController>();

        fadeObj = GameObject.Find("Fade");

        startTime = Time.time;

        fadeStart = "FadeIn";

        sceneName = SceneManager.GetActiveScene().name;
    }
	
	// Update is called once per frame
	void Update () {
        if (sceneName == "Title" || sceneName == "Ranking")
        {
            switch (fadeStart)
            {
                case "FadeIn":
                    a = 1.0f - (Time.time - startTime) / fadeTime;
                    fadeObj.GetComponent<Image>().color = new Color(0, 0, 0, a);
                    break;
                case "FadeOut":
                    a = (Time.time - startTime) / fadeTime;
                    fadeObj.GetComponent<Image>().color = new Color(0, 0, 0, a);
                    break;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (sceneName == "Title")
                {
                    fadeStart = "FadeOut";
                    startTime = Time.time;
                    Invoke("ToPlaying", 1.5f);
                }
            }
        }
        else if(sceneName == "Playing")
        {
            this.isGameOver = birdController.isGameOver;
            switch (fadeStart)
            {
                case "FadeIn":
                    a = 1.0f - (Time.time - startTime) / fadeTime;
                    fadeObj.GetComponent<Image>().color = new Color(0, 0, 0, a);
                    break;
                case "FadeOut":
                    a = (Time.time - startTime) / fadeTime;
                    fadeObj.GetComponent<Image>().color = new Color(0, 0, 0, a);
                    break;
            }

            if (isGameOver == true && Input.GetMouseButtonDown(0))
            {
                fadeStart = "FadeOut";
                startTime = Time.time;
                Invoke("ToRanking", 1.5f);
            }
        }
    }
    public void ToPlaying()
    {
        SceneManager.LoadScene("Playing");
    }

    public void ToRanking()
    {
        SceneManager.LoadScene("Ranking");
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void titleButtonDown()
    {
        fadeStart = "FadeOut";
        startTime = Time.time;
        Invoke("ToTitle", 1.5f);
    }
}
