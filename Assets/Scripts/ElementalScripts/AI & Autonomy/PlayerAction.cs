using UnityEngine;
using System.Collections;

public class PlayerAction : MonoBehaviour {

  private Movement move;
  private BaseAttack attack;
  private UIScript UI;

  //===================================================================================================================

  private void Start() {
    Camera.main.GetComponent<CamMovement>().Target = transform;
    move = GetComponent<Movement>();
    attack = GetComponent<BaseAttack>();
    UI = GameObject.FindWithTag("UI").GetComponent<UIScript>();
  }

  //===================================================================================================================

  private void OnEnable() {
    EventManager.startListening("Move", setTarget);
    EventManager.startListening("StopMove", stopTarget);
    EventManager.startListening("Attack", doAttack);
  }

  //===================================================================================================================

  private void OnDisable() {
    EventManager.stopListening("Move", setTarget);
    EventManager.stopListening("StopMove", stopTarget);
    EventManager.stopListening("Attack", doAttack);
  }

  //===================================================================================================================

  private void Update() {
    UI.UpdateCooldown(attack.cooldownLeft());
  }

  //===================================================================================================================

  private void setTarget() {
    Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    target.z = transform.position.z;
    if(move) move.Target = target;
  }

  //===================================================================================================================

  private void stopTarget() {
    if(move) move.Target = transform.position;
  }

  //===================================================================================================================

  private void doAttack() {
    attack.attack();
  }
}
