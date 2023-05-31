using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public string tagTarget = "Player";

    public List<Collider2D> detectedObj = new List<Collider2D>(); 

    public Collider2D zone; 
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(tagTarget))
        {
            detectedObj.Add(collider);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag(tagTarget))
        {
            detectedObj.Remove(collider);
        }
    }
}
