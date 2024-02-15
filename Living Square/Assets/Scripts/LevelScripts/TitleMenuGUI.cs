using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuGUI : MonoBehaviour
{
	//flags
	private bool levelSelectActive = false;

	//working area
	private int halfWidthStart;
	private int halfHeightStart;
	private int workingAreaWidth = 430;
	private int workingAreaHeight = 450;

	/*
	PLEASE NOTE THAT THESE VARIABLES ARE HERE SUCH THAT IF IN THE FUTURE CAN BE
	CHANGED INTO PUBLIC VARIABLES AVAILABLE IN THE INSPECTOR
	*/
	//title settings
	public Font titleFont;
	private Color titleColour = new Color(1,1,1,1);
	//menu settings
	public Font menuFont;
	private Color menuColour = new Color(1,1,1,1);
	private Color menuHoverColour = new Color(0.9f,0.5f,0,1);


	//title style
	private GUIStyle titleStyle = new GUIStyle();
	//menu style
	private GUIStyle menuStyle = new GUIStyle();
	//title and menu box
	private Rect titleBox;
	private Rect buttonBox1;
	private Rect buttonBox2;
	private Rect buttonBox3;
	private Rect buttonBox4;

	void Start(){
		//centered box on screen
		halfWidthStart = (int)((Screen.width - workingAreaWidth) * 0.5);
		halfHeightStart = (int)((Screen.height - workingAreaHeight) * 0.5);

		//setting the style for title
		titleStyle.font = titleFont;
		titleStyle.fontSize = 56;
		titleStyle.normal.textColor = titleColour;
		titleStyle.alignment = TextAnchor.UpperCenter;

		//setting the style for menu buttons
		menuStyle.font = menuFont;
		menuStyle.fontSize = 20;
		menuStyle.alignment = TextAnchor.UpperCenter;
		menuStyle.normal.textColor = menuColour;
		menuStyle.normal.background = Texture2D.blackTexture;
		//hover does not work unless there is a texture underneath
		menuStyle.hover.textColor = menuHoverColour; 
		//change colour of basic gray texture
		Texture2D aTex = Texture2D.grayTexture;
		Color[] texColours = new Color[16];
		for(int i = 0; i < 16; ++i){
			texColours[i].r = 0.1f;
			texColours[i].g = 0.1f;
			texColours[i].b = 0.1f;
			texColours[i].a = 1;
		}
		aTex.SetPixels(texColours);
		aTex.Apply(false);
		menuStyle.hover.background = aTex;

		//create title and menu box area
		titleBox = new Rect(0, 0, workingAreaWidth, 200);
		buttonBox1 = new Rect(0, 250, workingAreaWidth, 25);
		buttonBox2 = new Rect(0, 300, workingAreaWidth, 25);
		buttonBox3 = new Rect(0, 350, workingAreaWidth, 25);
		buttonBox4 = new Rect(0, 400, workingAreaWidth, 25);
	}

	void OnGUI(){
		//working area of the title and menu buttons
		GUI.BeginGroup(
			new Rect(halfWidthStart, halfHeightStart, workingAreaWidth, workingAreaHeight)
		);

		if(levelSelectActive){
			GUI.Label(titleBox, "LEVEL\nSELECT", titleStyle);
			if(GUI.Button(buttonBox1, "LEVEL 1", menuStyle)){
				SceneManager.LoadScene("Scene_lv3");
			}

			if(GUI.Button(buttonBox2, "LEVEL 2", menuStyle)){
				SceneManager.LoadScene("Scene_lv4");
			}

			if(GUI.Button(buttonBox3, "RETURN", menuStyle)){
				levelSelectActive = false;
			}
		}
		else{
			//Title label
			GUI.Label(titleBox, "PROJECT\nETHEREAL", titleStyle);

			if(GUI.Button(buttonBox1, "START", menuStyle)){
				//lighting may not generate in editor debug mode when loading scene
				//to fix (important go to "Scene_lvl1" first):  
				//Windows > Rendering > lighting settings > tick Auto Generate
				SceneManager.LoadScene("Scene_lv3");
			}

			if(GUI.Button(buttonBox2, "LEVEL SELECT", menuStyle)){
				levelSelectActive = true;
			}

			if(GUI.Button(buttonBox3, "CREDITS", menuStyle)){
				SceneManager.LoadScene("CreditScene");
			}

			if(GUI.Button(buttonBox4, "QUIT", menuStyle)){
				//this function is ignored by the debug editor and works only when
				// as a standalone application
				Application.Quit();
			}
		}

		GUI.EndGroup();
	}
}
