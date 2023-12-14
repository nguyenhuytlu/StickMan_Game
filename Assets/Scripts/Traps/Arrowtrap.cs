using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrowtrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    [Header("SFX")]
    [SerializeField] private AudioClip arrowsSound;
    private float cooldownTimer;

    private void Attack()
    {
        SoundManager.instance.PlaySound(arrowsSound);
        cooldownTimer = 0;
        arrows[FindFireBall()].transform.localScale = firePoint.position;
        arrows[FindFireBall()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindFireBall()
    {
        for (int i = 0;  i < arrows.Length;i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if(cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
