using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

  private Vector3 target = Vector3.zero;
  private float speed = 10f;
  private float stunTime = 0;

  private SoundController sound;

  public Vector3 Target {get {return target;} set {target = value;}}
  public float StunTime {set {stunTime = value;}}

  //===================================================================================================================

  private void Start() {
    target = transform.position;
    sound = GetComponent<SoundController>();
  }

  //===================================================================================================================

  private void Update() {
    if(stunTime > 0) stunTime -= Time.deltaTime;
    else move();
    keepInsideArena();
  }

  //===================================================================================================================

  private void move() {
    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
  }

  //===================================================================================================================

  private void keepInsideArena() {
    Vector3 pos = transform.position;
    if(pos.x > 25 || pos.x < -25) {
      pos.x = Mathf.Clamp(pos.x, -25, 25);
      transform.position = pos;
    }
    if(pos.y > 25 || pos.y < -25) {
      pos.y = Mathf.Clamp(pos.y, -25, 25);
      transform.position = pos;
    } 
    if(pos.z != 0) {
      Debug.LogWarning("An elemental has depth!");
      pos.z = 0;
      transform.position = pos;
    }
  }

  //===================================================================================================================

  public void die() {
    sound.playDamage();
    if(GetComponent<PlayerAction>()) {
      EventManager.triggerEvent("Player Death");
    }
    else EventManager.triggerEvent("Elemental Death");
    Destroy(gameObject);
  }
}
