using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody2D myRigidbody2D; 
    private Vector3 change;
    private Animator animat;
    void Start()
    {
     myRigidbody2D = GetComponent<Rigidbody2D>();   
     animat = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector2.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
     
    }
    void UpdateAnimationAndMove(){
   if (change != Vector3.zero){
            MoveCharacter();
            animat.SetFloat("moveX", change.x);
            animat.SetFloat("moveY", change.y);
            animat.SetBool("moving", true);

        }
        else {
            animat.SetBool("moving", false);
        }
    }
    void MoveCharacter(){
        myRigidbody2D.MovePosition(
             transform.position + change * speed * Time.deltaTime
        );
    }
}
