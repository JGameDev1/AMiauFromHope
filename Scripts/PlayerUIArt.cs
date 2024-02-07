using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIArt : MonoBehaviour
{private PlayerArtController playerArtController;
public Text LifeRep,ArmorRep,NextLevelButtontxt,ContinueButtontxt;
public MusicLanguajeManager _MusicLanguajeManager;
GameObject Joystick;
public GameObject[]Buttons;public GameObject PunchButton,SuperPunchButton,KickButton,JumpButton;

private void Awake(){_MusicLanguajeManager=GameManager.FindObjectOfType<MusicLanguajeManager>();playerArtController=FindObjectOfType<PlayerArtController>();Joystick=GameObject.Find("Fixed Joystick");}

void Start(){SuperPunchButton.SetActive(false);
LifeRep=GameObject.Find("LifeText").GetComponent<Text>();ArmorRep=GameObject.Find("ArmorText").GetComponent<Text>();
TextFunction();if(_MusicLanguajeManager.UseJoystick&&!_MusicLanguajeManager.PCGame){Joystick.SetActive(true);foreach(GameObject B in Buttons){B.SetActive(false);}KickButton.SetActive(true);JumpButton.SetActive(true);PunchButton.SetActive(true);}else if(!_MusicLanguajeManager.UseJoystick&&!_MusicLanguajeManager.PCGame){Joystick.SetActive(false);foreach(GameObject B in Buttons){B.SetActive(true);}KickButton.SetActive(true);JumpButton.SetActive(true);PunchButton.SetActive(true);}else if(_MusicLanguajeManager.PCGame){Joystick.SetActive(false);foreach(GameObject B in Buttons){B.SetActive(false);}KickButton.SetActive(false);PunchButton.SetActive(false);JumpButton.SetActive(false);}}
void TextFunction(){LifeRep.text=playerArtController.CurrentHealth.ToString();ArmorRep.text=playerArtController.CurrentArmor.ToString();
if(!_MusicLanguajeManager.Ingles){NextLevelButtontxt.text="Bien Hecho";ContinueButtontxt.text="Continuar";}else if(_MusicLanguajeManager.Ingles){NextLevelButtontxt.text="Well Done";ContinueButtontxt.text="Continue";}}

void ShowAttacksFunction(){if(playerArtController.SuperCharge>=10&&!_MusicLanguajeManager.PCGame){SuperPunchButton.SetActive(true);PunchButton.SetActive(false);}else if(playerArtController.SuperCharge<=10&&!_MusicLanguajeManager.PCGame){SuperPunchButton.SetActive(false);PunchButton.SetActive(true);}
if(_MusicLanguajeManager.PCGame){SuperPunchButton.SetActive(false);PunchButton.SetActive(false);}}

void Update(){TextFunction();ShowAttacksFunction();}
}
