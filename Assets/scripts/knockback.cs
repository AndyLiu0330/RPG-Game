using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockback : MonoBehaviour
{
    public float thrust;
    public float koncktime;
        

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("enemy")){
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if(enemy != null){
                enemy.isKinematic = false;
                Vector2 differece = enemy.transform.position - transform.position;
                differece = differece.normalized *thrust;
                enemy.AddForce(differece, ForceMode2D.Impulse);
                StartCoroutine(KnockCO(enemy));
            }

        }
        
    }
    private IEnumerator KnockCO(Rigidbody2D enemy){
        if (enemy != null){
        enemy.GetComponent<Enemy>().currentState = EnemyState.stagger;
            yield return new WaitForSeconds(koncktime);
            enemy.velocity = Vector2.zero;
            enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
            enemy.isKinematic = true;
        }
    }
}
