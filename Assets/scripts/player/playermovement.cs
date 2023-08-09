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
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue startingPosition;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    public Signal playerHit;
    void Start()
    {
        currentState = PlayerState.walk;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        animat = GetComponent<Animator>();
        animat.SetFloat("moveX", 0);
        animat.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == PlayerState.interact)
        {
            return;
        }
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
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }

    }
    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
            currentState = PlayerState.interact;
            animat.SetBool("receive item", true);
            receivedItemSprite.sprite = playerInventory.currentItem.itemsprite; 
            }
            else {
                animat.SetBool("receive item", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }   
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
    public void Knock(float koncktime, float damage)
    {
        currentHealth.runtimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.runtimeValue > 0)
        {
            StartCoroutine(KnockCO(koncktime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    private IEnumerator KnockCO(float koncktime)
    {
        playerHit.Raise();
        if (myRigidbody2D != null)
        {
            yield return new WaitForSeconds(koncktime);
            myRigidbody2D.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody2D.velocity = Vector2.zero;
        }
    }
}
