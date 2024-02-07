using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawManagerForART : MonoBehaviour
{public List<GameObject>CreatedEnemies;public List<GameObject>EnemiesInGame;public List<GameObject>ItemsCreated;public List<GameObject>ItemsInGame;public GameObject Player;
public float NormalRespawnTime,HordeRespawnTime,ItemsRespawnTime,ActivationCrono;public int LastCheckPoint;
float CurrentRespawnTime,CurrentItemsRespawnTime;
public bool EnemyHorde,ActivateRespawnFunction,ActivateItemRespawnFunction;
public List<Transform>RespawnPoints;public List<Transform>ItemsRespawnPoints;public List<Transform>CheckPoints;

void GeneralCronoForFunction(){if(GameObject.FindObjectOfType<PlayerArtController>().Idle){ActivationCrono-=Time.deltaTime;}}

void EnemiesInPool()
{GameObject FemaleblondeZombie=Instantiate(CreatedEnemies[0],transform.position,transform.rotation);
FemaleblondeZombie.SetActive(false);EnemiesInGame.Add(FemaleblondeZombie);
GameObject FemalePelirrojaZombie=Instantiate(CreatedEnemies[1],transform.position,transform.rotation);
FemalePelirrojaZombie.SetActive(false);EnemiesInGame.Add(FemalePelirrojaZombie);
GameObject FemaleMorochaZombie=Instantiate(CreatedEnemies[2],transform.position,transform.rotation);
FemaleMorochaZombie.SetActive(false);EnemiesInGame.Add(FemaleMorochaZombie);
GameObject MaleZombies=Instantiate(CreatedEnemies[3],transform.position,transform.rotation);
MaleZombies.SetActive(false);EnemiesInGame.Add(MaleZombies);
GameObject DogZombies=Instantiate(CreatedEnemies[4],transform.position,transform.rotation);
DogZombies.SetActive(false);EnemiesInGame.Add(DogZombies);
GameObject FlyerSkull=Instantiate(CreatedEnemies[5],transform.position,transform.rotation);
FlyerSkull.SetActive(false);EnemiesInGame.Add(FlyerSkull);
//SpecialEnemies----------------------------------------------------------------------------------
GameObject PurpleMaleZombies=Instantiate(CreatedEnemies[6],transform.position,transform.rotation);
PurpleMaleZombies.SetActive(false);EnemiesInGame.Add(PurpleMaleZombies);
GameObject GreenMaleZombies=Instantiate(CreatedEnemies[7],transform.position,transform.rotation);
GreenMaleZombies.SetActive(false);EnemiesInGame.Add(GreenMaleZombies);
GameObject PurpleFemaleZombies1=Instantiate(CreatedEnemies[8],transform.position,transform.rotation);
PurpleFemaleZombies1.SetActive(false);EnemiesInGame.Add(PurpleFemaleZombies1);
GameObject PurpleFemaleZombies2=Instantiate(CreatedEnemies[9],transform.position,transform.rotation);
PurpleFemaleZombies2.SetActive(false);EnemiesInGame.Add(PurpleFemaleZombies2);
GameObject PurpleFemaleZombies3=Instantiate(CreatedEnemies[10],transform.position,transform.rotation);
PurpleFemaleZombies3.SetActive(false);EnemiesInGame.Add(PurpleFemaleZombies3);
GameObject GreenFemaleZombies1=Instantiate(CreatedEnemies[11],transform.position,transform.rotation);
GreenFemaleZombies1.SetActive(false);EnemiesInGame.Add(GreenFemaleZombies1);
GameObject GreenFemaleZombies2=Instantiate(CreatedEnemies[12],transform.position,transform.rotation);
GreenFemaleZombies2.SetActive(false);EnemiesInGame.Add(GreenFemaleZombies2);
GameObject GreenFemaleZombies3=Instantiate(CreatedEnemies[13],transform.position,transform.rotation);
GreenFemaleZombies3.SetActive(false);EnemiesInGame.Add(GreenFemaleZombies3);}

void ItemsInPool()
{GameObject Item1=Instantiate(ItemsCreated[0]);
Item1.SetActive(false);ItemsInGame.Add(Item1);
GameObject Item2=Instantiate(ItemsCreated[1]);
Item2.SetActive(false);ItemsInGame.Add(Item2);
GameObject Item3=Instantiate(ItemsCreated[2]);
Item3.SetActive(false);ItemsInGame.Add(Item3);
GameObject Item4=Instantiate(ItemsCreated[3]);
Item4.SetActive(false);ItemsInGame.Add(Item4);
GameObject Item5=Instantiate(ItemsCreated[4]);
Item5.SetActive(false);ItemsInGame.Add(Item5);
GameObject Item6=Instantiate(ItemsCreated[5]);
Item6.SetActive(false);ItemsInGame.Add(Item6);}

GameObject RandomItemsCall()
{int Index=Random.Range(0,ItemsInGame.Count);
if(!ItemsInGame[Index].activeSelf){ItemsInGame[Index].SetActive(true);ItemsInGame[Index].transform.position=ItemsRespawnPoints[Random.Range(0,ItemsRespawnPoints.Count)].transform.position;ItemsRespawnPoints[Index].transform.rotation=transform.rotation;}
return ItemsInGame[Index];}

void RandomItemsRespawnCronometre(){CurrentItemsRespawnTime-=Time.deltaTime;if(CurrentItemsRespawnTime<=0){ActivateItemRespawnFunction=true;
if(ActivateItemRespawnFunction){RandomItemsCall();CurrentItemsRespawnTime=ItemsRespawnTime;ActivateItemRespawnFunction=false;}}}

GameObject RandomEnemiesCall()
{int Index=Random.Range(0,EnemiesInGame.Count);
if(!EnemiesInGame[Index].activeSelf){EnemiesInGame[Index].SetActive(true);EnemiesInGame[Index].transform.position=RespawnPoints[Random.Range(0,RespawnPoints.Count)].transform.position;EnemiesInGame[Index].transform.rotation=transform.rotation;}
return EnemiesInGame[Index];}

void CheckPointActualization(){if(Player.transform.position.x>CheckPoints[0].transform.position.x&&Player.transform.position.x<CheckPoints[1].transform.position.x){LastCheckPoint=0;}
else if(Player.transform.position.x>CheckPoints[1].transform.position.x&&Player.transform.position.x<CheckPoints[2].transform.position.x){LastCheckPoint=1;}
else if(Player.transform.position.x>CheckPoints[2].transform.position.x&&Player.transform.position.x<CheckPoints[3].transform.position.x){LastCheckPoint=2;}
else if(Player.transform.position.x>CheckPoints[3].transform.position.x&&Player.transform.position.x<CheckPoints[4].transform.position.x){LastCheckPoint=3;}
else if(Player.transform.position.x>CheckPoints[4].transform.position.x&&Player.transform.position.x<CheckPoints[5].transform.position.x){LastCheckPoint=4;}
}

public void RespawnPlayerInLastCheckPoint(){if(!Player.activeSelf){GameManager._SharedInstanceGameManager.RunTheGame();Player.SetActive(true);Player.transform.position=CheckPoints[LastCheckPoint].transform.position;}}

void RandomEnemiesRespawnCronometre(){if(!EnemyHorde){CurrentRespawnTime-=Time.deltaTime;if(CurrentRespawnTime<=0){ActivateRespawnFunction=true;if(ActivateRespawnFunction){RandomEnemiesCall();CurrentRespawnTime=NormalRespawnTime;ActivateRespawnFunction=false;}}}
else{CurrentRespawnTime-=Time.deltaTime;if(CurrentRespawnTime<=0){ActivateRespawnFunction=true;if(ActivateRespawnFunction){RandomEnemiesCall();CurrentRespawnTime=HordeRespawnTime;ActivateRespawnFunction=false;}}}}


private void Start(){Player=FindObjectOfType<PlayerArtController>().gameObject;LastCheckPoint=0;CurrentItemsRespawnTime=ItemsRespawnTime;CurrentRespawnTime=NormalRespawnTime;EnemiesInPool();ItemsInPool();}

private void Update(){CheckPointActualization();
if(Player.GetComponent<PlayerArtController>().Idle==true){ActivationCrono-=Time.deltaTime;}
if(ActivationCrono<=0){RandomEnemiesRespawnCronometre();RandomItemsRespawnCronometre();}
if(ActivationCrono<=-30){EnemyHorde=true;}}
}
