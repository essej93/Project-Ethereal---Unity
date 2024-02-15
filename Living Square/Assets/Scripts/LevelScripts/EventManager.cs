using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages a single event type
[CreateAssetMenu(menuName = "EventManager")]
public class EventManager : ScriptableObject
{
	public HashSet<EventListener> allListeners = new HashSet<EventListener>();

	//trigger listeners attached to the given event type
	public void RaiseEvent(Component invoker, object arg){
		foreach(var listener in allListeners){
			listener.OnRaisedEvent(invoker, arg);
		}
	}

	//add and remove listeners to this event type
	public void AddListener(EventListener listener){
		if(!allListeners.Contains(listener)){allListeners.Add(listener);}
	}

	public void RemoveListener(EventListener listener){
		if(allListeners.Contains(listener)){allListeners.Remove(listener);}
	}
}
