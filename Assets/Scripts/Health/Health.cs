using UnityEngine;
using System.Collections;
using Player;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;

    [SerializeField] private int numberOfFlashes;
    private Animator anim;
    private bool dead;
    private SpriteRenderer spriteRend;

    [Header("Audio")]
    [SerializeField] private AudioClip deadSound;
    [SerializeField] private AudioClip hurtSound;



    private UIManager uiManager;
    public float currentHealth { get; private set; }

    public void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                // anim.SetTrigger("die");
                GetComponent<Movement>().enabled = false;
                dead = true;

                uiManager.GameOver();
                SoundManager.instance.PlaySound(deadSound);
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            // anim.SetTrigger("hurt");
        }
        Physics2D.IgnoreLayerCollision(10, 9, false);
    }
}
