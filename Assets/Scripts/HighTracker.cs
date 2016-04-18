using UnityEngine;
using System.Collections;

public class HighTracker : MonoBehaviour {

  private int highScore = 0;

  public int HighScore {get {return highScore;} set {highScore=value;}}

  //===================================================================================================================

  private void Awake() {
    DontDestroyOnLoad(transform.gameObject);
  }

}
