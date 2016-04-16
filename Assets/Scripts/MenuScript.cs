using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

  private float duration = 0.25f;
  private Image fader;

  //===================================================================================================================

  private void Start() {
    fader = transform.Find("Fader").GetComponent<Image>();
    StartCoroutine(fadeOut(true));
  }

  //===================================================================================================================

  public void click(string sceneName) {
    StartCoroutine(fadeOut(false, sceneName));
  }

  //===================================================================================================================

  private IEnumerator fadeOut(bool reverse=false, string sceneName="") {
    float timeElapsed = 0;

    fader.color = !reverse ? Color.clear : Color.black;

    while(timeElapsed < duration) {
      timeElapsed += Time.deltaTime;
      if(!reverse) fader.color = Color.Lerp(Color.clear, Color.black, timeElapsed/duration);
      else fader.color = Color.Lerp(Color.black, Color.clear, timeElapsed/duration);
      yield return null;
    }

    if(!reverse) SceneManager.LoadScene(sceneName);
  }

}
