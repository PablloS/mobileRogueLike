using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachinePlayerFinder : MonoBehaviour
{
    CinemachineVirtualCamera cinemashine;
    GameObject player;

    private void Start()
    {
        cinemashine = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player");
        cinemashine.Follow = player.transform;
    }
}
