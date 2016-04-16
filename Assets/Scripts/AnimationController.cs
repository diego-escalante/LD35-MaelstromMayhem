using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

  private Animator anim;
  private Movement move;
  private int direction = 2;
  private bool isMoving = false;

  //===================================================================================================================

  private void Start() {
    anim = GetComponent<Animator>();
    move = GetComponent<Movement>();
  }

  //===================================================================================================================

  private void LateUpdate() {

    //Switch directions when needed.
    int newDir = getDirection();
    if(direction != newDir) {
      direction = newDir;
      switch(direction) {
        case 0:
          anim.SetTrigger("switchUp");
          break;
        case 1:
          anim.SetTrigger("switchRight");
          break;
        case 2:
          anim.SetTrigger("switchDown");
          break;
        case 3:
          anim.SetTrigger("switchLeft");
          break;
      }
    }

    //Are we moving right now?
    anim.SetBool("moving", isMoving);
  }

  //===================================================================================================================

  private int getDirection() {
    Vector2 vDir = -(transform.position - move.Target).normalized;

    if(vDir == Vector2.zero) {
      isMoving = false;
      return direction; 
    }

    isMoving = true;
    float angle = Vec2Deg(vDir);
    angle = angleToDir(angle);
    return (int)angle;
  }

  //===================================================================================================================

  public void doAttack(){
    // anim.SetTrigger("attack");
  }

  //===================================================================================================================

  private int angleToDir(float angle) {
    int[] roundedAngles = {1, 0, 3, 2, 1};
    int i = Mathf.RoundToInt(angle / 90);
    return roundedAngles[i];
  }

  //===================================================================================================================

  private float Vec2Deg(Vector2 v, bool isRelative=false){
    float angle = Mathf.Atan2 (v.y, v.x) * Mathf.Rad2Deg;
    if (angle < 0) angle += 360;
    if(isRelative && v.x > 0) angle = 180 - angle;
    return angle;
  }

}
