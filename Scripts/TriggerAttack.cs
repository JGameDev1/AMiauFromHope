using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAttack : MonoBehaviour
{public int AttackDamage;
public AudioClip AttackSound,WoodSound;
public AudioSource _AudioSource;
private void OnTriggerEnter2D(Collider2D collision)
{if(collision.gameObject.tag=="Enemy"){collision.GetComponent<EnemyHealthManager>().CurrentHealth-=AttackDamage;if(!_AudioSource.isPlaying&&this.name=="SliceEfect"){_AudioSource.PlayOneShot(AttackSound);}}
if(collision.gameObject.tag=="Destructible"){collision.gameObject.SetActive(false);if(!_AudioSource.isPlaying&&this.name=="SliceEfect"){_AudioSource.PlayOneShot(WoodSound);}}}
}
