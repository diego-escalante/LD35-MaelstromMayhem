using UnityEngine;
using System.Collections;

public class PlayerAction : MonoBehaviour {

  private Movement move;
  private BaseAttack attack;

  //===================================================================================================================

  private void Start() {
    Camera.main.GetComponent<CamMovement>().Target = transform;
    move = GetComponent<Movement>();
    attack = GetComponent<BaseAttack>();
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

  private void setTarget() {
    Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    target.z = transform.position.z;
    move.Target = target;
  }

  //===================================================================================================================

  private void stopTarget() {
    move.Target = transform.position;
  }

  //===================================================================================================================

  private void doAttack() {
    attack.attack();
  }
}
