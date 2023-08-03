using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{

    // Update is called once per frame

    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;
    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
                ChangeState(EnemyState.walk);
                changeAnim(temp - transform.position);
            }
            else
            {
                ChangeGoal();
            }
        }
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius
    && Vector3.Distance(target.position, transform.position) > attackRadius)
        {

            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {

                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
                ChangeState(EnemyState.walk);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                anim.SetBool("wakeup", true);
            }
        }
    }

    public void ChangeGoal()
    {
        if (currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }

}
