using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDedect : MonoBehaviour
{
    public Rigidbody2D Hunter;
    private void Start()
    {
        Hunter =  GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Hunter.velocity = new Vector2();
        }
    }
}
