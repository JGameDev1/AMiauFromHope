using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalUI : MonoBehaviour
{public Canvas Black;
private Text Texto;
public Image Sani,GatitosMensaje,CatsMessaje;
[TextArea]public string TextoLegadoEspañol="Con el lider de la horda eliminado, los amigos volvieron a reunirse. Juntos contemplaban desde aquel escenario un mundo nuevo por explorar, por limpiar y salvar.\r\nSi bien no sabemos que encontraremos adelante, lo único seguro es que siempre tendremos a alguien por quien seguir, siempre tendremos UN MIAU DE ESPERANZA.";
[TextArea]public string TextoLegadoIngles="With the leader of the horde eliminated, the friends reunited. Together they contemplated from that stage a new world to explore, to clean and save. \r\nAlthough we don't know what we will find ahead, the only sure thing is that we will always have someone to follow, we will always have A MEOW FROM HOPE.";
[TextArea]public string TextoAgradecimientoEspañol="Este juego fue inspirado por un gran compañero de vida y amigo. Descansa en paz pequeño.";
[TextArea]public string TextoAgradecimientoIngles="This game was inpirated by a great friend and life parter. Rest in peace little buddy.";
public float WriterSpeed;

private void Start(){Texto=GameObject.Find("TextoDelLegado").GetComponent<Text>();Sani=GameObject.Find("Sani").GetComponent<Image>();
if(!MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles){GatitosMensaje.enabled=true;CatsMessaje.enabled=false;StartCoroutine(Escrituradetexto(TextoLegadoEspañol,TextoAgradecimientoEspañol));}
else if(MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles){GatitosMensaje.enabled=false;CatsMessaje.enabled=true;StartCoroutine(Escrituradetexto(TextoLegadoIngles,TextoAgradecimientoIngles));}}
public void BackMenu(){SceneManager.LoadScene("Menu");}

public IEnumerator Escrituradetexto(string Legado,string Agradecimiento)
{foreach(char Caracter in Legado){Texto.text+=Caracter;yield return new WaitForSeconds(WriterSpeed);}
yield return new WaitForSeconds(2.0f);
Texto.text="";
foreach(char Caracter in Agradecimiento){Texto.text+=Caracter;yield return new WaitForSeconds(WriterSpeed);}
yield return new WaitForSeconds(2.0f);
Sani.enabled=true;
yield return new WaitForSeconds(5.0f);
Black.enabled=false;}

}