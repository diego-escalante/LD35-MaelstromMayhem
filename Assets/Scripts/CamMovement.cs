using UnityEngine;
using System.Collections;

public class CamMovement : MonoBehaviour {

  private Transform target;

  public Transform Target {set {target = value;}}

  //===================================================================================================================

  private void LateUpdate() {
    if(target) transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
  }

}
