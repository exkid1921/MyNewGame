using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ranking : MonoBehaviour {

    private GameObject warning;

    private GameObject firstScore;

    private GameObject secondScore;

    private GameObject thirdScore;

	// Use this for initialization
	void Start () {

        this.warning = GameObject.Find("Warning");

        this.warning.gameObject.SetActive(false);

        this.firstScore = GameObject.Find("1st");

        this.secondScore = GameObject.Find("2nd");

        this.thirdScore = GameObject.Find("3rd");

        this.firstScore.GetComponent<Text>().text ="1st　　:" + PlayerPrefs.GetInt("1st", 0).ToString();

        this.secondScore.GetComponent<Text>().text = "2nd　　:" + PlayerPrefs.GetInt("2nd", 0).ToString();

        this.thirdScore.GetComponent<Text>().text = "3rd　　:" + PlayerPrefs.GetInt("3rd", 0).ToString();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void ResetButtonDown()
    {
        //PlayerPrefs.DeleteAll();
        this.warning.gameObject.SetActive(true);
    }
}
