using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public float healthPoints;
    public int enemyDamage;
    public string enemyName;
    public float movementSpeed;
    public FloatValue maxHealth;

    private void Awake()
    {
        healthPoints = maxHealth.initialValue;
    }

    public void Knock(Rigidbody2D myRigidbody, float knockbackTime,float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockbackTime));
        TakeDamage(damage);
    }

    private void TakeDamage(float damage)
    {
        healthPoints -= damage;
        if(healthPoints <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody,float knockbackTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
