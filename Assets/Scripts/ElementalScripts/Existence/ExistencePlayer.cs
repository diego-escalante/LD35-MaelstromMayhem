using UnityEngine;
using System.Collections;

public class ExistencePlayer : ExistenceBase {

  private GameObject epicPS;
  private GameObject epicSpawnPS;

  //===================================================================================================================

  protected override void Awake() {
    
    epicPS = transform.Find("Epic PS").gameObject;
    epicSpawnPS = transform.Find("Spawn Epic PS").gameObject;
    base.Awake();
  }

  //===================================================================================================================

  protected override IEnumerator spawnSequence(){
    yield return null;
    sound.playSpawn(true);

    GetComponent<SpriteRenderer>().enabled = false;
    GetComponent<Collider2D>().enabled = false;
    GetComponent<Animator>().enabled = false;
    GetComponent<AnimationController>().enabled = false;
    GetComponent<PlayerAction>().enabled = false;
    GetComponent<Movement>().enabled = false;
    epicSpawnPS.GetComponent<ParticleSystem>().Play();
    
    yield return new WaitForSeconds(3f);
    
    GetComponent<SpriteRenderer>().enabled = true;
    GetComponent<Collider2D>().enabled = true;
    GetComponent<Animator>().enabled = true;
    GetComponent<AnimationController>().enabled = true;
    GetComponent<PlayerAction>().enabled = true;
    GetComponent<Movement>().enabled = true;

    EventManager.triggerEvent("Player Spawned");
  }

  //===================================================================================================================

  public override void die() {
    sound.playDeath(true);
    GameObject.FindWithTag("GameController").GetComponent<GameController>().SpawnPoint = transform.position;
    epicPS.GetComponent<ParticleSystem>().Play();
    epicPS.transform.parent = null;
    
    Destroy(epicPS, 10);
    Destroy(gameObject);
    EventManager.triggerEvent("Player Death");
  }
}
