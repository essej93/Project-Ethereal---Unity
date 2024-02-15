using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//creates a custom event to allow for passing parameters
// as the base unity events does not include it
[System.Serializable]
public class CustomEvent : UnityEvent<Component, object>{} 

public class EventListener : MonoBehaviour
{
	//the event type manager to listem to
	public EventManager eventManager;
	public CustomEvent anEvent;

	//add and remove listener on listener creation
	private void OnEnable(){
		eventManager.AddListener(this);
	} 

	private void OnDisable(){
		eventManager.RemoveListener(this);
	} 

	//called when event is triggered for each listener
	public void OnRaisedEvent(Component invoker, object arg){
		anEvent.Invoke(invoker, arg);
	}
}
