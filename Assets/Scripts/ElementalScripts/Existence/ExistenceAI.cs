using UnityEngine;
using System.Collections;

public class ExistenceAI : ExistenceBase {

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
