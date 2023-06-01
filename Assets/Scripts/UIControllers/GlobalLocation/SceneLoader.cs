using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private Button sceneButton;
    public string sceneName; 
    private Animator animator;

    private void Start()
    {
        sceneButton = gameObject.GetComponent<Button>();
        animator = gameObject.GetComponent<Animator>(); 
        sceneButton.onClick.AddListener(LoadScene); 
    }
    public void LoadScene()
    {
        StartCoroutine(PlayAnimation()); 
    }

    IEnumerator PlayAnimation()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetTrigger("Play");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(sceneName);
    }
}
