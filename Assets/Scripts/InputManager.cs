using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

  void Update () {
    if(Input.GetButton("Move"))
      EventManager.triggerEvent("Move");

    if(Input.GetButtonDown("Move"))
      EventManager.triggerEvent("StartMove");

    if(Input.GetButtonUp("Move"))
      EventManager.triggerEvent("StopMove");


    if(Input.GetButton("Attack"))
      EventManager.triggerEvent("Attack");
  }
}