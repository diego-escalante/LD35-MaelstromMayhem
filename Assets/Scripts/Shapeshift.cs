using UnityEngine;
using System.Collections;

public class Shapeshift : MonoBehaviour {

  private GameObject waterElemental;
  private GameObject fireElemental;
  private GameObject plantElemental;

  private const int WATER = 0;
  private const int FIRE = 1;
  private const int PLANT = 2;
  private int currentForm;

  private bool isPlayer = false;

  //===================================================================================================================

  private void Start() {
    isPlayer = (GetComponent<PlayerAction>());
    waterElemental = Resources.Load("Water Elemental") as GameObject;
    fireElemental = Resources.Load("Fire Elemental") as GameObject;
    plantElemental = Resources.Load("Plant Elemental") as GameObject;

    string formTag = gameObject.tag;
    switch(formTag) {
      case "Water Elemental":
        currentForm = WATER;
        break;
      case "Fire Elemental":
        currentForm = FIRE;
        break;
      case "Plant Elemental":
        currentForm = PLANT;
        break;
      default:
        Debug.LogError("Elemental tag not found!");
        break;
    }

    StartCoroutine(shapeShift());
  }

  //===================================================================================================================

  private IEnumerator shapeShift() {
    while(true) {
      yield return new WaitForSeconds(Random.Range(5f, 15f));

      int i;
      do {
        i = Random.Range(0,3);
      } while(i == currentForm);

      GameObject chosenElemental;

      if(i == 0) 
        chosenElemental = waterElemental;
      else if(i == 1)
        chosenElemental = fireElemental;
      else 
        chosenElemental = plantElemental;
      
      GameObject newGuy = Instantiate(chosenElemental, transform.position, Quaternion.identity) as GameObject;

      if(isPlayer) {
        Destroy(newGuy.GetComponent<BaseAI>());
        newGuy.AddComponent<PlayerAction>();
      }

      Destroy(gameObject);
    }
  }

}
