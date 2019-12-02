using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPatrol : LogMob
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentDestination;
    public float roundingDistance;

    //overrides base class in LogMob class
    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= aggroRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                myRigidbody.MovePosition(temp);
                animator.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > aggroRadius)
        {
            if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                    path[currentPoint].position, movementSpeed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                myRigidbody.MovePosition(temp);
            }
            else
            {
                ChangeDestination();
            }
        }
    }

    private void ChangeDestination()
    {
        if(currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentDestination = path[0];
        }
        else
        {
            currentPoint++;
            currentDestination = path[currentPoint];
        }
    }
}
