using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

  public GameObject waterElemental;
  public GameObject fireElemental;
  public GameObject plantElemental;

  private int initialAICount = 5;

  public Vector3 spawnPoint = Vector3.zero;
  public GameObject player;

  //===================================================================================================================

  private void Start() {
    startGame();
  }

  //===================================================================================================================

  private void startGame() {
    createPlayer();
    for(int i = 0; i < initialAICount; i++) createElemental();
  }

  //===================================================================================================================

  private void OnEnable() {
    EventManager.startListening("Elemental Death", createElemental);
    EventManager.startListening("Start Game", startGame);
  }

  //===================================================================================================================

  private void OnDisable() {
    EventManager.stopListening("Elemental Death", createElemental);
    EventManager.stopListening("Start Game", startGame);
  }

  //===================================================================================================================

  private void createElemental() {
    if(!player) return;
    int i = Random.Range(0, 3);
    Vector3 randPos = new Vector3(Random.Range(-25f, 25f), Random.Range(-25f, 25f), 0);

    GameObject g;
    if(i == 0) g = (GameObject)Instantiate(waterElemental, randPos, Quaternion.identity);
    else if(i == 1) g = (GameObject)Instantiate(fireElemental, randPos, Quaternion.identity);
    else g = (GameObject)Instantiate(plantElemental, randPos, Quaternion.identity);
    
    g.transform.Find("Shapeshift PS").GetComponent<ParticleSystem>().Play();
  }

  //===================================================================================================================

  private void createPlayer(){
    int i = Random.Range(0, 3);
    GameObject g;
    if(i == 0) g = (GameObject)Instantiate(waterElemental, spawnPoint, Quaternion.identity);
    else if(i == 1) g = (GameObject)Instantiate(plantElemental, spawnPoint, Quaternion.identity);
    else g = (GameObject)Instantiate(fireElemental, spawnPoint, Quaternion.identity);

    Destroy(g.GetComponent<BaseAI>());
    g.AddComponent<PlayerAction>();
    g.transform.Find("Epic PS").GetComponent<ParticleSystem>().Play();

    player = g;
  }

}
