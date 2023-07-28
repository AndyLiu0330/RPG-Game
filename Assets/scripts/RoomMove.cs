using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 CameraChanger;
    
    public Vector3 playerChange;
    private CameraMovement cam;
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player") && !other.isTrigger){
            cam.minPosition += CameraChanger;
            cam.maxPosition += CameraChanger;
            other.transform.position += playerChange;

        }
        if (needText){
            StartCoroutine(placeNameCo());

        }
    }
    private IEnumerator placeNameCo (){
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
