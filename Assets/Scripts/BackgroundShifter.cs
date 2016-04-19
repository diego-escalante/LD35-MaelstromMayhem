using UnityEngine;
using System.Collections;

public class BackgroundShifter : MonoBehaviour {

  private float oS = 192; 
  private float oV = 113;
  private float oH = 0;

  private SpriteRenderer bg;

    private float multi = 1;
    private bool isPlaying = false;


  //===================================================================================================================

  private void Start() {
    oH = Random.Range(0,360);
    bg = GetComponent<SpriteRenderer>();
    bg.color = Color.HSVToRGB(oH/360f, oS/255f, oV/255f);
    // transform.Rotate(0,0,Random.Range(0,360));
  }

  //===================================================================================================================

  private void OnEnable() {
    EventManager.startListening("Player Spawned", increaseMultiplier);
    EventManager.startListening("Player Death", resetMultiplier);
  }

  //===================================================================================================================

  private void OnDisable() {
    EventManager.stopListening("Player Spawned", increaseMultiplier);
    EventManager.stopListening("Player Death", resetMultiplier);
  }

  //===================================================================================================================

  private void Update() {
    if(!isPlaying) return;
    oH += Time.deltaTime * multi;
    multi += Time.deltaTime/10;
    if(oH > 360) oH -= 360;
    bg.color = Color.HSVToRGB(oH/360f, oS/255f, oV/255f);
    transform.Rotate(0,0,-Time.deltaTime * multi/2);
  }

  //===================================================================================================================

  private IEnumerator pitchReset() {
    float originalPitch = multi;
    float timeLeft = 1f;
    while(timeLeft > 0) {
      timeLeft -= Time.deltaTime;
      multi = 1 + (originalPitch - 1) * (timeLeft/0.5f);
      yield return null;
    }
    multi = 1;
    isPlaying = false;
  }

  //===================================================================================================================

  private void increaseMultiplier() {isPlaying = true;}
  private void resetMultiplier() {StartCoroutine(pitchReset());}
}