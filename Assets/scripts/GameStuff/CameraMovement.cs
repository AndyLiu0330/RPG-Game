using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("position veriables")]
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    [Header("Animator")]
    public Animator camAnim;
    [Header("Position Reset")]
    public VectorValue camMin;
    public VectorValue camMax;
    void Start()
    {
        maxPosition = camMax.initialValue;
        minPosition = camMin.initialValue;
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
