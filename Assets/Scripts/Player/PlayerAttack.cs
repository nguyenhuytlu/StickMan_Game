using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;
    public AudioSource aus;
    [SerializeField] private AudioClip fireballSound;


    private Animator anim;
    private PlayerController playerController;
    private float cooldownTimer = Mathf.Infinity;
    private  void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown)
            Attack();

        cooldownTimer += Time.deltaTime;
    }
    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);

        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireBalls[FindFireball()].transform.position = firePoint.position;
        fireBalls[FindFireball()].GetComponent<Fire>().SetDirection(Mathf.Sign(transform.localScale.x));

    }
    private int FindFireball()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
