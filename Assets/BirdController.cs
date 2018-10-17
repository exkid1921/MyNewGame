using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BirdController : MonoBehaviour {

    private Animator birdAnimator;

    private Rigidbody birdRigidbody;

    private float forwardForce = 200.0f;

    private float speed = 0.01f;

    private float movableRangeX = 0.5f;

    private float movableRangeY = 0.5f;

    private float x;

    private float y;

    private float posX;

    private float posY;

    private float posZ;

    // Use this for initialization
    void Start () {

        this.birdAnimator = GetComponent<Animator>();

        this.birdRigidbody = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {

        this.posX = this.transform.position.x;

        this.posY = this.transform.position.y;

        this.posZ = this.transform.position.z;

        this.x = CrossPlatformInputManager.GetAxisRaw("Horizontal");

        this.y = CrossPlatformInputManager.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, y, 0).normalized;

        //this.birdRigidbody.velocity = direction;

        this.transform.position += direction * speed;

        //this.transform.position = new Vector3(Mathf.Clamp(posX, -this.movableRangeX, this.movableRangeX), Mathf.Clamp(posY, -this.movableRangeY, this.movableRangeY), posZ);
             
        if(x > 0.1)
        {
            this.birdAnimator.SetBool("movingLeft", false);
            this.birdAnimator.SetBool("movingRight", true);
        }

        else if(x == 0)
        {
            this.birdAnimator.SetBool("movingLeft", false);
            this.birdAnimator.SetBool("movingRight", false);
        }

        else if(x < -0.1)
        {
            this.birdAnimator.SetBool("movingRight", false);
            this.birdAnimator.SetBool("movingLeft", true);
        }

        this.birdRigidbody.AddForce(this.transform.forward * this.forwardForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
}
