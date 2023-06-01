using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LevelController : MonoBehaviour
{

    private void Awake()
    {
        GlobalEventManager.OnTimerEnd.AddListener(FinishTheLevel);
    }

    void FinishTheLevel()
    {
        SceneManager.LoadScene("GlobalLocation");
    }
}
