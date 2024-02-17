using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public float ChangeTime;
    public int SceneId;
    void Update()
    {
        ChangeTime -= Time.deltaTime;
        if (ChangeTime < 0)
        {
            SceneManager.LoadScene(SceneId);
        }
        
    }
}
