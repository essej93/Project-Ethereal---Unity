using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideFloor : MonoBehaviour
{
	private Animator Animator;
	public int ID;
	
    void Start(){
		Animator = GetComponent<Animator>();
    }
	
	public void OnButtonPress(Component invoker, object arg)
	{
		if(invoker is Button && arg is bool){
			bool state = (bool)arg;
			if(((Button)invoker).ButtonID == ID){
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
