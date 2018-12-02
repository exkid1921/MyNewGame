using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour {

    public GameObject halfwall;

    public GameObject quarterwall;

    public GameObject closingWall;

    public GameObject straightwall;

    private GameObject bluejay;

    private int goalPos = 600;

    private int genePos = 10;

    private float rotationRange;

    private int wallNum = 0;

	// Use this for initialization
	void Start () {

        this.bluejay = GameObject.Find("blueJay");

    }
	
	// Update is called once per frame
	void Update () {
        if (genePos < this.bluejay.transform.position.z + 50  &&  genePos <= goalPos)
        {
            Generate(genePos);

            if( genePos <= 300)
            {
                genePos += 10;
            }
            else if(genePos > 300)
            {
                genePos += 15;
            }
            
        }
    }

    void Generate(int genePos)
    {
        if (wallNum == 10)
        {
            GameObject door = Instantiate(closingWall) as GameObject;
            door.transform.position = new Vector3(0, 0, genePos);
            this.rotationRange = Random.Range(0, 360);
            door.transform.rotation = Quaternion.Euler(0, 0, rotationRange);
            this.wallNum = 0;
        }
        else
        {
            int num = Random.Range(1, 11);

            if (1 <= num && num <= 4)
            {
                GameObject half = Instantiate(halfwall) as GameObject;
                half.transform.position = new Vector3(0, 0, genePos);
                this.rotationRange = Random.Range(0, 360);
                half.transform.rotation = Quaternion.Euler(0, 0, rotationRange);
                this.wallNum++;
            }
            else if (5 <= num && num <= 8)
            {
                GameObject straight = Instantiate(straightwall) as GameObject;
                straight.transform.position = new Vector3(0, 0, genePos);
                this.rotationRange = Random.Range(0, 360);
                straight.transform.rotation = Quaternion.Euler(0, 0, rotationRange);
                this.wallNum++;
            }
            else if (9 <= num && num <= 10)
            {
                GameObject quarter = Instantiate(quarterwall) as GameObject;
                quarter.transform.position = new Vector3(0, 0, genePos);
                this.rotationRange = Random.Range(0, 360);
                quarter.transform.rotation = Quaternion.Euler(0, 0, rotationRange);
                this.wallNum++;
            }
        }
    }
}
