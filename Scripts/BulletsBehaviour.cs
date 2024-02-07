using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsBehaviour : MonoBehaviour
{[SerializeField]private string WeaponOriginName;
public float BulletSpeed,NegLimitX,PosLimitX,NegLimitY,PosLimitY;private float OFFITEMSCRONOBLOODYIMPACT,OFFITEMSCRONO;
private int BulletDamage;
private Rigidbody2D BulletRb;
private PlayerControllerWMW2D _PlayerControllerWMW2D;
public GameObject BloodyEffectOfBullets;
public bool Impact;

private void Start()
{BulletRb=GetComponent<Rigidbody2D>();
_PlayerControllerWMW2D=GameObject.Find("PlayerActionMan").GetComponent<PlayerControllerWMW2D>();}
private void OnEnable()
{gameObject.GetComponent<SpriteRenderer>().enabled=true;gameObject.GetComponent<CapsuleCollider2D>().enabled=true;Impact=false;
BloodyEffectOfBullets.SetActive(false);
BulletRb=GetComponent<Rigidbody2D>();
OFFITEMSCRONOBLOODYIMPACT=1;
switch(WeaponOriginName)
{case "Pistol":_PlayerControllerWMW2D=GameObject.Find("PlayerActionMan").GetComponent<PlayerControllerWMW2D>();BulletSpeed=20;BulletDamage=2;BulletRb.velocity=new Vector2(_PlayerControllerWMW2D.LastMovement.x,_PlayerControllerWMW2D.LastMovement.y)*BulletSpeed;break;
 case "Uzi":_PlayerControllerWMW2D=GameObject.Find("PlayerActionMan").GetComponent<PlayerControllerWMW2D>();BulletSpeed=30;BulletDamage=1;BulletRb.velocity=new Vector2(_PlayerControllerWMW2D.LastMovement.x,_PlayerControllerWMW2D.LastMovement.y)*BulletSpeed;break;}}

private void OnDisable()
{OFFITEMSCRONO=5;}

private void OnCollisionEnter2D(Collision2D collision)
{if(collision.gameObject.tag=="Floor"||collision.gameObject.tag=="Player"){gameObject.SetActive(false);}
if(collision.gameObject.tag=="Destructible"){collision.gameObject.SetActive(false);gameObject.SetActive(false);}
if(collision.gameObject.tag=="Enemy"){collision.gameObject.GetComponent<EnemyHealthManager>().CurrentHealth-=BulletDamage;Impact=true;BulletRb.velocity=Vector2.zero*0;BloodyEffectOfBullets.SetActive(true);}else if(collision.gameObject.name=="GreenSlime"||collision.gameObject.name=="RedSlime"||collision.gameObject.name=="BlueSlime"){collision.gameObject.GetComponent<EnemyHealthManager>().CurrentHealth-=BulletDamage;Impact=true;BulletRb.velocity=Vector2.zero*0;BloodyEffectOfBullets.SetActive(true);}
if(collision.gameObject.name=="Soga"){collision.gameObject.GetComponentInParent<BarrilBehaviour>().CutJoin.Invoke();}
if(collision.gameObject.name=="BarrilExplosivo"){collision.gameObject.GetComponentInParent<BarrilBehaviour>().Explote.Invoke();}
gameObject.GetComponent<SpriteRenderer>().enabled=false;gameObject.GetComponent<CapsuleCollider2D>().enabled=false;
}
void DesactivateThis(){if(OFFITEMSCRONO<=0){gameObject.SetActive(false);}}
void DesactivateThisBySpaceLimits(){if(transform.position.x<NegLimitX||transform.position.x>PosLimitX){gameObject.SetActive(false);}
if(transform.position.y<NegLimitY||transform.position.y>PosLimitY){gameObject.SetActive(false);}}

void Update()
{OFFITEMSCRONO-=Time.deltaTime;DesactivateThis();DesactivateThisBySpaceLimits();if(Impact){OFFITEMSCRONOBLOODYIMPACT-=Time.deltaTime;}}
}
