using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    private Animator anim;
    private bool dead;
    public float currentHealth { get; private set; }

    [Header("iFrames")]
    [SerializeField] private float batTu;
    [SerializeField] private int numberOfFlashed;
    private SpriteRenderer spriteRend;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if(currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            //iframes
        }
        else
        {
            if(!dead)
            {
            anim.SetTrigger("die");
            GetComponent<PlayerController>().enabled = false;
                dead = true;
            }
        }
    }
    public void addHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
   
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);

        for (int i = 0; i < numberOfFlashed; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(batTu / (numberOfFlashed *2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(batTu / (numberOfFlashed * 2));
        }      
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}
