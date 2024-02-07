using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
private void OnCollisionEnter2D(Collision2D collision){if(collision.gameObject.tag=="Player"&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor>0)
{collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor-=100;}
else if(collision.gameObject.tag=="Player"&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor<=0)
{collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth-=300;}}
}
