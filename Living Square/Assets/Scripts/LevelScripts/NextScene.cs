using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
	//linking id
	public int NextSceneID;
	//transition to next scene name
	public string sceneName;

	public void OnButtonPress(Component invoker, object arg){
		//check if right object that triggered this
		if(invoker is Button && arg is bool){
			bool state = (bool)arg;
			//check if ids match
			if(((Button)invoker).ButtonID == NextSceneID){
				if(state){
					//load next scene
					if(sceneName=="CreditScene"){
						Invoke(nameof(unlockCursor), 1.5f);
						Invoke(nameof(nextScene), 1.5f);
					} else {
						Invoke(nameof(nextScene),0f);
					}
				}
			}
		}
	}
	
	private void nextScene(){
		SceneManager.LoadScene(sceneName);
	}
	
	private void unlockCursor(){
		Cursor.lockState = CursorLockMode.None;
	}
}
