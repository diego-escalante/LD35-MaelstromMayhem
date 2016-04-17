using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

  public GameObject waterElemental;
  public GameObject fireElemental;
  public GameObject plantElemental;

  public GameObject waterPlayer;
  public GameObject firePlayer;
  public GameObject plantPlayer;

  private int initialAICount = 5;

  private Vector3 spawnPoint = Vector3.zero;

  public Vector3 SpawnPoint {set {spawnPoint=value;}}

  //===================================================================================================================

  private void Start() {
    startGame();
  }

  //===================================================================================================================

  private void startGame() {
    //Kill any preexisting elementals on the board.
    StopCoroutine(killElementals());
    ExistenceAI[] elementals = FindObjectsOfType(typeof(ExistenceAI)) as ExistenceAI[];
    foreach(ExistenceAI elemental in elementals) elemental.die();

    EventManager.startListening("Elemental Death", createElemental);
    createPlayer();
  }

  //===================================================================================================================

  private void endGame() {
    EventManager.stopListening("Elemental Death", createElemental);
    StartCoroutine(killElementals());
  }

  //===================================================================================================================

  private void spawnElementals() {
    for(int i = 0; i < initialAICount; i++) createElemental();
  }

  //===================================================================================================================

  private void OnEnable() {
    EventManager.startListening("Start Game", startGame);
    EventManager.startListening("Player Spawned", spawnElementals);
    EventManager.startListening("Player Death", endGame);
  }

  //===================================================================================================================

  private void OnDisable() {
    EventManager.stopListening("Elemental Death", createElemental);
    EventManager.stopListening("Start Game", startGame);
    EventManager.stopListening("Player Spawned", spawnElementals);
    EventManager.stopListening("Player Death", endGame);
  }

  //===================================================================================================================

  private void createElemental() {
    int i = Random.Range(0, 3);
    Vector3 randPos = new Vector3(Random.Range(-25f, 25f), Random.Range(-25f, 25f), 0);

    GameObject g;
    if(i == 0) g = (GameObject)Instantiate(waterElemental, randPos, Quaternion.identity);
    else if(i == 1) g = (GameObject)Instantiate(fireElemental, randPos, Quaternion.identity);
    else g = (GameObject)Instantiate(plantElemental, randPos, Quaternion.identity);
    g.GetComponent<ExistenceBase>().spawn();

  }

  //===================================================================================================================

  private IEnumerator killElementals() {
    yield return new WaitForSeconds(1f);
    ExistenceAI[] elementals = FindObjectsOfType(typeof(ExistenceAI)) as ExistenceAI[];
    foreach(ExistenceAI elemental in elementals) {
      yield return new WaitForSeconds(0.25f);
      if(elemental) elemental.die();
    }

    elementals = FindObjectsOfType(typeof(ExistenceAI)) as ExistenceAI[];
    foreach(ExistenceAI elemental in elementals) elemental.die();
  }

  //===================================================================================================================

  private void createPlayer(){
    int i = Random.Range(0, 3);

    GameObject g;
    if(i == 0) g = (GameObject)Instantiate(waterPlayer, spawnPoint, Quaternion.identity);
    else if(i == 1) g = (GameObject)Instantiate(plantPlayer, spawnPoint, Quaternion.identity);
    else g = (GameObject)Instantiate(firePlayer, spawnPoint, Quaternion.identity);
    g.GetComponent<ExistenceBase>().spawn();
  }

}
