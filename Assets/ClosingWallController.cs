using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingWallController : MonoBehaviour {

    private GameObject bluejay;

    BirdController birdController;

    private Animator wallAnimator;

    private float distance;

    // Use this for initialization
    void Start () {

        this.bluejay = GameObject.Find("blueJay");

        birdController = GameObject.Find("BirdCentering").GetComponent<BirdController>();

        this.wallAnimator = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {

        this.distance = this.transform.position.z - bluejay.transform.position.z;
        
        if (distance <= birdController.closingRange)
        {
            this.wallAnimator.SetBool("isStartClosing", true);
        }
    }
}
