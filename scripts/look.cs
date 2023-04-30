using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class look : MonoBehaviour
{
    public Rigidbody legoman;
    //public GameObject dirLight;
    float xRot = 0f;
    public bool canPress = true;
    public LayerMask mask;
    public gameLogic logicScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * 1000 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 1000 * Time.deltaTime;
        
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);

        legoman.transform.Rotate(Vector3.up * mouseX);
        
        if(Input.GetKey("o") && canPress){
            Camera.main.orthographic = true;
            canPress = false;
            StartCoroutine(wait());
        }
        
        if(Input.GetKey("p") && canPress){
            Camera.main.orthographic = false;
            canPress = false;
            StartCoroutine(wait());
        }

        if(Input.GetMouseButtonDown(1)){
            if(Physics.Raycast(transform.position, transform.forward, out var hit,5000f, mask)){
                logicScript.rightClick(hit.collider.gameObject);
            }   
        }
        if(Input.GetMouseButtonDown(0)){
            if(Physics.Raycast(transform.position, transform.forward, out var hit,5000f, mask)){
                logicScript.leftClick(hit.collider.gameObject);
            }   
        }
    }



    
    IEnumerator wait(){
        yield return new WaitForSeconds(0.2f);
        canPress = true;
     }
}
