using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockback : MonoBehaviour
{
    public float thrust;
    public float koncktime;
    private float knocktime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("breakable"))
        {
            other.GetComponent<pot>().Smash();
        }

        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 differece = hit.transform.position - transform.position;
                differece = differece.normalized * thrust;
                hit.AddForce(differece, ForceMode2D.Impulse);


                if (other.gameObject.CompareTag("enemy"))
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, koncktime);

                }
                if (other.gameObject.CompareTag("Player"))
                {
                    hit.GetComponent<playermovement>().currentState = PlayerState.stagger;
                    other.GetComponent<playermovement>().Knock(knocktime);
                }
            }

        }

    }


}
