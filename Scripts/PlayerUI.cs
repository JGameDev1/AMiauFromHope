using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{   private PlayerControllerWMW2D _PlayerControllerWMW2D;
    public Text PistolBulletsTxt,ShotgunBulletsTxt,UziBulletsTxt,LifeRep,ArmorRep,CoresColectedtxt,BuyButtontxt,NextLevelButtontxt,ContinueButtontxt;
    public int PistolCurrentBullets,UziCurrentBullets,ShotgunCurrentBullets,CoresColected;
    public Image AxeIMG,PistolIMG,ShotgunIMG,UziIMG;
    public MusicLanguajeManager _MusicLanguajeManager;
    GameObject Joystick;
    public GameObject[]Buttons;public GameObject FireButton,JumpButton,BuyButton;
    MerchantBehaviour ZombieMerchantB;

private void Awake(){_MusicLanguajeManager=GameObject.FindObjectOfType<MusicLanguajeManager>();_PlayerControllerWMW2D=FindObjectOfType<PlayerControllerWMW2D>();ZombieMerchantB=FindObjectOfType<MerchantBehaviour>();
AxeIMG=GameObject.Find("AxeRep").GetComponent<Image>();PistolIMG=GameObject.Find("PistolRep").GetComponent<Image>();ShotgunIMG=GameObject.Find("ShotgunRep").GetComponent<Image>();UziIMG=GameObject.Find("UziRep").GetComponent<Image>();
Joystick=GameObject.Find("Fixed Joystick");}

void Start(){CoresColected=PlayerPrefs.GetInt("CoresColected",0);BuyButtontxt=GameObject.Find("BuyButtontxt").GetComponent<Text>();PistolBulletsTxt=GameObject.Find("PistolGunAmmoText").GetComponent<Text>();ShotgunBulletsTxt=GameObject.Find("ShotgunAmmoText").GetComponent<Text>();UziBulletsTxt=GameObject.Find("UziAmmoText").GetComponent<Text>();LifeRep=GameObject.Find("LifeText").GetComponent<Text>();ArmorRep=GameObject.Find("ArmorText").GetComponent<Text>();CoresColectedtxt=GameObject.Find("CoresColectedtxt").GetComponent<Text>();
TextFunction();if(_MusicLanguajeManager.UseJoystick&&!_MusicLanguajeManager.PCGame){Joystick.SetActive(true);foreach(GameObject B in Buttons){B.SetActive(false);}JumpButton.SetActive(true);FireButton.SetActive(true);BuyButton.SetActive(false);}else if(!_MusicLanguajeManager.UseJoystick&&!_MusicLanguajeManager.PCGame){Joystick.SetActive(false);foreach(GameObject B in Buttons){B.SetActive(true);}JumpButton.SetActive(true);FireButton.SetActive(true);BuyButton.SetActive(false);}else if(_MusicLanguajeManager.PCGame){Joystick.SetActive(false);foreach(GameObject B in Buttons){B.SetActive(false);}JumpButton.SetActive(false);FireButton.SetActive(false);BuyButton.SetActive(false);}}
void TextFunction(){PistolBulletsTxt.text=PistolCurrentBullets.ToString();ShotgunBulletsTxt.text=ShotgunCurrentBullets.ToString();UziBulletsTxt.text=UziCurrentBullets.ToString();LifeRep.text=_PlayerControllerWMW2D.CurrentHealth.ToString();ArmorRep.text=_PlayerControllerWMW2D.CurrentArmor.ToString();CoresColectedtxt.text=CoresColected.ToString();
if(!_MusicLanguajeManager.Ingles){BuyButtontxt.text="Intercambiar";NextLevelButtontxt.text="Bien Hecho";ContinueButtontxt.text="Continuar";}else if(_MusicLanguajeManager.Ingles){BuyButtontxt.text="Change";NextLevelButtontxt.text="Well Done";ContinueButtontxt.text="Continue";}}
void WeaponsAndBulletsRepresentationAndFunction(){if(PistolCurrentBullets<=0){PistolCurrentBullets=0;}if(ShotgunCurrentBullets<=0){ShotgunCurrentBullets=0;}if(UziCurrentBullets<=0){UziCurrentBullets=0;}
if(_PlayerControllerWMW2D.WeaponID==0){AxeIMG.enabled=true;PistolIMG.enabled=false;PistolBulletsTxt.enabled=false;ShotgunIMG.enabled=false;ShotgunBulletsTxt.enabled=false;UziIMG.enabled=false;UziBulletsTxt.enabled=false;}else if(_PlayerControllerWMW2D.WeaponID==1){AxeIMG.enabled=false;PistolIMG.enabled=true;PistolBulletsTxt.enabled=true;ShotgunIMG.enabled=false;ShotgunBulletsTxt.enabled=false;UziIMG.enabled=false;UziBulletsTxt.enabled=false;}else if(_PlayerControllerWMW2D.WeaponID==2){AxeIMG.enabled=false;PistolIMG.enabled=false;PistolBulletsTxt.enabled=false;ShotgunIMG.enabled=true;ShotgunBulletsTxt.enabled=true;UziIMG.enabled=false;UziBulletsTxt.enabled=false;}else if(_PlayerControllerWMW2D.WeaponID==3){AxeIMG.enabled=false;PistolIMG.enabled=false;PistolBulletsTxt.enabled=false;ShotgunIMG.enabled=false;ShotgunBulletsTxt.enabled=false;UziIMG.enabled=true;UziBulletsTxt.enabled=true;}}

public void BUYFROMBUTTON(){if(ZombieMerchantB.Talking&&CoresColected>=ZombieMerchantB.ZombieCoresNecesaries&&ZombieMerchantB.ComprasPosibles>0&&!GameObject.FindObjectOfType<MusicLanguajeManager>().Ingles){foreach(Image Im in ZombieMerchantB.DialogoDeVenta){Im.enabled=false;}ZombieMerchantB.ComprasPosibles--;ZombieMerchantB.DialogoDeVenta[1].enabled=true;GameObject.FindObjectOfType<PlayerUI>().PistolCurrentBullets+=50;GameObject.FindObjectOfType<PlayerUI>().UziCurrentBullets+=100;GameObject.FindObjectOfType<PlayerUI>().ShotgunCurrentBullets+=10;GameObject.FindObjectOfType<PlayerControllerWMW2D>().CurrentHealth=GameObject.FindObjectOfType<PlayerControllerWMW2D>().HealthValue;GameObject.FindObjectOfType<PlayerControllerWMW2D>().CurrentArmor=GameObject.FindObjectOfType<PlayerControllerWMW2D>().ArmorValue;}
else if(ZombieMerchantB.Talking&&CoresColected>=ZombieMerchantB.ZombieCoresNecesaries&&ZombieMerchantB.ComprasPosibles>0&&GameObject.FindObjectOfType<MusicLanguajeManager>().Ingles){foreach(Image Im in ZombieMerchantB.DialogoDeVenta){Im.enabled=false;}ZombieMerchantB.ComprasPosibles--;ZombieMerchantB.DialogoDeVenta[2].enabled=true;GameObject.FindObjectOfType<PlayerUI>().PistolCurrentBullets+=50;GameObject.FindObjectOfType<PlayerUI>().UziCurrentBullets+=100;GameObject.FindObjectOfType<PlayerUI>().ShotgunCurrentBullets+=10;GameObject.FindObjectOfType<PlayerControllerWMW2D>().CurrentHealth=GameObject.FindObjectOfType<PlayerControllerWMW2D>().HealthValue;GameObject.FindObjectOfType<PlayerControllerWMW2D>().CurrentArmor=GameObject.FindObjectOfType<PlayerControllerWMW2D>().ArmorValue;}
if(ZombieMerchantB.Talking&&CoresColected<ZombieMerchantB.ZombieCoresNecesaries&&ZombieMerchantB.ComprasPosibles>0&&!GameObject.FindObjectOfType<MusicLanguajeManager>().Ingles){foreach(Image Im in ZombieMerchantB.DialogoDeVenta){Im.enabled=false;}ZombieMerchantB.DialogoDeVenta[5].enabled=true;}
else if(ZombieMerchantB.Talking&&CoresColected<ZombieMerchantB.ZombieCoresNecesaries&&ZombieMerchantB.ComprasPosibles>0&&GameObject.FindObjectOfType<MusicLanguajeManager>().Ingles){foreach(Image Im in ZombieMerchantB.DialogoDeVenta){Im.enabled=false;}ZombieMerchantB.DialogoDeVenta[6].enabled=true;}}

void Update(){WeaponsAndBulletsRepresentationAndFunction();TextFunction();}
}