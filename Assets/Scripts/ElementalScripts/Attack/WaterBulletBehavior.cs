using UnityEngine;
using System.Collections;

public class WaterBulletBehavior : MonoBehaviour {

  private float lifetime = 0.75f;
  private float speed = 25f;
  private float bulletRadius = 0.25f;
  private Vector2 direction = Vector2.right;

  private string weakTag = "Fire Elemental";

  private bool playerOwned = false;

  public Vector2 Direction {set {direction = value;}}
  public bool PlayerOwned {set {playerOwned = value;}}

  //===================================================================================================================

  private void Start() {
    Destroy(gameObject, lifetime);
  }

  //===================================================================================================================

  private void Update() {
    transform.position = transform.position + (Vector3)direction * speed * Time.deltaTime;

    Collider2D[] candidates = Physics2D.OverlapCircleAll(transform.position, bulletRadius);
    foreach(Collider2D candidate in candidates) {
      if(candidate.gameObject.tag == weakTag) {
        candidate.GetComponent<ExistenceBase>().die();
        if(playerOwned) EventManager.triggerEvent("Player Kill");
      }
    }
  }

}
