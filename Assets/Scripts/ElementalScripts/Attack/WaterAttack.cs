using UnityEngine;
using System.Collections;

public class WaterAttack : BaseAttack {

  public GameObject bullet;

  //===================================================================================================================

  protected override void Awake() {
    base.Awake();
    stunDuration = 0.1f;
    attackCooldown = 0.25f;
  }

  //===================================================================================================================

  public override void attack() {
    if(!attackReady) return;
    move.StunTime = stunDuration;

    sound.playAttack(playerAttack);
    anim.doAttack();

    GameObject b = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
    Vector2 dirdir = -(transform.position - move.Target).normalized;
    if(dirdir == Vector2.zero && playerAttack) dirdir = -((Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
    b.GetComponent<WaterBulletBehavior>().Direction = dirdir;
    if(GetComponent<ExistencePlayer>()) b.GetComponent<WaterBulletBehavior>().PlayerOwned = true;

    StartCoroutine(onCooldown());
  }
}
