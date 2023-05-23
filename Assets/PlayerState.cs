using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

class PlayerState : MonoBehaviour
{
    public DamageableCharacter playerHealth;

    private void Update()
    {
        if (playerHealth.health <= 0)
        {
            StartCoroutine(GameOver()); 
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(5); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); 
    }
}
