using UnityEngine;
using System.Collections;

public class BaseAttack : MonoBehaviour {

  protected Movement move;
  protected SoundController sound;
  protected bool attackReady = true;

  protected float stunDuration = 0;
  protected float attackCooldown = 0;


  //===================================================================================================================

  protected virtual void Start() {
    move = GetComponent<Movement>();
    sound = GetComponent<SoundController>();
  }

  //===================================================================================================================

  public virtual void attack() {
    Debug.LogError("BaseAttack has issued an attack. Wat?");
  }

  //===================================================================================================================

  protected IEnumerator onCooldown() {
    attackReady = false;
    float timeLeft = attackCooldown;
    while(timeLeft > 0) {
      timeLeft -= Time.deltaTime;
      yield return null;
    }
    attackReady = true;
  }
}
