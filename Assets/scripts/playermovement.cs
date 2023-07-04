using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle    
}
public class playermovement : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody2D;
    private Vector3 change;
    private Animator animat;
    void Start()
    {
        currentState = PlayerState.walk;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        animat = GetComponent<Animator>();
        animat.SetFloat("moveX", 0);
        animat.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector2.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());

        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {

            UpdateAnimationAndMove();
        }

    }
    private IEnumerator AttackCo()
    {
        animat.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animat.SetBool("attacking", false);     
        yield return new WaitForSeconds (.3f);
        currentState = PlayerState.walk;

    }
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animat.SetFloat("moveX", change.x);
            animat.SetFloat("moveY", change.y);
            animat.SetBool("moving", true);

        }
        else
        {
            animat.SetBool("moving", false);
        }
    }
    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody2D.MovePosition(
             transform.position + change * speed * Time.deltaTime
        );
    }
    public void Knock(float knockTime)
{
    StartCoroutine(KnockCo(knockTime));
}

private IEnumerator KnockCo( float knockTime)
{
    if (myRigidbody2D != null)
    {
        yield return new WaitForSeconds(knockTime);
        myRigidbody2D.velocity = Vector2.zero;
        currentState = PlayerState.idle;
        myRigidbody2D.velocity = Vector2.zero;
    }
}

    
}
