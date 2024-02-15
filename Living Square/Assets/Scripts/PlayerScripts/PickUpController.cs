using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
/*==============================================================================
									START
==============================================================================*/
    void Start()
    {
		audiosource = GetComponent<AudioSource>();
		
		//interact label area under crosshair
		labelWorkingArea = new Rect(
			(int)((Screen.width - boxWidth) * 0.5),
			(int)((Screen.height - boxHeight) * 0.5) + heightOffset,
			boxWidth, boxHeight
		);
		//label style
		labelStyle.font = labelFont;
		labelStyle.fontSize = 14;
		labelStyle.normal.textColor = labelColour;
		labelStyle.alignment = TextAnchor.UpperCenter;
    }
	
	
/*==============================================================================
								VARIABLES
==============================================================================*/
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    [HideInInspector]public GameObject heldObj;
    private Rigidbody heldObjRb;
    private int heldObjOriginalLayer;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 3.0f;
    [SerializeField] private float pickupForce = 150.0f;
    [SerializeField] private float throwForce = 150.0f;
	
	//internal variables
	private AudioSource audiosource;
	private Ray ray;
	private RaycastHit hit;
	private bool hasHit = false;
	private bool activeButton = false;
    private float originalDrag;

	//text for interact
	private bool labelActive = false;
	private bool labelThrow = false;
	private int boxWidth = 50;
	private int boxHeight = 20;
	private int heightOffset = 20;
	private Rect labelWorkingArea;
	private GUIStyle labelStyle = new GUIStyle();
	private Color labelColour = new Color(0.9f,0.1f,0.1f,1);
	public Font labelFont;

/*==============================================================================
									UPDATE
==============================================================================*/
    private void Update()
    {
        // TODO: Don't know what's the best key for holding/release yet
		//Check if raycast hits object
		RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
        {
			//Check if raycast hits pickable object
			// Only pick up object if it has the tag Pickable
			if (hit.transform.gameObject.tag == "Pickable")
			{
				//Check if object is already picked up
				if(heldObj == null){
					labelActive = true;
				} else {
					labelActive= false;
				}
				//'E' pressed
				if (Input.GetKeyDown(KeyCode.E))
				{
					//Pick up item if no item is already picked up
					if (heldObj == null)
					{
                        // Pick up object from raycast
                        PickupObject(hit.transform.gameObject);
						labelActive=false;
						labelThrow=true;
					}
					//Drop picked up item
					else
					{
						// Drop object
						DropObject();
						labelThrow=false;
					}
                }
			}
			//Remove gui if no item with Pickable tag is found
			else 
			{
				labelActive=false;
			}
        }
        else
        {
            labelActive = false;
        }

        if (heldObj != null)
        {
            // Move object
            MoveObject();

            // TODO: Don't know what's the best key for throwing the object yet
            if (Input.GetMouseButtonDown(0))
            {
                // Throw picked up object
				audiosource.Play(0);
                ThrowObject();
            }
        }
    }
	
	void OnGUI(){
		//show only when looking at button
		if(labelActive){
			GUI.Label(labelWorkingArea, "Press 'E' to pick up", labelStyle);
		}
		if(labelThrow){
			GUI.Label(labelWorkingArea, "Left Click to throw", labelStyle);
		}
	}
/*==============================================================================
                                PICK UP METHODS
==============================================================================*/
    void PickupObject(GameObject pickedObj)
    {
        if (pickedObj.GetComponent<Rigidbody>())
        {
            heldObjRb = pickedObj.GetComponent<Rigidbody>();
            originalDrag = heldObjRb.drag;
            heldObjRb.useGravity = false;
            heldObjRb.drag = 10;
            heldObjRb.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRb.transform.parent = holdArea;
            heldObj = pickedObj;
            // Move held object to layer 12 (PickedUpObject) to prevent physics collision with body
            heldObjOriginalLayer = heldObj.layer;
            heldObj.layer = 12;
        }
    }
    public void DropObject()
    {
        heldObjRb.useGravity = true;
        heldObjRb.drag = originalDrag;
        heldObjRb.constraints = RigidbodyConstraints.None;

        // Return held object to its original layer
        heldObj.layer = heldObjOriginalLayer;

        heldObjRb.transform.parent = null;
        heldObj = null;
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRb.AddForce(moveDirection * pickupForce);
        }
    }
    void ThrowObject()
    {
		labelThrow = false;
        heldObjRb.useGravity = true;
        heldObjRb.drag = 1;
        heldObjRb.constraints = RigidbodyConstraints.None;

        Vector3 throwDirection = transform.forward;
        heldObjRb.AddForce(throwDirection * throwForce);

        // Return held object to its original layer
        heldObj.layer = heldObjOriginalLayer;

        heldObjRb.transform.parent = null;
        heldObj = null;
    }

    public void ResetLabelFlags()
    {
        labelActive = false;
        labelThrow = false;
    }
}
