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

  //===================================================================================================================

  private void Start() {
    fader = transform.Find("Fader").GetComponent<Image>();
    cooldown = transform.Find("Cooldown").GetComponent<Image>();
    scoreText = transform.Find("Score").GetComponent<Text>();
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
    EventManager.startListening("Player Death", pause);
  }

  //===================================================================================================================

  private void OnDisable() {
    EventManager.stopListening("Player Kill", scoreIncrease);
    EventManager.stopListening("Player Death", pause);
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
    scoreText.text = "SCORE\n" + score;
  }

  //===================================================================================================================

  private void pause() {
    StartCoroutine(fadeIn(true, 0.5f));
    cooldown.enabled = false;
    scoreText.enabled = false;

    finalScoreText.text = "Final Score: " + score + "\nClick to Restart";
    scoreText.text = "SCORE\n0";
    score = 0;
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
}
