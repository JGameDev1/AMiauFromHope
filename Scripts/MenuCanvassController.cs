using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuCanvassController : MonoBehaviour
{public Canvas PrincipalCanvas,InstruccionesPCCanvas,InstruccionesCellCanvas,CreditosCanvas,ControlSelectorCanvas,CanvassToHide,LvlSelectorCanvas;
public Button BotonJugar,BotonInstrucciones,BotonIdioma,BotonLanguaje,BotonCreditos,BotonDonar,QuitBanner;
public Button []LvlButtons;
public Text TextoJugar,TxtInstruccionesButton,TextoIdioma,TextoCreditos,PresentacionDeCredsTxt,LvlSelectorTxt,PizarraA,PizarraAr,PizarraC,PizarraD,PizarraS,LvlsTxt;
public bool Ingles;

void Start()
{PrincipalCanvas.enabled=true;CreditosCanvas.enabled=false;BotonLanguaje.gameObject.SetActive(false);BotonIdioma.gameObject.SetActive(true);
if(FindObjectOfType<MusicLanguajeManager>().PCGame==true){ControlSelectorCanvas.enabled=false;}else{ControlSelectorCanvas.enabled=true;}}

public void buttonsActivation()
{for(int i=0;i<LvlButtons.Length;i++)
if(PlayerPrefs.GetInt("lvlUnlock",1)>i){LvlButtons[i].interactable=true;}else{LvlButtons[i].interactable=false;}}

public void BotonJugarFunction(){if(PlayerPrefs.GetInt("lvlUnlock")==0){SceneManager.LoadScene(PlayerPrefs.GetInt("lvlUnlock")+1);}else{SceneManager.LoadScene(PlayerPrefs.GetInt("lvlUnlock"));}}
public void BotonInstruccionesFunction(){if(!FindObjectOfType<MusicLanguajeManager>().PCGame){PrincipalCanvas.enabled=false;InstruccionesCellCanvas.enabled=true;InstruccionesPCCanvas.enabled=false;CreditosCanvas.enabled=false;LvlSelectorCanvas.enabled=false;}else{PrincipalCanvas.enabled=false;InstruccionesCellCanvas.enabled=false;InstruccionesPCCanvas.enabled=true;CreditosCanvas.enabled=false;LvlSelectorCanvas.enabled=false;}}
public void BotonSelectorNiveles(){PrincipalCanvas.enabled=false;InstruccionesCellCanvas.enabled=false;CreditosCanvas.enabled=false;LvlSelectorCanvas.enabled=true;}
public void EnIngles(){if(Ingles==false){Ingles=true;}if(Ingles==true){TextoJugar.text="Play";LvlsTxt.text="Levels";TxtInstruccionesButton.text="Instructions";TextoCreditos.text="Credits";LvlSelectorTxt.text="Replay levels to relive your adventure or continue from the last stage that you played";PizarraC.text="Life";PizarraA.text="Armor";PizarraAr.text="Touch over your weapon to change it";PresentacionDeCredsTxt.text= "Juani: Designer, director, main artist and programmer juaniignacio94@hotmail.com.ar\r\n\r\nGWriterStudio\r\n: Horror Ambient Album-Cainos: Pixel Art Platformer Village Props-\r\nMúsicalizacion:Emanuel Marín, Emanuel Sachello-Sebastian Urbanowicz: Zombie genérico y Zombie Loli-War: Slimes-DEEP FORMS: Production & Background Music-Jean Moreno: Cartoon FX Free Digital Moons: Parallax Forest Background";PizarraD.text="Shoot button";PizarraS.text="Jump button";BotonLanguaje.gameObject.SetActive(true);BotonIdioma.gameObject.SetActive(false);MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles=true;}}
public void EnEspañol(){if(Ingles==true){Ingles=false;}if(Ingles==false){TextoJugar.text="Jugar";LvlsTxt.text="Niveles";TextoIdioma.text="Idioma";TxtInstruccionesButton.text="Instrucciones";TextoCreditos.text="Creditos";LvlSelectorTxt.text="Vuelve a jugar los niveles para revivir tu aventura o continua desde donde dejaste el juego.";PresentacionDeCredsTxt.text= "Juani: Diseñador, director, artista principal y programador juaniignacio94@hotmail.com.ar\r\n\r\nGWriterStudio\r\n: Horror Ambient Album-Cainos: Pixel Art Platformer Village Props-\r\nMúsicalizacion:Emanuel Marín, Emanuel Sachello-Sebastian Urbanowicz: Zombie genérico y Zombie Loli-War: Slimes-DEEP FORMS: Production & Background Music-Jean Moreno: Cartoon FX Free Digital Moons: Parallax Forest Background";PizarraC.text="Vida";PizarraA.text="Armadura";PizarraAr.text="Toca el arma para cambiarla";PizarraD.text="Boton de disparo";PizarraS.text="Boton de salto";BotonIdioma.gameObject.SetActive(true);BotonLanguaje.gameObject.SetActive(false);MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles=false;}}
public void QuitarApp(){Application.Quit();}
public void BotonCreditosFunction(){PrincipalCanvas.enabled=false;InstruccionesCellCanvas.enabled=false;InstruccionesPCCanvas.enabled=false;CreditosCanvas.enabled=true;LvlSelectorCanvas.enabled=false;}
public void AccesToURL(string Site){Application.OpenURL(Site);}
public void BotonRegresarFunction(){PrincipalCanvas.enabled=true;InstruccionesCellCanvas.enabled=false;InstruccionesPCCanvas.enabled=false;CreditosCanvas.enabled=false;LvlSelectorCanvas.enabled=false;}
public void SeleccionarJoystick(){FindObjectOfType<MusicLanguajeManager>().UseJoystick=true;ControlSelectorCanvas.enabled=false;LvlSelectorCanvas.enabled=false;}
public void SeleccionarBotones(){FindObjectOfType<MusicLanguajeManager>().UseJoystick=false;ControlSelectorCanvas.enabled=false;LvlSelectorCanvas.enabled=false;}
public void HideCanvass(){CanvassToHide.enabled=false;QuitBanner.enabled=false;}
public void GoTo(string Lvl){SceneManager.LoadScene(Lvl);}

private void Update()
{buttonsActivation();}

}
