using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    private Color OwnColor;
    // Start is called before the first frame update
    void Start()
    {
        OwnColor = GetComponent<SpriteRenderer>().color;
    }



    public void TakeDamage(Color color)
    {
        Debug.Log("Work");
        if (color == OwnColor)
        {
            Debug.Log("Die");
            Destroy(gameObject);

        }
    }
}
