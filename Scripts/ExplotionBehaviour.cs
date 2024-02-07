using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplotionBehaviour : MonoBehaviour
{   AudioClip metalSound;
    float ColliderTimeElimination;
    public float OnTimer;
    bool boom;

    private void Start()
    {ColliderTimeElimination=OnTimer;
    boom=false;}

    private void OnTriggerEnter2D(Collider2D collision)
    {if(collision.gameObject.tag=="Enemy"&&GetComponent<SpriteRenderer>().enabled==false){collision.gameObject.GetComponent<EnemyHealthManager>().CurrentHealth-=70;boom=true;}
    if(collision.gameObject.tag=="Player"&&GetComponent<SpriteRenderer>().enabled==false){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth-=70;collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor=0;collision.gameObject.GetComponent<Rigidbody2D>().AddForce((new Vector2(collision.transform.position.x-transform.position.x,0)+Vector2.up)*500,ForceMode2D.Force);boom=true;}
    if(collision.gameObject.tag=="Destructible"&&GetComponent<SpriteRenderer>().enabled==false){collision.gameObject.SetActive(false);boom=true;}}
    private void OnCollisionEnter2D(Collision2D collision)
    {if(collision.gameObject.tag=="Floor"&&GetComponentInParent<BarrilBehaviour>().aereal){GetComponentInParent<AudioSource>().PlayOneShot(metalSound);GetComponentInParent<BarrilBehaviour>().Explote.Invoke();}}
    private void Update()
    {if(ColliderTimeElimination<=0){GetComponentInParent<CircleCollider2D>().enabled=false;}
    if(boom==true){ColliderTimeElimination-=Time.deltaTime;}}
}
