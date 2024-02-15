using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
	private Animator Animator;
	public int BridgeID;
	
    void Start(){
		Animator = this.GetComponent<Animator>();
    }
	
	public void OnButtonPress(Component invoker, object arg){
		//check if right object that triggered this
		if(invoker is Button && arg is bool){
			bool state = (bool)arg;
			//check if ids match
			if(((Button)invoker).ButtonID == BridgeID){
				if(state){
					open();
				}
				else{
					close();
				}
			}
		}
	}
	
	void open()
	{
		Animator.SetTrigger("Open");
	}
	
	void close()
	{
		Animator.SetTrigger("Close");
		Invoke(nameof(reset), 2f);
	}
	
	void reset()
	{
		Animator.SetTrigger("Idle");
	}
}