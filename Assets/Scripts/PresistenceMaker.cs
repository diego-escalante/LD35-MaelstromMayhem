using UnityEngine;
using System.Collections;

public class PresistenceMaker : MonoBehaviour {

  public GameObject ETrigger;
  public GameObject Moosic;

  private void Awake() {
    GameObject g = GameObject.Find("Music Controller");
    if(g == null) {
      g = (GameObject)Instantiate(Moosic);
      g.name = "Music Controller";
    }

    g = GameObject.Find("EventSystem");
    if(g == null) {
      g = (GameObject)Instantiate(ETrigger);
      g.name = "EventSystem";
    }
  }
}
