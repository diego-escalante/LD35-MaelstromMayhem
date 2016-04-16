using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

  public AudioClip atkSound;
  public AudioClip damageSound;

  //===================================================================================================================

  public void playAttack(){
    playSound(atkSound);
  }

  //===================================================================================================================

  public void playDamage(){
    playSound(damageSound);
  }

  //===================================================================================================================

  private void playSound(AudioClip clip) {
    Vector3 pos = transform.position;
    pos.z = Camera.main.transform.position.z;
    AudioSource.PlayClipAtPoint(clip, pos);
  }
}
