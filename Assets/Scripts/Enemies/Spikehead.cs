using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikehead : Enemy_Damage
{
    [Header ("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private float checkTimer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destiantion;
    private bool attacking;
    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        if (attacking)
             transform.Translate(destiantion *Time.deltaTime*speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }
    private void CheckForPlayer()
    {
        Calculatedirections();
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
            if(hit.collider == null && !attacking)
            {
                attacking = true;
                destiantion = directions[i];
                checkTimer = 0;
            }
        }
    }
    private void Calculatedirections()
    {
        directions[0] = transform.right * range;//right
        directions[1] = -transform.right * range; //left
        directions[2] = transform.up * range; //up
        directions[1] = -transform.up * range;//down


    }
    private void Stop()
    {
        destiantion = transform.position;
        attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
