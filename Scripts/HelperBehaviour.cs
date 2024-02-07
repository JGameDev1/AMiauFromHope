using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperBehaviour : MonoBehaviour
{public GameObject Barril,Boss;
public List<GameObject>Bombs;
public float NegLimitX,LimitX,Speed;
public Vector3 MovementVector=Vector3.zero;
public Transform RespawnerBarril;
Rigidbody2D Rb;
Animator _Animator;
public bool B0,B1,B2,B3,B4,B5;
public float OnCounter; private float Counter;

void ForAnimations(){_Animator.SetFloat("VectorX",MovementVector.x);}

private void Start(){Boss=GameObject.Find("FlesherBoss");Rb=GetComponent<Rigidbody2D>();_Animator=GetComponent<Animator>();BarrelsInstantiation();Counter=OnCounter;}
void BarrelsInstantiation(){for (int i=0;i<6;i++){GameObject B=Instantiate(Barril);B.SetActive(false);Bombs.Add(B);}}
void BombsActivations(){
if(B0==false&&B1==false&&B2==false&&B3==false&&B4==false&&B5==false){Bombs[0].SetActive(true);Bombs[0].transform.position=RespawnerBarril.transform.position;}else
if(B0==true&&B1==false&&B2==false&&B3==false&&B4==false&&B5==false){Bombs[1].SetActive(true);Bombs[1].transform.position=RespawnerBarril.transform.position;}else
if(B0==true&&B1==true&&B2==false&&B3==false&&B4==false&&B5==false){Bombs[2].SetActive(true);Bombs[2].transform.position=RespawnerBarril.transform.position;}else
if(B0==true&&B1==true&&B2==true&&B3==false&&B4==false&&B5==false){Bombs[3].SetActive(true);Bombs[3].transform.position=RespawnerBarril.transform.position;}else
if(B0==true&&B1==true&&B2==true&&B3==true&&B4==false&&B5==false){Bombs[4].SetActive(true);Bombs[4].transform.position=RespawnerBarril.transform.position;}else
if(B0==true&&B1==true&&B2==true&&B3==true&&B4==true&&B5==false){Bombs[5].SetActive(true);Bombs[5].transform.position=RespawnerBarril.transform.position;}else{}
}

void Crono(){Counter-=Time.deltaTime;if(Counter<=0&&B0==false&&B1==false&&B2==false&&B3==false&&B4==false&&B5==false){Counter=OnCounter;B0=true;}else
if(Counter<=0&&B0==true&&B1==false&&B2==false&&B3==false&&B4==false&&B5==false){Counter=OnCounter;B1=true;}else
if(Counter<=0&&B0==true&&B1==true&&B2==false&&B3==false&&B4==false&&B5==false){Counter=OnCounter;B2=true;}else
if(Counter<=0&&B0==true&&B1==true&&B2==true&&B3==false&&B4==false&&B5==false){Counter=OnCounter;B3=true;}else
if(Counter<=0&&B0==true&&B1==true&&B2==true&&B3==true&&B4==false&&B5==false){Counter=OnCounter;B4=true;}else
if(Counter<=0&&B0==true&&B1==true&&B2==true&&B3==true&&B4==true&&B5==false){Counter=OnCounter;B5=true;}else{}}


void Update()
{ForAnimations();Crono();
if(transform.position.x<=NegLimitX){MovementVector=Vector3.right;BombsActivations();}
else if(transform.position.x>=LimitX){MovementVector=Vector3.left;}
Rb.MovePosition(transform.position+MovementVector*Speed);
if(!Boss.activeSelf){gameObject.SetActive(false);}}
}
