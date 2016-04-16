using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

  //Variables
  private Dictionary <string, UnityEvent> eventDictionary;
  private static EventManager eventManager;

  //===================================================================================================================

  //Property
  public static EventManager instance {
    get {
      if(!eventManager) {
        eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

        if(!eventManager)
          Debug.LogError("There's no event manager attached anywhere, buddy!");
        else 
          eventManager.init();
      }
      return eventManager;
    }
  }

  //===================================================================================================================

  private void init(){
    if(eventDictionary == null)
      eventDictionary = new Dictionary<string, UnityEvent>();
  }

  //===================================================================================================================

  public static void startListening(string eventName, UnityAction listener) {
    UnityEvent thisEvent = null;
    if(instance.eventDictionary.TryGetValue(eventName, out thisEvent))
      thisEvent.AddListener(listener);
    else {
      thisEvent = new UnityEvent();
      thisEvent.AddListener(listener);
      instance.eventDictionary.Add(eventName, thisEvent);
    }
  }

  //===================================================================================================================

  public static void stopListening(string eventName, UnityAction listener) {
    if(eventManager == null) return;
    UnityEvent thisEvent = null;
    if(instance.eventDictionary.TryGetValue(eventName, out thisEvent))
      thisEvent.RemoveListener(listener);
  }

  //===================================================================================================================

  public static void triggerEvent(string eventName) {
    UnityEvent thisEvent = null;
    if(instance.eventDictionary.TryGetValue(eventName, out thisEvent))
      thisEvent.Invoke();
  }
}
