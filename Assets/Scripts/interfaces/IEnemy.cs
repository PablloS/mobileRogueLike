using System;
using System.Collections;
using UnityEngine;

 public interface IEnemy
 {
    public float Damage { get; set; }
    public float MoveSpeed { get; set; }

    public void RotateEnemy(Vector2 direction);

    public IEnumerator EnemyAttack();
}