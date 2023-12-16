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
    [Header("DeadSound")]
    [SerializeField] private AudioClip deadSound;
    [SerializeField] private int numberOfFlashed;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField]private Behaviour[] components;
    private bool invulnerble;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerble) return;
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
            //vo hieu hoa tat ca
            foreach(Behaviour component in components)
                    component.enabled = false;
            anim.SetBool("grounded", true);
            anim.SetTrigger("die");
            dead = true;
            SoundManager.instance.PlaySound(deadSound);
            }
        }
    }
    public void addHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    public void Respawn()
    {
        dead = false;
        addHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());
        //kich hoat hanh dong nguoi choi
        foreach (Behaviour component in components)
            component.enabled = true;
    }
   
    private IEnumerator Invunerability()
    {
        invulnerble = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);

        for (int i = 0; i < numberOfFlashed; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(batTu / (numberOfFlashed *2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(batTu / (numberOfFlashed * 2));
        }      
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerble = false;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
