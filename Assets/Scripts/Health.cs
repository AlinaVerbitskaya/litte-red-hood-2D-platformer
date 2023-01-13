using Redhood.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField] private int m_currentHealth = 100, maxHealth = 100;
    [SerializeField] private Slider HealthSlider;
    public bool invincible = false;

    public int CurrentHealth { 
        get
        {
            return m_currentHealth;
        }
        set
        {
            m_currentHealth = value;
            HealthSlider.value = (float)m_currentHealth / maxHealth;
            if (m_currentHealth > maxHealth)
            {
                m_currentHealth = maxHealth;
            }
            if (m_currentHealth <= 0)
            {
                m_currentHealth = 0;
                SendMessage("Death", SendMessageOptions.DontRequireReceiver);
            }
        } 
    }

    private void OnEnable()
    {
        CurrentHealth = m_currentHealth; 
    }

    private void Awake()
    {
        HealthSlider.value = (float)CurrentHealth / maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageCheck(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        DamageCheck(collision.gameObject);
    }

    private void DamageCheck(GameObject source)
    {
        DamageDealer dd = source.GetComponent<DamageDealer>();
        bool healing = (dd != null) && (dd.damage < 0) && dd.IsInLayerMask(gameObject, dd.damageLayerMask);
        if (healing)
        {
            ChangeHealth(-dd.damage);
        }
        bool damaging = (dd != null) && (dd.damage > 0) && !IsInvincible() && dd.IsInLayerMask(gameObject, dd.damageLayerMask);
        if (damaging)
        {
            ChangeHealth(-dd.damage);
            if (CurrentHealth > 0)
            {
                SendMessage("HitReact", source.transform, SendMessageOptions.DontRequireReceiver);
            }
        }
        if (source.CompareTag("DeathTrigger"))
        {
            SendMessage("DeathTriggerReact", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void ChangeHealth(int amount)
    {
        if (!invincible)
        {
            CurrentHealth += amount;
        }
    }
    public IEnumerator MakeInvincible(float seconds)
    {
        invincible = true;
        yield return new WaitForSeconds(seconds);
        invincible = false;
    }

    public bool IsInvincible()
    {
        return invincible;
    }
}
