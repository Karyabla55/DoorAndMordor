using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Color OwnColor;
    private Color TargetsColor;
    private Rigidbody2D Rigidbody;

    private void Start()
    {
        OwnColor = GetComponent<SpriteRenderer>().color;
        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.velocity = new Vector2(15,2);
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TargetsColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
        if(OwnColor == TargetsColor)
        {
            Destroy(gameObject);
        }
    }
}
