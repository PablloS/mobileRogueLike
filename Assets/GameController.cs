using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] characters; // ������ �������� ����������
����public int selectedCharacterIndex; // ������ ���������� ��������� � ����
    public int selectedWeaponIndex; 
����public CinemachineVirtualCamera virtualCamera; // ������ CinemachineVirtualCamera

����void Start()
    {
        selectedCharacterIndex = PlayerPrefs.GetInt("WeaponIndex"); 
        if (selectedCharacterIndex < characters.Length)
        {
            GameObject player = Instantiate(characters[selectedCharacterIndex]); // �������� ���������� ��������� � ������� �����
������������virtualCamera.Follow = player.transform; // ������������ Cinemachine � ���������
��������}
        else
        {
            Debug.LogError("Selected character index is out of range!"); // ��������� ������, ���� ��������� �������� �� ������
��������}
    }
}

