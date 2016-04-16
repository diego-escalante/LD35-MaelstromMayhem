using UnityEngine;
using System.Collections;

public class WaterAttack : BaseAttack {

  private GameObject bullet;

  //===================================================================================================================

  private void Awake() {
    stunDuration = 0.25f;
    attackCooldown = 0.25f;
  }

  //===================================================================================================================

  protected override void Start() {
    base.Start();
    bullet = Resources.Load("Water Bullet") as GameObject;
  }

  //===================================================================================================================

  public override void attack() {
    if(!attackReady) return;
    move.StunTime = stunDuration;

    GameObject b = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
    b.GetComponent<WaterBulletBehavior>().Direction = -(transform.position - move.Target).normalized;

    StartCoroutine(onCooldown());
  }
}
