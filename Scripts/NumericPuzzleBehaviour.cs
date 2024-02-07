using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NumericPuzzleBehaviour : MonoBehaviour
{public Canvas puzzleCanvas;
public string nFromPlayer,SecurityCode;
public bool[]Comprobaciones;
public int[]NumbersForCode;
UnityEvent ButtonPressed;
public GameObject Bareer;
public Text CodeRepresentation,ZombieCoresTxt;
public Image ButtonJump,ButtonShoot,ZombieCoresRep;
private void OnTriggerEnter2D(Collider2D collision){if(collision.gameObject.name=="PlayerActionMan"){puzzleCanvas.enabled=true;ButtonJump.enabled=false;ButtonShoot.enabled=false;ZombieCoresRep.enabled=false;ZombieCoresTxt.enabled=false;}}

private void OnTriggerExit2D(Collider2D collision){if(collision.gameObject.name=="PlayerActionMan"){puzzleCanvas.enabled=false;ButtonJump.enabled=true;ButtonShoot.enabled=true;ZombieCoresRep.enabled=true;ZombieCoresTxt.enabled=true;nFromPlayer="";for(int i=0;i<Comprobaciones.Length;i++){Comprobaciones[i]=false;NumbersForCode[i]=0;}}}

void CodigoPuzzle()
{if(Comprobaciones[0]==false&&Comprobaciones[1]==false&&Comprobaciones[2]==false&&Comprobaciones[3]==false){nFromPlayer.Equals(NumbersForCode[0].ToString());}
else if(Comprobaciones[0]==true&&Comprobaciones[1]==false&&Comprobaciones[2]==false&&Comprobaciones[3]==false){nFromPlayer+=NumbersForCode[1].ToString();}
else if(Comprobaciones[0]==true&&Comprobaciones[1]==true&&Comprobaciones[2]==false&&Comprobaciones[3]==false){nFromPlayer+=NumbersForCode[2].ToString();}
else if(Comprobaciones[0]==true&&Comprobaciones[1]==true&&Comprobaciones[2]==true&&Comprobaciones[3]==false){nFromPlayer+=NumbersForCode[3].ToString();}
}

public void ButtonValue(int value){if(Comprobaciones[0]==false&&Comprobaciones[1]==false&&Comprobaciones[2]==false&&Comprobaciones[3]==false){NumbersForCode[0]=value;Comprobaciones[0]=true;ButtonPressed.Invoke();}
else if(Comprobaciones[0]==true&&Comprobaciones[1]==false&&Comprobaciones[2]==false&&Comprobaciones[3]==false){NumbersForCode[1]=value;Comprobaciones[1]=true;ButtonPressed.Invoke();}
else if(Comprobaciones[0]==true&&Comprobaciones[1]==true&&Comprobaciones[2]==false&&Comprobaciones[3]==false){NumbersForCode[2]=value;Comprobaciones[2]=true;ButtonPressed.Invoke();}
else if(Comprobaciones[0]==true&&Comprobaciones[1]==true&&Comprobaciones[2]==true&&Comprobaciones[3]==false){NumbersForCode[3]=value;Comprobaciones[3]=true;ButtonPressed.Invoke();}
}

public void ButtonEnter(){if(nFromPlayer.Equals(SecurityCode)){Bareer.SetActive(false);}else
{nFromPlayer="";for(int i=0;i<Comprobaciones.Length;i++){Comprobaciones[i]=false;NumbersForCode[i]=0;}}}
public void ButtonClear(){nFromPlayer="";for(int i=0;i<Comprobaciones.Length;i++){Comprobaciones[i]=false;NumbersForCode[i]=0;}}

private void Awake(){ButtonShoot=GameObject.Find("ButtonShoot").GetComponent<Image>();ButtonJump=GameObject.Find("ButtonJump").GetComponent<Image>();ZombieCoresRep=GameObject.Find("ZombieCoreRep").GetComponent<Image>();ZombieCoresTxt=GameObject.Find("ZombieCoreRep").GetComponentInChildren<Text>();}

private void Start(){puzzleCanvas.enabled=false;ButtonPressed=new UnityEvent();ButtonPressed.AddListener(CodigoPuzzle);}

private void Update(){nFromPlayer="Code:"+NumbersForCode[0].ToString()+NumbersForCode[1].ToString()+NumbersForCode[2].ToString()+NumbersForCode[3].ToString();CodeRepresentation.text=nFromPlayer+"_";}

}
