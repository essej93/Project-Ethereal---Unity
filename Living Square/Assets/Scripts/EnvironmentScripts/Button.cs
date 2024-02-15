using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
/*==============================================================================
									VARIABLES
==============================================================================*/
	//flags
	private bool isTriggered = false;

	//objects
	private Animator ButtonAnimator;
	private AudioSource audiosource;

	//Events
	public EventManager OnButtonTriggered;
	//id for what is linked to this button
	public int ButtonID; 

	//internal variables
	private Ray ray;
	private RaycastHit hit;
	private bool hasHit = false;
	private bool activeButton = false;

	//text for interact
	private bool labelActive = false;
	private int boxWidth = 50;
	private int boxHeight = 20;
	private int heightOffset = 20;
	private Rect labelWorkingArea;
	private GUIStyle labelStyle = new GUIStyle();
	private Color labelColour = new Color(0.9f,0.1f,0.1f,1);
	public Font labelFont;

/*==============================================================================
									START
==============================================================================*/
    void Start()
    {
		audiosource = GetComponent<AudioSource>();
		ButtonAnimator = GetComponent<Animator>();

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
	
	//trigger activebutton for only the button where the player is
	void OnTriggerEnter(){activeButton = true;}
	void OnTriggerExit(){activeButton = false;}

    void Update()
    {
		if(activeButton){
			//get ray for where camera is pointing
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			hasHit = Physics.Raycast(ray,out hit, 3.0f);

			//check if hit an interactive area ie. button
			if(hasHit && hit.collider.gameObject.tag == "InteractiveArea"){
				labelActive = true;

				if(Input.GetMouseButtonDown(0)){
					//animate and sound audio
					ButtonAnimator.SetTrigger("Down");
					audiosource.Play(0);

					//trigger button action (function "up") after 0.5 seconds
					Invoke(nameof(up), 0.5f);

					//toggle button
					if(isTriggered == false)
					{
						//open action object
						isTriggered = true;
						OnButtonTriggered.RaiseEvent(this, true);
					} else {
						//close action object
						isTriggered = false;
						OnButtonTriggered.RaiseEvent(this, false);
					}
				}
			}
			else if(labelActive){labelActive = false;}
		}
		else if(labelActive){labelActive = false;}
    }
	
	void OnGUI(){
		//show only when looking at button
		if(labelActive){
			GUI.Label(labelWorkingArea, "Left Click", labelStyle);
		}
	}

	public void setLabelActive(bool state){
		labelActive = state;
	}

	void up()
	{
		ButtonAnimator.SetTrigger("Up");
		Invoke(nameof(idle),1f);
	}
	
	void idle()
	{
		ButtonAnimator.SetTrigger("Idle");
	}
}