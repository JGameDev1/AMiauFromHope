using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitImpact : MonoBehaviour
{public int ImpactDamage;
Rigidbody2D Rb;
public bool Impact;
public GameObject BloodyEffect;
public GameObject BloodyEffectRep;
float BloodyEfectCrono;

void EfectComprobation(){if(BloodyEffect==null){GameObject Respuesto=Instantiate(BloodyEffectRep);BloodyEffect=BloodyEffectRep;}}
void BloodyEfectDesactivation(){if(BloodyEffect.activeSelf){BloodyEfectCrono-=Time.deltaTime;}
if(BloodyEfectCrono<=0){BloodyEffect.SetActive(false);BloodyEfectCrono=1;}}

private void Start()
{Rb=GetComponent<Rigidbody2D>();
 BloodyEfectCrono=1;}

private void OnCollisionEnter2D(Collision2D collision)
{if(collision.gameObject.tag=="Destructible"){collision.gameObject.SetActive(false);gameObject.SetActive(false);}
if(collision.gameObject.tag=="Enemy"){collision.gameObject.GetComponent<EnemyHealthManager>().CurrentHealth-=ImpactDamage;Impact=true;BloodyEffect.SetActive(true);this.GetComponentInParent<PlayerArtController>().SuperCharge++;}}

private void Update()
{EfectComprobation();if(BloodyEffect!=null){BloodyEfectDesactivation();}}
}
