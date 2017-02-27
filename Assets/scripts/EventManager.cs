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

	// A dictionary that maps Event types to Events.registeredHandlers
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
	// This is how to "publish" an event
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
/* to create a new event, make a new event that inherits from GameEvent */
public class GameEvent {}

public class EnemyKilledEvent : GameEvent {}


