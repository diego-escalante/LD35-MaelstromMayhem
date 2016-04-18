using UnityEngine;
using System.Collections;

public class BackgroundShifter : MonoBehaviour {

  private float oS = 192; 
  private float oV = 113;
  private float oH = 0;

  private SpriteRenderer bg;

  private int multi = 0;



  //===================================================================================================================

  private void Start() {
    oH = Random.Range(0,360);
    bg = GetComponent<SpriteRenderer>();
  }

  //===================================================================================================================

  private void OnEnable() {
    EventManager.startListening("Player Kill", increaseMultiplier);
    EventManager.startListening("Player Death", resetMultiplier);
  }

  //===================================================================================================================

  private void OnDisable() {
    EventManager.stopListening("Player Kill", increaseMultiplier);
    EventManager.stopListening("Player Death", resetMultiplier);
  }

  //===================================================================================================================

  private void Update() {
    oH += Time.deltaTime * 10 * multi;
    if(oH > 360) oH -= 360;
    bg.color = Color.HSVToRGB(oH/360f, oS/255f, oV/255f);
  }

  //===================================================================================================================

  private void increaseMultiplier() {multi++;}
  private void resetMultiplier() {multi=0;}
}