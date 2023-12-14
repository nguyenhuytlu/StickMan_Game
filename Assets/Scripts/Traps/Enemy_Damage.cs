using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] private AudioClip SpikesTrapSound;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(SpikesTrapSound);
        if (collision.tag == "Player")
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}
