using UnityEngine;
using System.Collections;

public class BaseAttack : MonoBehaviour {

  protected Movement move;
  protected SoundController sound;
  protected AnimationController anim;
  protected bool attackReady = true;

  protected float stunDuration = 0;
  private float timeLeft = 0;
  protected float attackCooldown = 0;

  protected bool playerAttack = false;

  //===================================================================================================================

  protected virtual void Awake() {
    move = GetComponent<Movement>();
    sound = GetComponent<SoundController>();
    anim = GetComponent<AnimationController>();

    if(GetComponent<ExistencePlayer>()) playerAttack = true;
  }

  //===================================================================================================================

  public virtual void attack() {
    Debug.LogError("BaseAttack has issued an attack. Wat?");
  }

  //===================================================================================================================

  protected IEnumerator onCooldown() {
    attackReady = false;
    timeLeft = attackCooldown;
    while(timeLeft > 0) {
      timeLeft -= Time.deltaTime;
      yield return null;
    }
    attackReady = true;
  }

  //===================================================================================================================

  public float cooldownLeft() {
    return timeLeft/attackCooldown;
  }
}
