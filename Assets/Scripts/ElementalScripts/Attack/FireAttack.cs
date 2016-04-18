using UnityEngine;
using System.Collections;

public class FireAttack : BaseAttack {

  private float delay = 0.125f;
  private float dashSpeed = 30f;
  private string weakTag = "Plant Elemental";
  private float atkRadius = 0.5f;

  //===================================================================================================================

  protected override void Awake() {
    base.Awake();
    stunDuration = 0.5f;
    attackCooldown = 1.5f;
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
    bool panicBool = false;
    if(direction == Vector2.zero) {
      direction = -((Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
      panicBool = true;
    }
    // Vector2 direction = -(transform.position - move.Target).normalized;

    yield return new WaitForSeconds(delay);

    sound.playAttack(playerAttack);
    anim.doAttack();
    
    float timeLeft = stunDuration - delay;
    while(timeLeft > 0) {
      timeLeft -= Time.deltaTime;
      transform.Translate(direction * dashSpeed * Time.deltaTime);
      if(panicBool) move.Target = transform.position;
      Collider2D[] candidates = Physics2D.OverlapCircleAll(transform.position, atkRadius);
      foreach(Collider2D candidate in candidates) {
        if(candidate.gameObject.tag == weakTag) {
          ExistenceBase ex = candidate.GetComponent<ExistenceBase>();
          if(!ex.Invulnerable) {
            ex.die();
            if(playerAttack) EventManager.triggerEvent("Player Kill");
          }
        }
      }
      yield return null;
    }
  }
}
