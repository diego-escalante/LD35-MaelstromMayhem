using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

  public AudioSource everythingElse;
  public AudioSource onlyDrums;

  //===================================================================================================================

  private void Awake() { DontDestroyOnLoad(transform.gameObject); }

  //===================================================================================================================

  private void Start() {
    StartCoroutine(fadeIn(onlyDrums, false, 2));
  }

  //===================================================================================================================

  private void OnEnable() {
    EventManager.startListening("Player Spawned", startGame);
    EventManager.startListening("Player Death", playerDeath);    
  }

  //===================================================================================================================

  private void OnDisable() {
    EventManager.stopListening("Player Spawned", startGame);
    EventManager.stopListening("Player Death", playerDeath);  
  }

  //===================================================================================================================

  private IEnumerator fadeIn(AudioSource audio, bool reverse=false, float duration=0.25f) {
    float timeLeft = duration;
    audio.volume = !reverse ? 0 : 1;
    while(timeLeft > 0) {
      audio.volume = !reverse ? 1 - timeLeft/duration : timeLeft/duration;
      timeLeft -= Time.deltaTime;
      yield return null;
    }
    audio.volume = !reverse ? 1 : 0;
  }

  //===================================================================================================================

  private void startGame() {
    StartCoroutine(fadeIn(everythingElse));
  }

  //===================================================================================================================

  private void playerDeath() {
    StartCoroutine(fadeIn(everythingElse, true));
  }
}
