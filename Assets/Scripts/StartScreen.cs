using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    
    public void Onstart()
    {
        
        SceneManager.LoadScene("GameScene");
    }
}
