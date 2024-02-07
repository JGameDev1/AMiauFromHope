using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCanvas : MonoBehaviour
{public Image avisoEsp,avisoIng;
void Update(){if(!MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles){avisoEsp.enabled=true;avisoIng.enabled=false;}else{avisoEsp.enabled=false;avisoIng.enabled=true;}}

}
