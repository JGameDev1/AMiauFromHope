using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlesherBoss : MonoBehaviour
{Rigidbody2D Rb;
AudioSource _AudioSource;
EnemyHealthManager _EnemyHealthManager;
Animator _Animator;
public List<GameObject>ObjectsToActive;public List<GameObject>ObjectsToDisable;public GameObject WinZone,Player;
public float OnAttackCrono,OnDamageCrono,DistanciaDelJugadorX;
float AttackCrono,DamageCrono;
public bool IsIdle,IsAttacking,Painfull;
public GameObject Crawler;
public PrefabRespawnManager RespawnManager;

private void OnEnable(){RespawnManager=GameObject.FindObjectOfType<PrefabRespawnManager>();Crawler.SetActive(true);Player=GameObject.Find("PlayerActionMan");WinZone=GameObject.Find("WinZone");Rb=GetComponent<Rigidbody2D>();_AudioSource=GetComponent<AudioSource>();
_EnemyHealthManager=GetComponent<EnemyHealthManager>();_Animator=GetComponent<Animator>();WinZone.SetActive(false);RespawnManager.EnemyHorde=true;}

private void OnDisable(){WinZone.SetActive(true);}

private void OnCollisionEnter2D(Collision2D collision)
{if(collision.gameObject.tag=="Player"&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor>=0){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor-=150;}
else if(collision.gameObject.tag=="Player"&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor<=0){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth-=150;}
if(collision.gameObject.tag=="PProjectile"){Painfull=true;IsIdle=false;IsAttacking=false;}}

private void OnTriggerEnter2D(Collider2D collision){if(collision.gameObject.tag=="Player"){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth=0;}
if(collision.gameObject.tag=="PProjectile"){Painfull=true;IsIdle=false;IsAttacking=false;}}
void Attack(){DistanciaDelJugadorX=transform.position.x-Player.transform.position.x;if(DistanciaDelJugadorX<0){DistanciaDelJugadorX=-DistanciaDelJugadorX;}
if(DistanciaDelJugadorX<18){IsIdle=false;IsAttacking=true;}
else{IsIdle=true;IsAttacking=false;}
if(IsAttacking==true){AttackCrono-=Time.deltaTime;}
if(AttackCrono<0){IsAttacking=false;AttackCrono=OnAttackCrono;IsIdle=true;}}

void InfoForAnimations(){_Animator.SetBool("IsIdle",IsIdle);_Animator.SetBool("IsAttacking",IsAttacking);_Animator.SetBool("Painfull",Painfull);
_Animator.SetInteger("LIFE",_EnemyHealthManager.CurrentHealth);}

private void Update(){
InfoForAnimations();Attack();
if(Painfull==true){DamageCrono-=Time.deltaTime;}
if(DamageCrono<0){Painfull=false;DamageCrono=OnDamageCrono;IsIdle=true;}}
}
