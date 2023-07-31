using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeasureChest : Interactable


{
    public Item contents;
    public bool isOpen;
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;
    public Inventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (!isOpen)
            {
                //open the chest
                OpenChest();
            }
            else
            {
                //chest is already open
                ChestAlreadyOpen();
            }
        }


    }
    public void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemdescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        raiseItem.Raise();
        isOpen = true;
        context.Raise();
        anim.SetBool("opened", true);

    }
    public void ChestAlreadyOpen()
    {
            dialogBox.SetActive(false);
            //playerInventory.currentItem = null;
            raiseItem.Raise();

    }
    private void OnTriggerEnter2D (Collider2D other){

        if (other.CompareTag("Player")&& !other.isTrigger && !isOpen){
            context.Raise();
            playerInRange = true;

        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen){
            context.Raise();
            playerInRange = false ;

    }
}
}
