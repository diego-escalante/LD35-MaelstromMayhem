using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(Movement))]

public class BaseAI : MonoBehaviour {

  private Transform target = null;
  private Movement move;
  private BaseAttack attack;

  protected string weakTag = "";
  protected string strongTag = "";
  protected float attackRange = 0f;

  //===================================================================================================================

  private void Start() {
    move = GetComponent<Movement>();
    attack = GetComponent<BaseAttack>();
  }

  //===================================================================================================================

  private void Update() {
    //Get the nearest target.
    getTarget();
    if(!target) return;  

    //Move towards/attack weak target, run away from strong target. 
    if(target.gameObject.tag == weakTag)
      if(Vector2.Distance(transform.position, target.position) < attackRange) {
        attack.attack();
      } 
      else move.Target = target.position;
    else {
      Vector3 delta = target.position - transform.position;
      move.Target = transform.position - delta;
    }

    //Move randomly if the target is RIGHT ON YOU.
    if(target.position == transform.position) 
      move.Target = transform.position + new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f), 0);
  }

  //===================================================================================================================

  private void getTarget() {
    Transform closest = null;
    float closestDist = Mathf.Infinity;

    GameObject[] weakCandidates = GameObject.FindGameObjectsWithTag(weakTag);
    GameObject[] strongCandidates = GameObject.FindGameObjectsWithTag(strongTag);
    GameObject[] candidates = weakCandidates.Concat(strongCandidates).ToArray();

    foreach(GameObject candidate in candidates) {
      float distance = Vector2.Distance(candidate.transform.position, transform.position);
      if(!closest || closestDist > distance) {
        closest = candidate.transform;
        closestDist = distance;
      }
    }

    target = closest;
  }

  //===================================================================================================================

  private void OnDestroy(){
    //Put this somewhere else.
    // EventManager.triggerEvent("ElementalDeath");
  }
}
