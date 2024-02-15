using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Camera must ALWAYS be a child of an object (Physical or Ethereal body)
 * Otherwise this script will break and the world will explode :D
*/
public class CameraController : MonoBehaviour
{
    // Settings variables
	private GameObject player;
	private bool deathMenu = false;
	private NormalMenu normMenu;
    public float SENS_HOR = 3f;
    public float SENS_VER = 2f;
    public float VERTICAL_ROTATE_LIMIT = 45f;

    // internal variables
    private float mouseX = 0.0f;
    private float mouseY = 0.0f;
		bool mouseActive = true;

	//crosshair point area size
	//crosshair is active or not can be turned off
	private bool crosshairActive = true;
	private int boxWidth = 6;
	private int boxHeight = 6;
	//must not be larger than box width or height
	private int crosshairWidth = 2;
	private Rect crosshairWorkingArea;
	private Rect crosshairBox1;
	private Rect crosshairBox2;
	private GUIStyle crosshairStyle = new GUIStyle();

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.FindWithTag("Player");
		normMenu = player.GetComponent<NormalMenu>();
        // disable the mouse cursor
        Cursor.lockState = CursorLockMode.Locked;

		//crosshair in 8x8 box in center
		crosshairWorkingArea = new Rect(
			(int)((Screen.width - boxWidth) * 0.5),
			(int)((Screen.height - boxHeight) * 0.5),
			boxWidth, boxHeight
		);
		//start x=2,y=0 and width=4,height=8
		crosshairBox1 = new Rect(
			(int)((boxWidth - crosshairWidth) * 0.5), 0,
			crosshairWidth, boxHeight
		);
		//start x=0,y=2 and width=8,height=4
		crosshairBox2 = new Rect(
			0, (int)((boxWidth - crosshairWidth) * 0.5),
			boxWidth, crosshairWidth
		);

		crosshairStyle.normal.background = Texture2D.whiteTexture;
    }

    // Update is called once per frame
    void Update()
    {
        // get inputs
		if(mouseActive){
        	mouseX = Input.GetAxisRaw("Mouse X") * SENS_HOR;
        	mouseY = Input.GetAxisRaw("Mouse Y") * SENS_VER * -1;

        	//rotate around y axis for body
        	transform.parent.Rotate(0, mouseX, 0);
        	//rotate camera according to mouse
       		// transform.Rotate(mouseY, 0, 0);
  	    	ClampCameraRotation(mouseY);
		}
//using key Q instead of escape since the unity editor also uses that key
//for escaping the editor
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q)){ 
#else
        // enable the mouse cursor if Esc pressed
        if (Input.GetKeyDown(KeyCode.Escape)){ 
#endif
			if (!normMenu.deathMenu){
				setmouseActive();
			}
		}
    }
    // Method to restrict vertical camera movement
    void ClampCameraRotation(float incrementY)
    {
        float currentXRot = transform.eulerAngles.x;
        float whenAdded = currentXRot + incrementY;
        float upperBound = 360 - VERTICAL_ROTATE_LIMIT;

        if (whenAdded < upperBound && whenAdded > 90)
        {
            incrementY = 360 - VERTICAL_ROTATE_LIMIT - currentXRot;
        }
        else if (whenAdded > VERTICAL_ROTATE_LIMIT && whenAdded <= 90)
        {
            incrementY = VERTICAL_ROTATE_LIMIT - currentXRot;
        }

        transform.Rotate(incrementY, 0, 0);
    }

	void OnGUI(){
		if(crosshairActive){
			GUI.BeginGroup(crosshairWorkingArea);
			GUI.Box(crosshairBox1, "", crosshairStyle);
			GUI.Box(crosshairBox2, "", crosshairStyle);
			GUI.EndGroup();
		}
	}

	//can be changed when accessing menu or upon death
	public void setCrosshairActive(bool state){
		crosshairActive = state;
	}
	
	public void setmouseActive()
	{
		mouseActive = !mouseActive;
	}
}
