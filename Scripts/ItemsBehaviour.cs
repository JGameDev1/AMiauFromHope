using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsBehaviour : MonoBehaviour
{public string TypeOfItem;
public PlayerUI _PlayerUI;
public PlayerUIArt _PlayerUIArt;
public bool IsAWeapon,ForArt;

private void Start()
{if(!ForArt){_PlayerUI=GameObject.Find("UI").GetComponent<PlayerUI>();}
if(ForArt){_PlayerUIArt=GameObject.Find("UI").GetComponent<PlayerUIArt>();}}
private void OnTriggerEnter2D(Collider2D Other)
{if(Other.gameObject.tag=="Player"&&!ForArt){
switch(TypeOfItem)
{case "Pistol":if(!IsAWeapon){_PlayerUI.PistolCurrentBullets+=Other.GetComponent<PlayerControllerWMW2D>().AmmoForPistol;}else{_PlayerUI.PistolCurrentBullets+=10;}break;
case "Shotgun":if(!IsAWeapon){_PlayerUI.ShotgunCurrentBullets+=Other.GetComponent<PlayerControllerWMW2D>().AmmoForShotgun;}else{_PlayerUI.ShotgunCurrentBullets+=6;};break;
case "Uzi":if(!IsAWeapon){_PlayerUI.UziCurrentBullets+=Other.GetComponent<PlayerControllerWMW2D>().AmmoForUzi;}else{_PlayerUI.UziCurrentBullets+=30;};break;
case "AmmoBox":_PlayerUI.UziCurrentBullets+=100;_PlayerUI.ShotgunCurrentBullets+=12;_PlayerUI.PistolCurrentBullets+=20;break;
case "Botiquin":Other.GetComponent<PlayerControllerWMW2D>().CurrentHealth=Other.GetComponent<PlayerControllerWMW2D>().HealthValue;break;
case "Pildora":Other.GetComponent<PlayerControllerWMW2D>().CurrentHealth+=Other.GetComponent<PlayerControllerWMW2D>().HealthValue/4;break;
case "Frasco":Other.GetComponent<PlayerControllerWMW2D>().CurrentHealth+=Other.GetComponent<PlayerControllerWMW2D>().HealthValue/2;break;
case "Armor":Other.GetComponent<PlayerControllerWMW2D>().CurrentArmor=Other.GetComponent<PlayerControllerWMW2D>().ArmorValue;break;
case "Score":_PlayerUI.CoresColected++;break;}
gameObject.SetActive(false);}
else if(Other.gameObject.tag=="Player"&&ForArt){switch(TypeOfItem)
{case "Botiquin":Other.GetComponent<PlayerArtController>().CurrentHealth=Other.GetComponent<PlayerArtController>().HealthValue;break;
case "Pildora":Other.GetComponent<PlayerArtController>().CurrentHealth+=Other.GetComponent<PlayerArtController>().HealthValue/4;break;
case "Frasco":Other.GetComponent<PlayerArtController>().CurrentHealth+=Other.GetComponent<PlayerArtController>().HealthValue/2;break;
case "Armor":Other.GetComponent<PlayerArtController>().CurrentArmor=Other.GetComponent<PlayerArtController>().ArmorValue;break;}
gameObject.SetActive(false);}}
}
