using UnityEngine;
using System.Collections;

public class PlantAttack : BaseAttack {

  // private float plantAtkRadius = 3f;
  // private string weakTag = "Water Elemental";

  private ExistenceBase ex;
  public GameObject spin;

  //===================================================================================================================

  protected override void Awake() {
    base.Awake();
    stunDuration = 0.5f;
    attackCooldown = 1f;

    ex = GetComponent<ExistenceBase>();
  }

  //===================================================================================================================

  public override void attack() {
    if(!attackReady) return;

    sound.playAttack(playerAttack);
    anim.doAttack();
    
    move.StunTime = stunDuration;

    GameObject b = (GameObject)Instantiate(spin, transform.position, Quaternion.identity);
    // b.GetComponent<PlantSpinBehavior>().Direction = -(transform.position - move.Target).normalized;
    if(GetComponent<ExistencePlayer>()) b.GetComponent<PlantSpinBehavior>().PlayerOwned = true;

    // Collider2D[] candidates = Physics2D.OverlapCircleAll(transform.position, plantAtkRadius);
    // foreach(Collider2D candidate in candidates) {
    //   if(candidate.gameObject.tag == weakTag) {
    //     candidate.GetComponent<ExistenceBase>().die();
    //     if(playerAttack) EventManager.triggerEvent("Player Kill");
    //   }
    // }

    StartCoroutine(vulnerability());
    StartCoroutine(onCooldown());
  }

  //===================================================================================================================

  private IEnumerator vulnerability() {
    ex.Invulnerable = true;
    yield return new WaitForSeconds(stunDuration);
    ex.Invulnerable = false;
  }
}

