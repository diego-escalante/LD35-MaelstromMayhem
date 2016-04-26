using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Highscores : MonoBehaviour {

  private const string privateCode = "PUT PRIVATE CODE HERE";
  private const string publicCode = "571c5d436e51b60f8852abe8";
  private const string webURL = "https://dreamlo.com/lb/";

  public Highscore[] highscoresList;

  private Text hsList;

  public static Highscores singleton = null;  //presistent-singletonify!

  public bool gotScores = false;
  public int lowestScore = 0;

  //===================================================================================================================

  private void Awake() {
    if(!singleton) {
      singleton = this;
      DontDestroyOnLoad(gameObject);
    }
    else Destroy(gameObject);
  }

  //===================================================================================================================

  public void Start() {
    DownloadHighscores();
  }


  //===================================================================================================================

  public void AddNewHighscore(string username, int score) {
    StartCoroutine(UploadNewHighscore(username,score));
  }

  //===================================================================================================================

  private IEnumerator UploadNewHighscore(string username, int score) {
    WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
    yield return www;

    if (string.IsNullOrEmpty(www.error))
      // print ("Upload Successful");
      DownloadHighscores();
    else {
      print ("Error uploading: " + www.error);
    }
  }

  //===================================================================================================================

  public void DownloadHighscores(bool display=false) {
    StartCoroutine("DownloadHighscoresFromDatabase", display);
  }

  //===================================================================================================================

  private IEnumerator DownloadHighscoresFromDatabase(bool display=false) {
    WWW www = new WWW(webURL + publicCode + "/pipe/15");
    yield return www;
    
    if (string.IsNullOrEmpty(www.error)) {
      FormatHighscores(www.text);
      gotScores = true;
    }
    else {
      print ("Error Downloading: " + www.error);
      gotScores = false;
    }

    if(display) {
      string list = "";
      int position = 0;

      foreach(Highscore highscore in highscoresList) {
        position++;
        string posStr = position < 10 ? "0" + position + ". " : position + ". ";
        string scoreStr = highscore.score < 10 ? "0" + highscore.score : highscore.score.ToString();
        list += posStr + scoreStr + " - " + highscore.username + "\n";
      }
      hsList = GameObject.Find("List").GetComponent<Text>();
      hsList.text = list;
    }
  }

  //===================================================================================================================

  private void FormatHighscores(string textStream) {
    string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
    highscoresList = new Highscore[entries.Length];

    for (int i = 0; i <entries.Length; i ++) {
      string[] entryInfo = entries[i].Split(new char[] {'|'});
      string username = entryInfo[0];
      int score = int.Parse(entryInfo[1]);
      highscoresList[i] = new Highscore(username,score);
    }

    lowestScore = entries.Length < 15 ? 1 : highscoresList[entries.Length - 1].score;
  }
}

//!!===================================================================================================================

public struct Highscore {
  public string username;
  public int score;

  public Highscore(string _username, int _score) {
    username = _username;
    score = _score;
  }
}