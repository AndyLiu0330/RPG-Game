using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}
public class door : Interactable
{
    [Header("Door Variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D doorCollider;

    public void Update()
    {
        if (Input.GetButtonDown("attack"))
        {
            if (playerInRange && thisDoorType == DoorType.key)
            {
                //Does the player have a key?
                if (playerInventory.numberOfKeys > 0)
                {
                    //Remove a player key
                    playerInventory.numberOfKeys--;
                    //If so, then call the open method
                    Open();
                }
            }
        }
    }
    public void Open (){
        doorSprite.enabled = false;
        open = true;
        doorCollider.enabled = false;

    }
}