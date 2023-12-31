using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}
public class Enemy : MonoBehaviour
{
    [Header("State Machine")]
    public EnemyState currentState;
    [Header("Enemy Stats")]
    public float health;
    public FloatValue maxHealth;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public Vector2 homePosition;
    [Header("Death Effect")]
    public GameObject deathEffect;
    private float deathEffectDelay = 1f;
    // Start is called before the first frame update

    private void Awake()
    {
        health = maxHealth.initialValue;
    }
    private void OnEnable() {
        transform.position = homePosition;
    }
    private void Takedamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathEffect();
            this.gameObject.SetActive(false);
                
        }
    }
    private void DeathEffect(){
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Knock(Rigidbody2D myRigdbody2D, float koncktime, float damage)
    {
        StartCoroutine(KnockCO(myRigdbody2D, koncktime));
        Takedamage(damage);
    }
    private IEnumerator KnockCO(Rigidbody2D myRigdbody2D, float koncktime)
    {
        if (myRigdbody2D != null)
        {
            myRigdbody2D.GetComponent<Enemy>().currentState = EnemyState.stagger;
            yield return new WaitForSeconds(koncktime);
            myRigdbody2D.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigdbody2D.velocity = Vector2.zero;

        }
    }
}
