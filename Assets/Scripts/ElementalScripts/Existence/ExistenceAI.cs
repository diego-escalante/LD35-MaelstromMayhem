using UnityEngine;
using System.Collections;

public class ExistenceAI : ExistenceBase {

  private void OnEnable() {
    EventManager.startListening("Player Death", autoDie);
  }

  //===================================================================================================================

  private void OnDisable() {
    EventManager.stopListening("Player Death", autoDie); 
  }

  //===================================================================================================================

  protected override IEnumerator spawnSequence(){
    // yield return new WaitForSeconds(5f);
    yield return null;
    normalPS.GetComponent<ParticleSystem>().Play();
  }

  //===================================================================================================================

  private void autoDie(){
    GetComponent<BaseAttack>().enabled = false;
    GetComponent<Movement>().enabled = false;
    GetComponent<Animator>().enabled = false;
    StopCoroutine(asyncShapeshift);
    Invoke("die", Random.Range(1f, 3f));
  }

  //===================================================================================================================

  public override void die() {
    base.die();
    normalPS.GetComponent<ParticleSystem>().Play();
    normalPS.transform.parent = null;
    EventManager.triggerEvent("Elemental Death");
    Destroy(normalPS, 4);
    Destroy(gameObject);
  }
}
