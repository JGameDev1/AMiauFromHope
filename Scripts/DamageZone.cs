using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{public string TypeOf;
public int Damage;
private void OnTriggerStay2D(Collider2D collision)
	{
		switch (TypeOf)
		{
			case "Environmental": if(collision.gameObject.tag=="Player"){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth-=Damage;}break;
            case "FromEnemy": if(collision.gameObject.tag=="Enemy"){collision.gameObject.GetComponent<EnemyHealthManager>().CurrentHealth-=Damage;}break;
            default:collision.gameObject.GetComponent<EnemyHealthManager>().CurrentHealth-=Damage;break;				
		}
}
}
