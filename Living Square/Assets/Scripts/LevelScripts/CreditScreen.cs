using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScreen : MonoBehaviour
{
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

	//credit settings
	public Font creditFont;
	private Color creditColour = new Color(0.9f,0.6f,0,1);

	//title style
	private GUIStyle titleStyle = new GUIStyle();
	//menu style
	private GUIStyle menuStyle = new GUIStyle();
	//credit style
	private GUIStyle creditStyle = new GUIStyle();
	//title and menu box
	private Rect titleBox;
	private Rect buttonBox1;
	private Rect buttonBox2;

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

		//settings for credit
		creditStyle.font = creditFont;
		creditStyle.fontSize = 20;
		creditStyle.alignment = TextAnchor.UpperCenter;
		creditStyle.normal.textColor = creditColour;
		creditStyle.normal.background = Texture2D.blackTexture;

		//create title and menu box area
		titleBox = new Rect(0, 0, workingAreaWidth, 200);
		buttonBox1 = new Rect(0, 180, workingAreaWidth, 25);
		buttonBox2 = new Rect(0, 390, workingAreaWidth, 25);
	}

	void OnGUI(){
		//working area of the title and menu buttons
		GUI.BeginGroup(
			new Rect(halfWidthStart, halfHeightStart, workingAreaWidth, workingAreaHeight)
		);
		GUI.Label(titleBox, "THE END\nCREDITS", titleStyle);

		GUI.Label(buttonBox1, "Jesse Surridge\nRaymond King\nGardyan Priangga Akbar\nNicholas Arthur\nLyon Fan\nJosh Hogan", creditStyle);

		if(GUI.Button(buttonBox2, "RETURN", menuStyle)){
			SceneManager.LoadScene("TitleMenu");
		}

		GUI.EndGroup();
	}
}
