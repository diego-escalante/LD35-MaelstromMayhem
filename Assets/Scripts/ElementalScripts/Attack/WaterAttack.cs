using UnityEngine;
using System.Collections;

public class WaterAttack : BaseAttack {

  private GameObject bullet;

  //===================================================================================================================

  protected override void Awake() {
    base.Awake();
    stunDuration = 0.25f;
    attackCooldown = 0.25f;
  }

  //===================================================================================================================

  protected void Start() {
    bullet = Resources.Load("Water Bullet") as GameObject;
  }

  //===================================================================================================================

  public override void attack() {
    if(!attackReady) return;
    move.StunTime = stunDuration;

    sound.playAttack();
    anim.doAttack();

    GameObject b = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
    b.GetComponent<WaterBulletBehavior>().Direction = -(transform.position - move.Target).normalized;

    StartCoroutine(onCooldown());
  }
}
