using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

class Sword : PlayerAttackBehavior
{
    Animator animator;
    PlayerController pc;
    public AttackingItems sword; 

    private void Start()
    {
        animator = GetComponent<Animator>();
        pc = GetComponent<PlayerController>(); 
    }

    public override void Attack()
    {
        StartCoroutine(SwordAttack()); 
    }


    IEnumerator SwordAttack()
    {
        animator.SetTrigger("swordAttack");
        pc.canMove = false; 
        StartCoroutine(sword.Attack());
        yield return new WaitForSeconds(sword.attackPreparationTime + sword.attackTime);
        pc.canMove = true;
    }
}

