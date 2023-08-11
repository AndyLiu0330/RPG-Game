using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool active;
    public BoolValue storedValue;
    public Sprite activeSprite;
    private SpriteRenderer mySpriteRenderer;
    public door thisDoor;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        active = storedValue.runtimeValue;
        if (active)
        {
            ActivateSwitch();
        }
        
    }
    public void ActivateSwitch()
    {
        active = true;
        storedValue.runtimeValue = active;
        thisDoor.Open();
        mySpriteRenderer.sprite = activeSprite;
    }

public void  OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player") && !other.isTrigger)
    {
        ActivateSwitch();
        
        }
    
}
}
