using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HSUI : MonoBehaviour {

  private Highscores hs;
  private Text text;

  //===================================================================================================================

  private void Start() {
    hs = GameObject.Find("Highscores").GetComponent<Highscores>();
    text = GetComponent<Text>();
    displayScores();  
  }

  //===================================================================================================================

  private void displayScores() {
    text.text = "Downloading highscores...";
    hs.DownloadHighscores(true);
  }
}
