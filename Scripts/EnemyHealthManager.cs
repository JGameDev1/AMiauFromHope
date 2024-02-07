using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyHealthManager : MonoBehaviour
{public int HealthValue,CurrentHealth;int ScoreValue;
public bool NormalEnemy;
public float OnDeathCrono;float DeathCrono;
public string TypeOfEnemy;
private CapsuleCollider2D MyCapsulleCollider;
public GameObject EnemyInstantiation;public List<GameObject>InvokedEnemies;
public PlayerUI UIfromPlayer;

void InstantiateInvokationEnemy()
{GameObject InvokedEnemy=Instantiate(EnemyInstantiation);InvokedEnemy.SetActive(false);InvokedEnemies.Add(InvokedEnemy);}

GameObject RequesEnemyAfterDeath(){if(!InvokedEnemies[0].activeSelf){InvokedEnemies[0].SetActive(true);InvokedEnemies[0].transform.position=transform.position+new Vector3(0,2,0);}return InvokedEnemies[0];}

private void Start()
{if(!NormalEnemy){InstantiateInvokationEnemy();}
CurrentHealth=HealthValue;
DeathCrono=OnDeathCrono;
MyCapsulleCollider=GetComponent<CapsuleCollider2D>();
UIfromPlayer=GameObject.FindObjectOfType<PlayerUI>();}

private void OnCollisionEnter2D(Collision2D Other){if(Other.gameObject.tag=="PowerUps"){CurrentHealth+=1;Other.gameObject.SetActive(false);}}

private void OnEnable()
{MyCapsulleCollider=GetComponent<CapsuleCollider2D>();
CurrentHealth=HealthValue;DeathCrono=OnDeathCrono;MyCapsulleCollider.enabled=true;
switch (TypeOfEnemy)
{case "ZombieMale":HealthValue=3;OnDeathCrono=5;ScoreValue=2;break;
case "ZombieFemale":HealthValue=3;OnDeathCrono=5;ScoreValue=2;break;
case "Flyer":HealthValue=2;OnDeathCrono=1;ScoreValue=1;break;
case "Animal":HealthValue=2;OnDeathCrono=5;ScoreValue=3;break;
case "Slime":HealthValue=20;OnDeathCrono=1;ScoreValue=3;break;
case "SuperSlime":HealthValue=40;OnDeathCrono=1;ScoreValue=4;break;
case "FlesherM":HealthValue=80;OnDeathCrono=4;ScoreValue=10;break;
case "Mother":HealthValue=60;OnDeathCrono=4;ScoreValue=10;break;
case "Boss":ScoreValue=50;break;
default:break;}
}

private void Update()
{if(CurrentHealth>=HealthValue){CurrentHealth=HealthValue;}
if(CurrentHealth<=0&&!NormalEnemy){RequesEnemyAfterDeath();MyCapsulleCollider.enabled=false;DeathCrono-=Time.deltaTime;if(DeathCrono<=0){gameObject.SetActive(false);if(UIfromPlayer!=null){UIfromPlayer.CoresColected+=ScoreValue;}}}
if(CurrentHealth<=0){MyCapsulleCollider.enabled=false;DeathCrono-=Time.deltaTime;if(DeathCrono<=0){gameObject.SetActive(false);if(UIfromPlayer!=null){UIfromPlayer.CoresColected+=ScoreValue;}}}}
}