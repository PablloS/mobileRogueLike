using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class TimerController : MonoBehaviour
{
    int levelTime = 90;
    int timerTime = 90; 
    public TextMeshProUGUI textMesh;

    private void Start()
    {
        StartCoroutine(GlobalTimer());
        StartCoroutine(HudTimer());
        textMesh.SetText(ToTimeFormat(timerTime));
    }

    private string ToTimeFormat(int time)
    {
        StringBuilder sb = new StringBuilder("", 5);
        int min = time / 60;
        int sec = time % 60; 
        sb.Append(min);
        sb.Append(':');
        if (sec < 10)
        {
            sb.Append('0');
            sb.Append(sec); 
        }
        else
        {
            sb.Append(sec); 
        }

        return sb.ToString(); 
    }

    IEnumerator GlobalTimer()
    {
        yield return new WaitForSeconds(levelTime);
        GlobalEventManager.SendTimerEnd();
    }

    IEnumerator HudTimer()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(1);
            timerTime--; 
            
            textMesh.SetText(ToTimeFormat(timerTime)); 
        }
    }
}
