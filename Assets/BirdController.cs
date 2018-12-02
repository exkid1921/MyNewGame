using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour {

    WallEraser wallEraser;

    private GameObject gameoverText;

    private Animator birdAnimator;

    private Rigidbody birdRigidbody;

    private float forwardForce = 200.0f;

    private float speed = 0.05f;

    private float turnspeed = 2.0f;

    private float movableRangeX = 0.5f;

    private float movableRangeY = 0.5f;

    private Vector3 targetPosition;

    private float x;

    private float y;

    //private float posX;

    //private float posY;

    private float posZ;

    private bool isBoostButtonDown = false;

    private bool turnflag = false;

    private GameObject birdcentering;

    private float time;

    public float closingRange = 10f;

    public bool isGameOver;

    private int[] highScore = new int[3];

    private string[] key = {"1st", "2nd", "3rd"};

    private float loadedTime;

    private float playingTime;

    private Vector3 startposition;

    private Vector3 currentposition;

    // Use this for initialization
    void Start () {

        this.loadedTime = Time.time;

        this.wallEraser = GameObject.Find("DeleteLine").GetComponent<WallEraser>();

        this.birdAnimator = GetComponent<Animator>();

        this.birdRigidbody = GetComponent<Rigidbody>();

        this.gameoverText = GameObject.Find("GameOverText");

        this.isGameOver = false;

        highScore[0] = PlayerPrefs.GetInt("1st", 0);

        highScore[1] = PlayerPrefs.GetInt("2nd", 0);

        highScore[2] = PlayerPrefs.GetInt("3rd", 0);

    }
	
	// Update is called once per frame
	void Update () {

        playingTime = Time.time - loadedTime;

        //this.posX = this.transform.position.x;

        //this.posY = this.transform.position.y;

        this.posZ = this.transform.position.z;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                startposition = new Vector3(Mathf.Clamp(hit.point.x, -this.movableRangeX, this.movableRangeX), Mathf.Clamp(hit.point.y, -this.movableRangeY, this.movableRangeY), hit.point.z);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                currentposition = new Vector3(Mathf.Clamp(hit.point.x, -this.movableRangeX, this.movableRangeX), Mathf.Clamp(hit.point.y, -this.movableRangeY, this.movableRangeY), hit.point.z);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            startposition = Vector3.zero;
            currentposition = Vector3.zero;
        }

        Vector3 direction = (this.currentposition - this.startposition);//.normalized;

        Vector3 nextPos = this.transform.position + direction * speed;

        this.transform.position = new Vector3(Mathf.Clamp(nextPos.x, -this.movableRangeX, this.movableRangeX), Mathf.Clamp(nextPos.y, -this.movableRangeY, this.movableRangeY), posZ);

        this.x = startposition.x - currentposition.x;

        if(x > 0.15)
        {
            this.birdAnimator.SetBool("movingLeft", false);
            this.birdAnimator.SetBool("movingRight", true);
        }

        else if(-0.15 <= x  &&  x <= 0.15)
        {
            this.birdAnimator.SetBool("movingLeft", false);
            this.birdAnimator.SetBool("movingRight", false);
        }

        else if(x < -0.15)
        {
            this.birdAnimator.SetBool("movingRight", false);
            this.birdAnimator.SetBool("movingLeft", true);
        }

        if(playingTime < 4)
        {
            if(1 <= playingTime && playingTime < 2)
            {
                this.gameoverText.GetComponent<Text>().text = "3";
            }
            else if(2 <= playingTime && playingTime < 3) {
                this.gameoverText.GetComponent<Text>().text = "2";
            }
            else if (3 <= playingTime && playingTime < 4)
            {
                this.gameoverText.GetComponent<Text>().text = "1";
            }
        }

        if (!isGameOver && playingTime >= 4)
        {
            if (4 <= playingTime && playingTime < 4.5)
            {
                this.gameoverText.GetComponent<Text>().text = "START!!";
            }
            else
            {
                this.gameoverText.GetComponent<Text>().text = "";
            }
            this.birdRigidbody.AddForce(this.transform.forward * this.forwardForce);

            if (this.isBoostButtonDown || Input.GetKeyDown(KeyCode.Space))
            {
                this.forwardForce = this.forwardForce * 1.175f;
                this.isBoostButtonDown = false;
                this.turnflag = true;
            }

            if (turnflag)
            {
                time = Mathf.Clamp(time, 0f, 1f);
                float angleZ = Mathf.LerpUnclamped(0f, -360f, time);
                transform.rotation = Quaternion.Euler(0, 0, angleZ);
                if (time >= 1f)
                {
                    time = 0f;
                    this.turnflag = false;
                }
                else
                {
                    time += Time.deltaTime * turnspeed;
                }
            }
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "WallTag" || other.gameObject.tag == "closingWall" || other.gameObject.tag == "StraightWall")
        {
            this.isGameOver = true;
            this.birdAnimator.SetBool("isGameOver", true);
            forwardForce = 0;
            speed = 0;
            this.gameoverText.GetComponent<Text>().text = "GAME OVER";
            for(int i = 0; i < key.Length; i++)
            {
                if(wallEraser.score > highScore[i])
                {
                    if(i == 0)
                    {
                        highScore[2] = highScore[1];
                        highScore[1] = highScore[i];
                        highScore[i] = wallEraser.score;
                        break;
                    }
                    else if(i == 1)
                    {
                        Debug.Log("2bandayo");
                        highScore[2] = highScore[1];
                        highScore[i] = wallEraser.score;
                        break;
                    }
                    else if (i == 2)
                    {
                        Debug.Log("3bandayo");
                        highScore[i] = wallEraser.score;
                        break;
                    }
                }
            }
            for (int j = 0; j < key.Length; j++)
            {
                PlayerPrefs.SetInt(key[j], highScore[j]);
            }
            PlayerPrefs.Save();
        }
    }

    public void BoostButtonDown()
    {
        this.isBoostButtonDown = true;
    }

}
