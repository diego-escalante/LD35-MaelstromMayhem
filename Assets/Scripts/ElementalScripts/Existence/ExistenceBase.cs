using UnityEngine;
using System.Collections;

public class ExistenceBase : MonoBehaviour {

  private int currentForm;
  protected GameObject normalPS;
  protected GameObject spawnPS;
  
  public GameObject waterElemental; 
  public GameObject fireElemental;  
  public GameObject plantElemental; 

  protected SoundController sound;

  protected IEnumerator asyncShapeshift;

  protected bool invulnerable = false;
  public bool Invulnerable {get;set;}

  //===================================================================================================================

  protected virtual void Awake() {
    //Get current form.
    if(gameObject.tag == "Water Elemental") currentForm = 0;
    else if(gameObject.tag == "Fire Elemental") currentForm = 1;
    else currentForm = 2;

    normalPS = transform.Find("Shapeshift PS").gameObject;
    spawnPS = transform.Find("Spawn PS").gameObject;
    sound = GetComponent<SoundController>();

    asyncShapeshift = shapeshift();
    StartCoroutine(asyncShapeshift);
  }

  //===================================================================================================================

  public void spawn() {
    StartCoroutine(spawnSequence());
    // Invoke("shapeshift", Random.Range(5f, 10f));
  }

  //===================================================================================================================

  protected virtual IEnumerator spawnSequence(){
    Debug.LogError("This shouldn't happen. Check the children.");
    yield return null;
  }

  //===================================================================================================================

  public virtual void die() {
    sound.playDamage();
  }

  //===================================================================================================================

  public IEnumerator shapeshift() {
    yield return new WaitForSeconds(Random.Range(5f,10f));

    int newForm;
    do {
      newForm = Random.Range(0, 3);
    } while (newForm == currentForm);

    ParticleSystem SPS = spawnPS.GetComponent<ParticleSystem>();

    Color[] cs = {new Color(42/255f,161/255f,180/255f), new Color(255/255f,100/255f,0f), new Color(126/225f, 255/255f, 22/225f)};
    SPS.startColor = cs[newForm];
    SPS.Play();

    yield return new WaitForSeconds(2.5f);

    GameObject newElemental = createElemental(newForm, transform.position);
    spawnPS.transform.parent = newElemental.transform;
    Destroy(spawnPS, 5);
    // newElemental.GetComponent<ExistenceBase>().emitShapeParticles();
    Destroy(gameObject);
  }

  //===================================================================================================================

  private GameObject createElemental(int i, Vector3 v) {
    if(i == 0)      return (GameObject)Instantiate(waterElemental, v, Quaternion.identity);
    else if(i == 1) return (GameObject)Instantiate(fireElemental, v, Quaternion.identity);
    else            return (GameObject)Instantiate(plantElemental, v, Quaternion.identity);
  }

  //===================================================================================================================

  // public void emitShapeParticles(){
  //   normalPS.GetComponent<ParticleSystem>().Play();
  // }
}
