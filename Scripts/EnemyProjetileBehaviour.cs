using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjetileBehaviour : MonoBehaviour
{[SerializeField]private string TypeOfProjectile;
public float ProjectileSpeed,NegLimitX,PosLimitX,NegLimitY,PosLimitY;private float OFFITEMSCRONOBLOODYIMPACT,OFFITEMSCRONO;
private int ProjectileDamage;
private Rigidbody2D ProjectileRb;
public GameObject EffectOfBullets;
public bool forArt;

private void Start()
{forArt=GetComponentInParent<EnemyPatrolMovement>().PlayerArt;
ProjectileRb= GetComponent<Rigidbody2D>();}
private void OnEnable()
{OFFITEMSCRONO=5;
EffectOfBullets.SetActive(false);gameObject.GetComponent<SpriteRenderer>().enabled=true;gameObject.GetComponent<TrailRenderer>().enabled=true;gameObject.GetComponent<CapsuleCollider2D>().enabled=true;ProjectileRb=GetComponent<Rigidbody2D>();OFFITEMSCRONOBLOODYIMPACT=1;
switch(TypeOfProjectile)
{case "FlemaV": ProjectileSpeed=10;ProjectileDamage=10;if(GameObject.Find("PlayerActionMan")!=null){ProjectileRb.AddForce(new Vector2(GameObject.Find("PlayerActionMan").transform.position.x-transform.position.x,0.5f).normalized*ProjectileSpeed,ForceMode2D.Impulse);}else{ProjectileRb.AddForce(new Vector2(Random.Range(0,2)*ProjectileSpeed,2),ForceMode2D.Impulse);}break;}}

private void OnDisable()
{OFFITEMSCRONO=5;}

private void OnCollisionEnter2D(Collision2D collision)
{if(collision.gameObject.name=="Floor"){gameObject.GetComponent<CapsuleCollider2D>().enabled=false;OFFITEMSCRONOBLOODYIMPACT-=Time.deltaTime;EffectOfBullets.SetActive(true);gameObject.GetComponent<SpriteRenderer>().enabled=false;gameObject.GetComponent<TrailRenderer>().enabled=false;ProjectileRb.velocity=Vector2.zero*0;ProjectileDamage=0;}
if(collision.gameObject.tag=="Player"&&!forArt){gameObject.GetComponent<SpriteRenderer>().enabled=false;gameObject.GetComponent<TrailRenderer>().enabled=false;collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth-=ProjectileDamage;OFFITEMSCRONOBLOODYIMPACT-=Time.deltaTime;ProjectileRb.velocity=Vector2.zero*0;EffectOfBullets.SetActive(true);}
if(collision.gameObject.tag=="Player"&&forArt){gameObject.GetComponent<SpriteRenderer>().enabled=false;gameObject.GetComponent<TrailRenderer>().enabled=false;collision.gameObject.GetComponent<PlayerArtController>().CurrentHealth-=ProjectileDamage;OFFITEMSCRONOBLOODYIMPACT-=Time.deltaTime;ProjectileRb.velocity=Vector2.zero*0;EffectOfBullets.SetActive(true);}}
void DesactivateThis(){if(OFFITEMSCRONO<=0){gameObject.SetActive(false);}}
void DesactivateThisBySpaceLimits(){if(transform.position.x<NegLimitX||transform.position.x>PosLimitX){gameObject.SetActive(false);}
if(transform.position.y<NegLimitY||transform.position.y>PosLimitY){gameObject.SetActive(false);}}

void Update()
{OFFITEMSCRONO-=Time.deltaTime;DesactivateThis();DesactivateThisBySpaceLimits();}
}