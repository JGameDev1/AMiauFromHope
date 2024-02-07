using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewOfPlayer : MonoBehaviour
{PlayerControllerWMW2D _PlayerControllerWMW2D;
private void Start()
{_PlayerControllerWMW2D=GameObject.Find("PlayerActionMan").GetComponent<PlayerControllerWMW2D>();}
private void OnTriggerStay2D(Collider2D Other){if(Other.gameObject.tag=="Enemy"||Other.gameObject.tag=="Boss"||Other.gameObject.tag=="BossProjectile"||Other.gameObject.tag=="EnemyProjectile"){_PlayerControllerWMW2D.InDanger=true;}}
}
