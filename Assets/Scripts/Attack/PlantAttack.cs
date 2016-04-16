using UnityEngine;
using System.Collections;

public class PlantAttack : BaseAttack {

  private float plantAtkRadius = 3f;
  private string weakTag = "Water Elemental";

  //===================================================================================================================

  private void Awake() {
    stunDuration = 0.5f;
    attackCooldown = 0.5f;
  }

  //===================================================================================================================

  public override void attack() {
    if(!attackReady) return;

    sound.playAttack();
    anim.doAttack();
    
    move.StunTime = stunDuration;
    Collider2D[] candidates = Physics2D.OverlapCircleAll(transform.position, plantAtkRadius);
    foreach(Collider2D candidate in candidates) {
      if(candidate.gameObject.tag == weakTag) {
        candidate.GetComponent<Movement>().die();
      }
    }

    StartCoroutine(onCooldown());
  }
}

