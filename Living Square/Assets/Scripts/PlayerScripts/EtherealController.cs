using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherealController : MonoBehaviour{

/*==============================================================================
								VARIABLES
==============================================================================*/
	//player settings variables
    public float SPEED = 5f;
    public float DISTANCE_LIMIT = 10f;
    public float DISTANCE_LIMIT_SQUARED = 100f;

	//player internal variables
	private float mvX;
	private float mvZ;
	private Vector3 startPos;
	private Vector3 mvDir;
	private Vector3 mvPos;
	private Vector3 mvPosClamped;

	//player Components
	private Rigidbody thisRBody;
	private new CapsuleCollider collider;

	//Animation Objects
	private Animator charAnim;

	/*==============================================================================
										START
	==============================================================================*/
	// Start is called before the first frame update ie. Constructor
	void Start(){
		//get the components needed
		thisRBody = GetComponent<Rigidbody>();
		//get the starting position
		startPos = transform.position;
		//ignore collision with the player itself when ethereal
		Physics.IgnoreLayerCollision(11, 8);
		//ignore collision Ethereal layer 11 with objects in passable layer 10
		Physics.IgnoreLayerCollision(11, 10);
		//get player's collider object
		collider = GetComponent<CapsuleCollider>();
		// Character animation controller
		charAnim = GetComponent<Animator>();
	}


/*==============================================================================
									UPDATE
==============================================================================*/
    // Update is called once per frame
	//should only be used to get input and non physics related code
    void Update(){
		//get inputs
        mvX = Input.GetAxis("Horizontal");
        mvZ = Input.GetAxis("Vertical");

		// Update animation state
		if (charAnim != null)
		{
			if (mvX != 0 || mvZ != 0)
			{
				//Debug.Log("RUNNING");
				charAnim.SetBool("running", true);
			}
			else
			{
				//Debug.Log("IDLE");
				charAnim.SetBool("running", false);
			}
		}
	}


/*==============================================================================
								FIXED_UPDATE
==============================================================================*/
	// May update many times between frames follows fixed physics timestep
	void FixedUpdate(){
		//calculate direction vector
		mvDir = transform.forward * mvZ + transform.right * mvX;
		//calculate moved position
		mvPos = transform.position + mvDir * Time.fixedDeltaTime * SPEED;
		//check if greater than distance limit
		if((mvPos - startPos).sqrMagnitude > DISTANCE_LIMIT_SQUARED){
			//clamp the magnitude to the distance limit
			mvPosClamped = Vector3.ClampMagnitude(mvPos - startPos, DISTANCE_LIMIT);
			//move body to clamped distance from radius center in direction
			thisRBody.MovePosition(mvPosClamped + startPos);
		}
		else{
			//move body
			thisRBody.MovePosition(mvPos);
		}
	}

/*==============================================================================
                                ON_COLLISION_ENTER
==============================================================================*/
    private void OnCollisionEnter(Collision collision)
    {
		// Ignore collision with any pickable objects
        if (collision.gameObject.tag == "Pickable")
            Physics.IgnoreCollision(collision.collider, collider);
    }
}
