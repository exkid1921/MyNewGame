using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallEraser : MonoBehaviour {

    private GameObject scoreText;

    BirdController birdController;

    public int score = 0;

    // Use this for initialization
    void Start () {

        birdController = GameObject.Find("BirdCentering").GetComponent<BirdController>();

        this.scoreText = GameObject.Find("Score");

    }
	
	// Update is called once per frame
	void Update () {

        this.scoreText.GetComponent<Text>().text = "Score : " + score + "pt";
    }

    void OnCollisionEnter(Collision wall)
    {
        if (wall.gameObject.tag == "WallTag" || wall.gameObject.tag == "closingWall" || wall.gameObject.tag == "StraightWall")
        {
            Debug.Log("destroy");
            if(wall.gameObject.tag == "closingWall")
            {
                birdController.closingRange *= Mathf.Sqrt(1.15f);
                score += 300;
            }
            else if(wall.gameObject.tag == "WallTag")
            {
                score += 150;
            }
            else if(wall.gameObject.tag == "StraightWall")
            {
                score += 100;
            }
            Destroy(wall.transform.parent.gameObject.gameObject);
        }
    }


}
