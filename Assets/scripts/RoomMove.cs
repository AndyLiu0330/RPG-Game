using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 CameraChanger;
    
    public Vector3 playerChange;
    private CameraMovement cam;
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            cam.minPosition += CameraChanger;
            cam.maxPosition += CameraChanger;
            other.transform.position += playerChange;

        }
    }
}
