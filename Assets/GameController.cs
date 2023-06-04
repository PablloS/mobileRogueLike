using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] characters; // массив префабов персонажей
    public int selectedCharacterIndex; // индекс выбранного персонажа в меню
    public int selectedWeaponIndex; 
    public CinemachineVirtualCamera virtualCamera; // объект CinemachineVirtualCamera

    void Start()
    {
        selectedCharacterIndex = PlayerPrefs.GetInt("WeaponIndex"); 
        if (selectedCharacterIndex < characters.Length)
        {
            GameObject player = Instantiate(characters[selectedCharacterIndex]); // загрузка выбранного персонажа в игровую сцену
            virtualCamera.Follow = player.transform; // прикрепление Cinemachine к персонажу
        }
        else
        {
            Debug.LogError("Selected character index is out of range!"); // обработка ошибки, если выбранный персонаж не найден
        }
    }
}

