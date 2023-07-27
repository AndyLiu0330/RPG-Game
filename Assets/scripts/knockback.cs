using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockback : MonoBehaviour
{
    public float thrust;
    public float koncktime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("breakable"))
        {
            other.GetComponent<pot>().Smash();
        }
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player")) ;
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 differece = hit.transform.position - transform.position;
                differece = differece.normalized * thrust;
                hit.AddForce(differece, ForceMode2D.Impulse);
                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, koncktime, damage);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    hit.GetComponent<playermovement>().currentState = PlayerState.stagger;
                    other.GetComponent<playermovement>().Knock(koncktime, damage);
                    }
                }

                // StartCoroutine(KnockCO(hit));


            }

        }
        // private IEnumerator KnockCO(Rigidbody2D enemy)
        // {
        //     if (enemy != null)
        //     {
        //         yield return new WaitForSeconds(koncktime);
        //         enemy.velocity = Vector2.zero;
        //         enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
        //     }
        // }

    }