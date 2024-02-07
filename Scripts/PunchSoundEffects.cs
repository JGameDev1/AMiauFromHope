using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSoundEffects : MonoBehaviour
{AudioSource audioSource;
public AudioClip[]PunchSounds;

private void OnEnable()
{audioSource=GetComponent<AudioSource>();audioSource.PlayOneShot(PunchSounds[Random.Range(0,PunchSounds.Length)]);}
}
