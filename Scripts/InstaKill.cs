using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKill : MonoBehaviour
{public bool forArt;
    
private void OnTriggerEnter2D(Collider2D collision)
{if(collision.gameObject.name=="PlayerActionMan"&&!forArt){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor=0;collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth=0;}
else if(collision.gameObject.name=="PlayerActionMan"&&forArt){collision.gameObject.GetComponent<PlayerArtController>().CurrentArmor=0;collision.gameObject.GetComponent<PlayerArtController>().CurrentHealth=0;}
if(collision.gameObject.name!="PlayerActionMan"){collision.gameObject.SetActive(false);}}
}
