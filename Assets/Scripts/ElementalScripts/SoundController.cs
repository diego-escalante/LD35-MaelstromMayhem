using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

  public AudioClip atkSound;
  public AudioClip damageSound;
  public AudioClip chargeSound;

  //===================================================================================================================

  public void playAttack(){
    playSound(atkSound);
  }

  //===================================================================================================================

  public void playDamage(){
    playSound(damageSound);
  }

  //===================================================================================================================

  public void playCharge(){
    playSound(chargeSound);
  }

  //===================================================================================================================

  private void playSound(AudioClip clip) {
    Vector3 pos = transform.position;
    pos.z = Camera.main.transform.position.z;
    AudioSource.PlayClipAtPoint(clip, pos);
  }
}
