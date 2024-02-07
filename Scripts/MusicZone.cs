using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicZone : MonoBehaviour
{AudioSource audioSource;
private void Start(){audioSource=GetComponent<AudioSource>();}
private void OnTriggerEnter2D(Collider2D collision){if(collision.gameObject.tag=="Player"){audioSource.Play();}}
private void OnTriggerExit2D(Collider2D collision){if(collision.gameObject.tag=="Player"){audioSource.Stop();}}
}
