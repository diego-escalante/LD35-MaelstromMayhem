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
    GetComponent<SpriteRenderer>().enabled = false;
    GetComponent<Collider2D>().enabled = false;
    GetComponent<Animator>().enabled = false;
    GetComponent<AnimationController>().enabled = false;
    GetComponent<BaseAI>().enabled = false;
    GetComponent<Movement>().enabled = false;
    spawnPS.GetComponent<ParticleSystem>().Play();
    
    yield return new WaitForSeconds(2.75f);
    
    // normalPS.GetComponent<ParticleSystem>().Play();
    GetComponent<SpriteRenderer>().enabled = true;
    GetComponent<Collider2D>().enabled = true;
    GetComponent<Animator>().enabled = true;
    GetComponent<AnimationController>().enabled = true;
    GetComponent<BaseAI>().enabled = true;
    GetComponent<Movement>().enabled = true;
  }

  //===================================================================================================================

  private void autoDie(){
    GetComponent<BaseAttack>().enabled = false;
    GetComponent<Movement>().enabled = false;
    GetComponent<Animator>().enabled = false;
    StopCoroutine(asyncShapeshift);
    Invoke("die", Random.Range(1f, 1.5f));
  }

  //===================================================================================================================

  public override void die() {
    sound.playDamage();
    normalPS.GetComponent<ParticleSystem>().Play();
    normalPS.transform.parent = null;
    EventManager.triggerEvent("Elemental Death");
    Destroy(normalPS, 4);
    Destroy(gameObject);
  }
}
