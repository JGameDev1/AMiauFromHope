using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{public GameObject Boss;
public BoxCollider2D Box;

private void Start(){Box=GetComponent<BoxCollider2D>();}
private void OnTriggerEnter2D(Collider2D collision){if(collision.gameObject.tag=="Player"){Boss.SetActive(true);Box.enabled=false;}}
private void OnTriggerExit2D(Collider2D collision){if(collision.gameObject.tag=="Player"){Box.enabled=false;}}
}
