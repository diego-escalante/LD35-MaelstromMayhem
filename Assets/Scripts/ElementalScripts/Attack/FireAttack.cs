using UnityEngine;
using System.Collections;

public class FireAttack : BaseAttack {

  private float delay = 0.125f;
  private float dashSpeed = 30f;
  private string weakTag = "Plant Elemental";
  private float atkRadius = 0.5f;

  //===================================================================================================================

  private void Awake() {
    stunDuration = 0.5f;
    attackCooldown = 0.5f;
  }

  //===================================================================================================================

  public override void attack() {
    if(!attackReady) return;
    move.StunTime = stunDuration;

    StartCoroutine(dash());
    StartCoroutine(onCooldown());
  }

  //===================================================================================================================

  private IEnumerator dash() {
    Vector2 direction = -(transform.position - move.Target).normalized;

    yield return new WaitForSeconds(delay);

    sound.playAttack();
    anim.doAttack();
    
    float timeLeft = stunDuration - delay;
    while(timeLeft > 0) {
      timeLeft -= Time.deltaTime;
      transform.Translate(direction * dashSpeed * Time.deltaTime);
      Collider2D[] candidates = Physics2D.OverlapCircleAll(transform.position, atkRadius);
      foreach(Collider2D candidate in candidates) {
        if(candidate.gameObject.tag == weakTag) {
          candidate.GetComponent<Movement>().die();
        }
      }
      yield return null;
    }
  }
}
