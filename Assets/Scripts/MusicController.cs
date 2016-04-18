using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

  public AudioSource everythingElse;
  public AudioSource onlyDrums;
  private bool playing = false;
  private float pitch = 1;

  //===================================================================================================================

  private void Awake() { DontDestroyOnLoad(transform.gameObject); }

  //===================================================================================================================

  private void Start() {
    StartCoroutine(fadeIn(onlyDrums, false, 2));
  }

  //===================================================================================================================

  private void Update() {
    everythingElse.pitch = pitch;
    onlyDrums.pitch = pitch;
    if(!playing) return;
    pitch += Time.deltaTime / 480;
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

  private IEnumerator pitchReset() {
    float originalPitch = pitch;
    float timeLeft = 0.5f;
    while(timeLeft > 0) {
      timeLeft -= Time.deltaTime;
      pitch = 1 + (originalPitch - 1) * (timeLeft/0.5f);
      yield return null;
    }
    pitch = 1;
  }

  //===================================================================================================================

  private void startGame() {
    playing = true;
    StartCoroutine(fadeIn(everythingElse));
  }

  //===================================================================================================================

  private void playerDeath() {
    playing = false;
    StartCoroutine(pitchReset());
    StartCoroutine(fadeIn(everythingElse, true));
  }
}
