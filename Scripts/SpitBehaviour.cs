using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpitType{Green,Yellow,Red,YClone}
public class SpitBehaviour : MonoBehaviour
{public SpitType Spit;
public Animator _Animator;
public bool GreenColor,YellowColor,RedColor,YellowClone;
Rigidbody2D SpitRb;
CrawlerBehaviour _CrawlerBehaviour;
public float OnGreenOffCrono,OnYellowOffCrono,OnRedOffCrono,OnCloneOffCrono,LimitsOfMovementY,NegLimitsOfMovementY,RedSpeed,YellowSpeed,GreenSpeed,LastPositionRegistredX;
float GreenOffCrono,YellowOffCrono,CloneOffCrono,RedOffCrono;
public GameObject BloodExplotionG,BloodExplotionY,BloodExplotionR;
public List<GameObject>Effects;
public int DamageImpact;

private void OnEnable(){_Animator=GetComponent<Animator>();_CrawlerBehaviour=GetComponentInParent<CrawlerBehaviour>();SpitRb=GetComponent<Rigidbody2D>();
GreenOffCrono=OnGreenOffCrono;YellowOffCrono=OnYellowOffCrono;RedOffCrono=OnRedOffCrono;CloneOffCrono=OnCloneOffCrono;
switch(Spit){case SpitType.Green:{YellowColor=false;RedColor=false;GreenColor=true;transform.position=_CrawlerBehaviour.gameObject.transform.position+_CrawlerBehaviour.LastPositionRegistred+Vector3.up;}break;
case SpitType.Yellow:{YellowColor=true;RedColor=false;GreenColor=false;transform.position=_CrawlerBehaviour.gameObject.transform.position+_CrawlerBehaviour.LastPositionRegistred+Vector3.up*2;}break;
case SpitType.Red:{YellowColor=false;RedColor=true;GreenColor=false;transform.position=_CrawlerBehaviour.gameObject.transform.position+_CrawlerBehaviour.LastPositionRegistred+Vector3.up*2;}break;
case SpitType.YClone:{YellowClone=true;YellowColor=false;RedColor=false;GreenColor=false;}break;}}

private void OnDisable(){switch(Spit){case SpitType.Yellow:{transform.position=transform.parent.position;SpitRb.velocity=Vector3.zero;}break;
case SpitType.Red:{transform.position=transform.parent.position;transform.localScale=new Vector3(1,1,1);SpitRb.velocity=Vector3.zero;}break;
case SpitType.Green:{transform.position=transform.parent.position;SpitRb.velocity=Vector3.zero;}break;
}}

void EffectsInvocation(){if(Spit==SpitType.Green){GameObject G=Instantiate(BloodExplotionG);Effects.Add(G);BloodExplotionG.SetActive(false);}
if(Spit==SpitType.Yellow){GameObject Y=Instantiate(BloodExplotionY);Effects.Add(Y);BloodExplotionY.SetActive(false);}
if(Spit==SpitType.Red){GameObject R=Instantiate(BloodExplotionR);Effects.Add(R);BloodExplotionR.SetActive(false);}
if(Spit==SpitType.YClone){GameObject Y=Instantiate(BloodExplotionY);Effects.Add(Y);BloodExplotionY.SetActive(false);}
}

void EffectsComprobation(){
if(Spit==SpitType.Green&&Effects[0]==null){Effects[0]=Instantiate(BloodExplotionG);Effects[0].SetActive(false);}
if(Spit==SpitType.Yellow&&Effects[0]==null){Effects[0]=Instantiate(BloodExplotionY);Effects[0].SetActive(false);}
if(Spit==SpitType.Red&&Effects[0]==null){Effects[0]=Instantiate(BloodExplotionR);Effects[0].SetActive(false);}
if(Spit==SpitType.YClone&&Effects[0]==null){Effects[0]=Instantiate(BloodExplotionY);Effects[0].SetActive(false);}}

void Behaviours(){switch(Spit)
{case SpitType.Green:{SpitRb.velocity=_CrawlerBehaviour.LastPositionRegistred*GreenSpeed*Time.fixedDeltaTime;GreenOffCrono-=Time.deltaTime;if(GreenOffCrono<=0){gameObject.SetActive(false);}break;}
case SpitType.Yellow:{SpitRb.velocity=Vector3.up*YellowSpeed*Time.fixedDeltaTime;YellowOffCrono-=Time.deltaTime;if(YellowOffCrono<=0){gameObject.SetActive(false);}break;}
case SpitType.Red:{RedOffCrono-=Time.deltaTime;if(RedOffCrono>OnRedOffCrono/2){SpitRb.velocity=new Vector3(_CrawlerBehaviour.Player.transform.position.x-_CrawlerBehaviour.Player.transform.position.x,1,0)*RedSpeed*Time.fixedDeltaTime;}
else if(RedOffCrono<OnRedOffCrono/2){transform.localScale=new Vector3(15,15,0);SpitRb.velocity=new Vector3(_CrawlerBehaviour.Player.transform.position.x-transform.position.x,-1,0)*RedSpeed*Time.fixedDeltaTime;}break;}
case SpitType.YClone:{SpitRb.velocity=Vector3.down*YellowSpeed;CloneOffCrono-=Time.deltaTime;if(CloneOffCrono<=0){gameObject.SetActive(false);}break;}}}

private void OnCollisionEnter2D(Collision2D collision){
if(collision.gameObject.tag=="Floor"){Effects[0].transform.position=this.transform.position;Effects[0].SetActive(true);gameObject.SetActive(false);}
if(collision.gameObject.tag=="Player"&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor>0){Effects[0].transform.position=this.transform.position;Effects[0].SetActive(true);collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor-=DamageImpact;gameObject.SetActive(false);}
else if(collision.gameObject.tag=="Player"&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor<=0){Effects[0].transform.position=this.transform.position;Effects[0].SetActive(true);collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth-=DamageImpact;gameObject.SetActive(false);}
}
void DontCrossTheLimits()
{if(transform.position.y>=LimitsOfMovementY){transform.position=new Vector3(transform.position.x,LimitsOfMovementY,transform.position.z);}
if(transform.position.y<=NegLimitsOfMovementY){transform.position=new Vector3(transform.position.x,NegLimitsOfMovementY,transform.position.z);}}
void AnimationParametre(){if(Spit!=SpitType.YClone){LastPositionRegistredX=_CrawlerBehaviour.LastPositionRegistred.x;_Animator.SetBool("Green",GreenColor);_Animator.SetBool("Yellow",YellowColor);_Animator.SetBool("YellowClone",YellowClone);_Animator.SetBool("Red",RedColor);_Animator.SetFloat("RedTimer",RedOffCrono);_Animator.SetFloat("LastPositionRegistred",LastPositionRegistredX);}
else{_Animator.SetBool("Green",GreenColor);_Animator.SetBool("Yellow",YellowColor);_Animator.SetBool("YellowClone",YellowClone);_Animator.SetBool("Red",RedColor);_Animator.SetFloat("RedTimer",RedOffCrono);_Animator.SetFloat("LastPositionRegistred",0);}}

 private void Awake(){EffectsInvocation();}
private void Update(){AnimationParametre();DontCrossTheLimits();EffectsComprobation();}
private void FixedUpdate(){Behaviours();}
}