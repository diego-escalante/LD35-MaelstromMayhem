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

  //===================================================================================================================

  private void Start() {
    Destroy(gameObject, lifetime);
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

}

