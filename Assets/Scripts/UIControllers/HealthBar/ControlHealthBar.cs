using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHealthBar : MonoBehaviour
{
    public DamageableCharacter trackObject;
    public float sizeScale = 1f;

    public float yOffset = 0.2f;

    public float barHeight = 1; 

    private void Start()
    {
        transform.Find("HealthBar").transform.localScale = new Vector3(sizeScale, sizeScale*barHeight, 1); 
    }

    void FixedUpdate()
    {
        if (trackObject.health <= 0)
        {
            Destroy(gameObject); 
        }
        Vector3 trackPosition = trackObject.transform.position;
        gameObject.transform.position = new Vector3(trackPosition.x, trackPosition.y+yOffset, trackPosition.z);
    }
}
