using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemProjectile : MonoBehaviour
{
    public Transform firePoint; 
    public GameObject projectTilePrefab;

    public float projectTileForce = 4f;

    void throwProjectTile(Transform target)
    {
        GameObject projectTile = Instantiate(projectTilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectTile.GetComponent<Rigidbody2D>();
        Vector2 aim = (target.position - firePoint.position).normalized; 
        rb.AddForce(aim * projectTileForce, ForceMode2D.Impulse);
    }
}
