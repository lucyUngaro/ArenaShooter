using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager {
	static private EventManager instance;
	static public EventManager Instance { get {
			if (instance == null) {
				instance = new EventManager();
			}
			return instance;
		} }

	// A dictionary that maps types (specifically Events types in this case) to Events.registeredHandlers
	private Dictionary<System.Type,System.Delegate> registeredHandlers = new Dictionary<System.Type, System.Delegate>();

	public delegate void EventDelegate<T> (T e) where T : GameEvent;

	public void AddListener<T>(EventDelegate<T> del) where T : GameEvent {	
		if (registeredHandlers.ContainsKey(typeof(T))) {
			System.Delegate tempDel = registeredHandlers[typeof(T)];
			registeredHandlers[typeof(T)] = System.Delegate.Combine(tempDel, del);
		} else {
			registeredHandlers[typeof(T)] = del;
		}
	}


	// This is where you stop listening to an event. Make sure to balance
	// any calls to Register with corresponding calls to Unregister
	public void RemoveListener<T>(EventDelegate<T> del) where T : GameEvent {
		if (registeredHandlers.ContainsKey(typeof(T))) {
			var currentDel = System.Delegate.Remove(registeredHandlers[typeof(T)], del);
			if (currentDel == null) {
				registeredHandlers.Remove(typeof(T));
			} else {
				registeredHandlers[typeof(T)] = currentDel;
			}
		}
	}
	// This is how you "publish" and event. All it entails is looking up
	// the event type and calling the delegate containing all the handlers
	public void Fire(GameEvent e) {
		if (e == null) {
			Debug.Log("Invalid event arugment: " + e.GetType().ToString());
			return;
		}

		if (registeredHandlers.ContainsKey(e.GetType())) {
			registeredHandlers[e.GetType()].DynamicInvoke(e);
		}
	}

}
/* to create a new event, you need to make a new event that inherits from GameEvent */
public class GameEvent {} //blank brackets indicate the class does nothing

public class EnemyKilledEvent : GameEvent {
	public EnemyKilledEvent(){
	 
	}

}


