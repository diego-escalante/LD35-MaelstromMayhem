using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

  public GameObject waterElemental;
  public GameObject fireElemental;
  public GameObject plantElemental;

  private int initialAICount = 5;

  //===================================================================================================================

  private void Start() {
    for(int i = 0; i < initialAICount; i++) createElemental();
  }

  //===================================================================================================================

  private void OnEnable() {
    EventManager.startListening("Elemental Death", createElemental);
  }

  //===================================================================================================================

  private void OnDisable() {
    EventManager.stopListening("Elemental Death", createElemental);
  }

  //===================================================================================================================

  private void createElemental() {
    int i = Random.Range(0, 3);
    Vector3 randPos = new Vector3(Random.Range(-25f, 25f), Random.Range(-25f, 25f), 0);
    switch(i) {
      case 0:
        Instantiate(waterElemental, randPos, Quaternion.identity);
        break;
      case 1:
        Instantiate(fireElemental, randPos, Quaternion.identity);
        break;
      case 2:
        Instantiate(plantElemental, randPos, Quaternion.identity);
        break;
    }
  }

}
