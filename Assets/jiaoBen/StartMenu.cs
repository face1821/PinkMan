using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public void StatrGame()
    {
        Debug.Log("游戏开始");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
