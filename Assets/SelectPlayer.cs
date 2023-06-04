using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectPlayer : MonoBehaviour
{
    private GameObject[] characters;
    private int weaponIndex = 0;
    private int index = 0;

    private void Start()
    {
        characters = new GameObject[transform.childCount]; 

        for(int i = 0; i<transform.childCount; i++)
        {
            characters[i] = transform.GetChild(i).gameObject; 
        }

        foreach (GameObject go in characters)
        {
            go.SetActive(false); 
        }

        if (characters[index])
        {
            characters[index].SetActive(true);
        }
    }

    public void SelectLeft()
    {
        print("test"); 
        characters[index].SetActive(false);
        index--; 
        if (index < 0)
        {
            index = characters.Length - 1; 
        }
        print(index);
        characters[index].SetActive(true); 
    }
    public void SelectRight()
    {
        characters[index].SetActive(false);
        index++;
        if (index == characters.Length)
        {
            index = 0;
        }

        characters[index].SetActive(true);
    }

    public void SelectRightWeapon()
    {
        weaponIndex = 1; 
    }

    public void SelectLeftWeapon()
    {
        weaponIndex = 0;
    }

    public void StartPlay()
    {
        PlayerPrefs.SetInt("HeroIndex", index);
        PlayerPrefs.SetInt("WeaponIndex", weaponIndex); 
        PlayerPrefs.Save();
        SceneManager.LoadScene("GlobalLocation");
    }
}
