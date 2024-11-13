using UnityEngine;

public class GoblinPatrol : MonoBehaviour
{
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;
    [SerializeField] private Animator anim;
    [SerializeField] private float idleTime;
    private float idleTimer;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving",false);
    }

    private void Update()
    {
        //anim.SetBool("moving",true);
        if (movingLeft)
        {
            if (enemy.position.x >= left.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirrectionChange();
            }
        }
        else 
        {
            if (enemy.position.x <= right.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirrectionChange();
            }
        }
        
    }

    private void DirrectionChange()
    {
        anim.SetBool("moving", false);

        idleTimer += Time.deltaTime;
        if (idleTimer > idleTime)
        {
            movingLeft = !movingLeft;
        }
    }



    private void MoveInDirection(int dir)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * (-1) * dir,initScale.y,initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * dir * speed,
            enemy.position.y, enemy.position.z);
    }
}
