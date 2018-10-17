using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject bluejay;

    private float distance;


    // Use this for initialization
    void Start () {

        this.bluejay = GameObject.Find("blueJay");

        this.distance = bluejay.transform.position.z - this.transform.position.z;

    }
	
	// Update is called once per frame
	void Update () {

        this.transform.position = new Vector3(0, this.transform.position.y, this.bluejay.transform.position.z - distance);

	}
}
