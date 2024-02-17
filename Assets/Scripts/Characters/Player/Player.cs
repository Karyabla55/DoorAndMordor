using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int Health;

    public void TakeDamege()
    {
        if (Health < 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(1);
        }
        else
        {
            Health -= 1;
        }

    }
}
