using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGeneration : MonoBehaviour
{

    public GameObject[] objects; 
    void Start()
    {
        int random = Random.Range(0, objects.Length);
        GameObject tmpObj = objects[random];

        float randomTransform = Random.Range(1f, 2f);
        tmpObj.transform.localScale = new Vector3(randomTransform, randomTransform, randomTransform); 

        Instantiate(objects[random], transform.position, Quaternion.identity);
    }

}
