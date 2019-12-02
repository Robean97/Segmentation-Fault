using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMob : Enemy
{
    public Rigidbody2D myRigidbody;
    public Transform target;
    public Transform basePosition;
    public float aggroRadius;
    public float attackRadius;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        //transform holds location informations
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        animator.SetBool("wakeUp",true);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
    {
        if(Vector3.Distance(target.position,transform.position) <= aggroRadius  
            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk 
                && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                myRigidbody.MovePosition(temp);  
                ChangeState(EnemyState.walk);
                animator.SetBool("wakeUp", true);
            }
        }
        else if(Vector3.Distance(target.position, transform.position) > aggroRadius)
        {
            animator.SetBool("wakeUp", false);
        }
    }

    public void ChangeAnimation(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                SetAnimatorFloat(Vector2.right);
            }else if(direction.x < 0)
            {
                SetAnimatorFloat(Vector2.left);
            }
        }else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if(direction.y > 0)
            {
                SetAnimatorFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimatorFloat(Vector2.down);
            }
        }
    }

    private void SetAnimatorFloat(Vector2 setVector){
        animator.SetFloat("moveX",setVector.x);
        animator.SetFloat("moveY", setVector.y);
    }

    private void ChangeState(EnemyState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }
}
