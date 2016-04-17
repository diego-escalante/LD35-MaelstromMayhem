using UnityEngine;
using System.Collections;

public class ExistenceBase : MonoBehaviour {

  private int currentForm;
  protected GameObject normalPS;
  
  protected GameObject waterElemental; 
  protected GameObject fireElemental;  
  protected GameObject plantElemental; 

  private SoundController sound;

  //===================================================================================================================

  protected virtual void Awake() {
    //Get current form.
    if(gameObject.tag == "Water Elemental") currentForm = 0;
    else if(gameObject.tag == "Fire Elemental") currentForm = 1;
    else currentForm = 2;

    normalPS = transform.Find("Shapeshift PS").gameObject;
    sound = GetComponent<SoundController>();
  }

  //===================================================================================================================

  public void spawn() {
    StartCoroutine(spawnSequence());
    Invoke("shapeshift", Random.Range(5f, 15f));
  }

  //===================================================================================================================

  protected virtual IEnumerator spawnSequence(){
    Debug.LogError("This shouldn't happen.");
    yield return null;
  }

  //===================================================================================================================

  public virtual void die() {
    sound.playDamage();
  }

  //===================================================================================================================

  private void shapeshift() {
    int newForm;
    do {
      newForm = Random.Range(0, 3);
    } while (newForm == currentForm);

    GameObject newElemental = createElemental(newForm, transform.position);
    newElemental.GetComponent<ExistenceBase>().emitShapeParticles();
    Destroy(gameObject);
  }

  //===================================================================================================================

  private GameObject createElemental(int i, Vector3 v) {
    if(i == 0)      return (GameObject)Instantiate(waterElemental, v, Quaternion.identity);
    else if(i == 1) return (GameObject)Instantiate(fireElemental, v, Quaternion.identity);
    else            return (GameObject)Instantiate(plantElemental, v, Quaternion.identity);
  }

  //===================================================================================================================

  public void emitShapeParticles(){
    normalPS.GetComponent<ParticleSystem>().Play();
  }
}
