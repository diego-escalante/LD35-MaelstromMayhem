using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

  public AudioClip atkSound;
  public AudioClip damageSound;
  public AudioClip chargeSound;
  public AudioClip deathSound;
  public AudioClip spawnSound;
  public AudioClip shiftSound;

  private AudioSource aSource;

  //===================================================================================================================

  private void Start() {
    aSource = Camera.main.GetComponent<AudioSource>();
  }

  //===================================================================================================================

  public void playShift(bool global=false) {
    playSound(shiftSound, global);
  }

  //===================================================================================================================

  public void playAttack(bool global=false){
    playSound(atkSound, global);
  }

  //===================================================================================================================

  public void playDamage(bool global=false){
    playSound(damageSound, global);
  }

  //===================================================================================================================

  public void playCharge(bool global=false){
    playSound(chargeSound, global);
  }

  //===================================================================================================================

  public void playDeath(bool global=false){
    playSound(deathSound, global);
  }

  //===================================================================================================================

  public void playSpawn(bool global=false){
    playSound(spawnSound, global);
  }

  //===================================================================================================================

  private void playSound(AudioClip clip, bool global=false) {
    if(global) {
      aSource.PlayOneShot(clip);
      return;
    }
    Vector3 pos = transform.position;
    pos.z = Camera.main.transform.position.z;
    AudioSource.PlayClipAtPoint(clip, pos);
  }
}
