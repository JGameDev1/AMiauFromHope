using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{public int Damage;
private void OnCollisionEnter2D(Collision2D collision)
{if(collision.gameObject.tag=="Player"){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth-=Damage;}
if(collision.gameObject.tag=="Enemy"){collision.gameObject.GetComponent<EnemyHealthManager>().CurrentHealth-=Damage;}}
}
