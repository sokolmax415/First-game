using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private Animator anim;

    private int MAX_HEALTH = 100;

    private bool dead;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Damage(10);
        } 
       
    }
    public void Damage(int _damage)
    {
        if (_damage < 0) {
            throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
        }

        
        this.health -= _damage;

        if (health <= 0) {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerAttack>().enabled = false;
                dead = true;
            }
        } else
        {
            anim.SetTrigger("hurt");
        }

       
    }

   

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative healing");
        }
        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;

        if (wouldBeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        } else
        {
            this.health += amount;
        }
    }

    
}
