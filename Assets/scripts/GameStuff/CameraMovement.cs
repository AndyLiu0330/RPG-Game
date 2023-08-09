using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public Animator camAnim;
    void Start()
    {
        camAnim = GetComponent<Animator>();
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
       if(transform.position != target.position){
        Vector3 targetPosition = new Vector3 (target.position.x , target.position.y , transform.position.z);

        targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
       } 
    }
    public void BeginKick(){
        camAnim.SetBool("kickactive", true);
        StartCoroutine(Kickco());
    }
    public IEnumerator Kickco(){
        yield return null;
        camAnim.SetBool("kickactive", false);
    }
}
