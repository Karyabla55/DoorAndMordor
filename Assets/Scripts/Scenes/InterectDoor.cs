using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InterectDoor : MonoBehaviour
{
    public int Door›ndex;
    public int Scene›ndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("›˛lev denene bilir");

           

            if (Input.GetKey(KeyCode.E))
            {
                switch (Door›ndex)
                {
                    case 0:
                        SceneManager.LoadScene(Scene›ndex);
                        break;
                    case 1:
                        SceneManager.LoadScene(Scene›ndex);
                        break;
                    case 2:
                        SceneManager.LoadScene(Scene›ndex);
                        break;
                }
            }
        }
    }



}
