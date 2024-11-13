using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private Health playerHealth;
    private GoblinPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //boxCollider = GetComponent<BoxCollider2D>();
        enemyPatrol = GetComponentInParent<GoblinPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            cooldownTimer = 0;
            anim.SetTrigger("attack_short");
        }
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left,0, playerLayer);
        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer(int damage)
    {
        if (PlayerInSight())
        {
            //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            //playerHealth.SetHealth(playerHealth.GetHeath() - damage);
            playerHealth.Damage(damage);
        }
            //playerHealth.Damage(damage);
    }
}
