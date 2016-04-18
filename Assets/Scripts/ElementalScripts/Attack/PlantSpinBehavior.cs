using UnityEngine;
using System.Collections;

public class PlantSpinBehavior : MonoBehaviour {

  private float lifetime = 0.5f;
  // private float speed = 25f;
  private float spinRadius = 3f;
  // private Vector2 direction = Vector2.right;

  private string weakTag = "Water Elemental";

  private bool playerOwned = false;

  // public Vector2 Direction {set {direction = value;}}
  public bool PlayerOwned {set {playerOwned = value;}}

  private Animator anim;
  public Vector2 direction = Vector2.zero;

  //===================================================================================================================

  private void Awake() {
    anim = GetComponent<Animator>();
  }

  //===================================================================================================================

  private void Start() {
    Destroy(gameObject, lifetime);
    getDirection();
  }

  //===================================================================================================================

  private void Update() {
    // transform.position = transform.position + (Vector3)direction * speed * Time.deltaTime;

    Collider2D[] candidates = Physics2D.OverlapCircleAll(transform.position, spinRadius);
    foreach(Collider2D candidate in candidates) {
      if(candidate.gameObject.tag == weakTag) {
        candidate.GetComponent<ExistenceBase>().die();
        if(playerOwned) EventManager.triggerEvent("Player Kill");
      }
    }
  }

  //===================================================================================================================

  private void getDirection() {
    // Vector2 vDir = -(transform.position - move.Target).normalized;

    // if(vDir == Vector2.zero) {
    //   isMoving = false;
    //   return direction; 
    // }
    float angle = Vec2Deg(direction);
    angle = angleToDir(angle);
    
    switch((int)angle) {
      case 0:
        anim.SetTrigger("Up");
        break;
      case 1:
        anim.SetTrigger("Right");
        break;
      case 2:
        anim.SetTrigger("Down");
        break;
      case 3:
        anim.SetTrigger("Left");
        break;
    };
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

