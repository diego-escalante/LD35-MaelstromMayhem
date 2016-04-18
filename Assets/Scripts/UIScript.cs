using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIScript : MonoBehaviour {

  private Image fader;
  private Text scoreText;
  private Text finalScoreText;
  private Image cooldown;
  private GameObject backButton;
  private GameObject againButton;
  private int score = 0;

  public Sprite waterIcon;
  public Sprite fireIcon;
  public Sprite plantIcon;

  public GameObject highPrefab;
  private HighTracker hs;
  private bool newBest = false;

  //===================================================================================================================

  private void Start() {
    GameObject hsObj = GameObject.Find("High Score(Clone)");
    if(!hsObj) hsObj = (GameObject)Instantiate(highPrefab);
    hs = hsObj.GetComponent<HighTracker>();

    fader = transform.Find("Fader").GetComponent<Image>();
    cooldown = transform.Find("Cooldown").GetComponent<Image>();
    scoreText = transform.Find("Score").GetComponent<Text>();
    scoreText.text = "Best: " + zerorize(hs.HighScore) + "\nScore: " + zerorize(0);
    finalScoreText = transform.Find("Final Score").GetComponent<Text>();

    backButton = transform.Find("Back Button").gameObject;
    againButton = transform.Find("Again Button").gameObject;
    backButton.SetActive(false);
    againButton.SetActive(false);

    fader.color = Color.black;
    StartCoroutine(fadeIn());
  }

  //===================================================================================================================

  private void OnEnable() {
    EventManager.startListening("Player Kill", scoreIncrease);
    EventManager.startListening("Player Death", delayPause);
  }

  //===================================================================================================================

  private void OnDisable() {
    EventManager.stopListening("Player Kill", scoreIncrease);
    EventManager.stopListening("Player Death", delayPause);
  }

  //===================================================================================================================

  private IEnumerator fadeIn(bool reverse=false, float fraction=1) {
    float duration = 0.25f;
    float timeElapsed = 0;

    Color original = fader.color;

    while(timeElapsed < duration) {
      timeElapsed += Time.deltaTime;
      if(!reverse) fader.color = Color.Lerp(original, Color.clear, timeElapsed/duration * fraction);
      else fader.color = Color.Lerp(original, Color.black, timeElapsed/duration * fraction);
      yield return null;
    }
  }

  //===================================================================================================================

  private void scoreIncrease() {
    score++;
    if(score > hs.HighScore) {
      newBest = true;
      hs.HighScore = score;
    }
    scoreText.text = "Best: " + zerorize(hs.HighScore) + "\nScore: " + zerorize(score);
  }

  //===================================================================================================================

  private void delayPause() {
    Invoke("pause", 1f);
  }

  //===================================================================================================================

  private void pause() {
    StartCoroutine(fadeIn(true, 0.5f));
    cooldown.enabled = false;
    scoreText.enabled = false;

    if(newBest) finalScoreText.text = "<color=yellow>New record!\nFinal Score: " + zerorize(score) + "</color>\nClick to Restart";
    else finalScoreText.text = "Final Score: " + zerorize(score) + "\nClick to Restart";
    newBest = false;
    scoreText.text = "Best: " + zerorize(hs.HighScore) + "\nScore: " + zerorize(0);
    backButton.SetActive(true);
    againButton.SetActive(true);
  }

  //===================================================================================================================

  public void UpdateCooldown(float fraction) {
    cooldown.fillAmount = 1 - fraction;

    if(cooldown.fillAmount >= 1) cooldown.color = Color.white;
    else {
      Color c = Color.white;
      c.a = 0.5f;
      cooldown.color = c;
    }
  }

  //===================================================================================================================

  public void clickBack() {
    StartCoroutine(goBack());
  }

  //===================================================================================================================

  public void clickAgain() {
    EventManager.triggerEvent("Start Game");
    StartCoroutine(fadeIn());
    cooldown.enabled = true;
    scoreText.enabled = true;
    score = 0;

    finalScoreText.text = "";
    backButton.SetActive(false);
    againButton.SetActive(false);
  }
	
  //===================================================================================================================

  private IEnumerator goBack(){
    finalScoreText.text = "";
    backButton.SetActive(false);
    againButton.SetActive(false);
    StartCoroutine(fadeIn(true));
    yield return new WaitForSeconds(0.25f);
    SceneManager.LoadScene("intro");
  }

  //===================================================================================================================

  private string zerorize(int score) {
    if(score < 10) return "0000000" + score;
    else return "000000" + score;
  }

  //===================================================================================================================

  public void swapIcon(int i) {
    if(i == 0) cooldown.sprite = waterIcon;
    else if(i == 1) cooldown.sprite = fireIcon;
    else cooldown.sprite = plantIcon;
    UpdateCooldown(0);
  }
}
