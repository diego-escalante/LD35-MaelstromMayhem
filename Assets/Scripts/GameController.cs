using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

  public GameObject waterElemental;
  public GameObject fireElemental;
  public GameObject plantElemental;

  public GameObject waterPlayer;
  public GameObject firePlayer;
  public GameObject plantPlayer;

  private bool canCreateAI = false;

  private int initialAICount = 5;
  private float spawnRate = 2f;

  private Vector3 spawnPoint = new Vector3(0.75f, 0.5f, 0);

  public Vector3 SpawnPoint {set {spawnPoint=value;}}

  //===================================================================================================================

  private void Start() {
    startGame();
    EventManager.triggerEvent("One Time Thing Everything Is Getting Messy");
  }

  //===================================================================================================================

  private void startGame() {
    //Kill any preexisting elementals on the board.
    ExistenceAI[] elementals = FindObjectsOfType(typeof(ExistenceAI)) as ExistenceAI[];
    foreach(ExistenceAI elemental in elementals) elemental.die();
    createPlayer();
  }

  //===================================================================================================================

  private void endGame() {
    canCreateAI = false;
    CancelInvoke();
  }

  //===================================================================================================================

  private void spawnElementals() {
    canCreateAI = true;
    for(int i = 0; i < initialAICount; i++) createElemental();
    InvokeRepeating("createElemental", spawnRate, spawnRate);
  }

  //===================================================================================================================

  private void OnEnable() {
    EventManager.startListening("Elemental Death", createElemental);
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
    if(!canCreateAI) return;
    int i = Random.Range(0, 3);
    Vector3 randPos = new Vector3(Random.Range(-25f, 25f), Random.Range(-25f, 25f), 0);

    GameObject g;
    if(i == 0) g = (GameObject)Instantiate(waterElemental, randPos, Quaternion.identity);
    else if(i == 1) g = (GameObject)Instantiate(fireElemental, randPos, Quaternion.identity);
    else g = (GameObject)Instantiate(plantElemental, randPos, Quaternion.identity);
    g.GetComponent<ExistenceBase>().spawn();

  }

  //===================================================================================================================

  private void createPlayer(){
    int i = Random.Range(0, 3);

    GameObject g;
    if(i == 0) g = (GameObject)Instantiate(waterPlayer, spawnPoint, Quaternion.identity);
    else if(i == 1) g = (GameObject)Instantiate(plantPlayer, spawnPoint, Quaternion.identity);
    else g = (GameObject)Instantiate(firePlayer, spawnPoint, Quaternion.identity);
    g.GetComponent<ExistenceBase>().spawn();
    GameObject.FindWithTag("UI").GetComponent<UIScript>().swapIcon(i);
  }

}
