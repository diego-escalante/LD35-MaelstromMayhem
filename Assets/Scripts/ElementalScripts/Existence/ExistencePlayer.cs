using UnityEngine;
using System.Collections;

public class ExistencePlayer : ExistenceBase {

  private GameObject epicPS;

  //===================================================================================================================

  protected override void Awake() {
    
    epicPS = transform.Find("Epic PS").gameObject;
    base.Awake();
  }

  //===================================================================================================================

  protected override IEnumerator spawnSequence(){
    yield return null;
    epicPS.GetComponent<ParticleSystem>().Play();
    EventManager.triggerEvent("Player Spawned");
  }

  //===================================================================================================================

  public override void die() {
    base.die();
    GameObject.FindWithTag("GameController").GetComponent<GameController>().SpawnPoint = transform.position;
    epicPS.GetComponent<ParticleSystem>().Play();
    epicPS.transform.parent = null;
    
    Destroy(epicPS, 10);
    Destroy(gameObject);

    EventManager.triggerEvent("Player Death");
  }
}
