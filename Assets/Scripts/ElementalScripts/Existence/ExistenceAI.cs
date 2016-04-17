﻿using UnityEngine;
using System.Collections;

public class ExistenceAI : ExistenceBase {

  protected override void Awake() {

    waterElemental = Resources.Load("AI/Water AI") as GameObject;
    fireElemental = Resources.Load("AI/Fire AI") as GameObject;
    plantElemental = Resources.Load("AI/Plant AI") as GameObject;
    base.Awake();
  }

  //===================================================================================================================

  protected override IEnumerator spawnSequence(){
    // yield return new WaitForSeconds(5f);
    yield return null;
    normalPS.GetComponent<ParticleSystem>().Play();
  }

  //===================================================================================================================

  public override void die() {
    base.die();
    normalPS.GetComponent<ParticleSystem>().Play();
    normalPS.transform.parent = null;
    
    Destroy(normalPS, 4);
    Destroy(gameObject);

    EventManager.triggerEvent("Elemental Death");
  }
}
