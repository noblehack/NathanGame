using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Rigidbody legoman;
    bool canPress = true;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("hi");
        Application.targetFrameRate = 60;
    }

    
    void FixedUpdate()
    {   
        if(Input.GetKey("r")){
            legoman.transform.position = new Vector3(-2, 4, -2);
            legoman.velocity = new Vector3(0,0, 0);
        }
        
        if (Input.GetKey("g")&& canPress){
            legoman.useGravity = !legoman.useGravity;
            canPress = false;
            StartCoroutine(wait());
        }
        if(legoman.useGravity == false){
        legoman.drag = 10;

        if(Mathf.Sqrt((legoman.velocity.x * legoman.velocity.x) + (legoman.velocity.z * legoman.velocity.z) + (legoman.velocity.y * legoman.velocity.y)) > 15 ){
            legoman.velocity = Vector3.ClampMagnitude(legoman.velocity, 15);
        }

        if(Input.GetKey("w")){
            legoman.AddForce(transform.forward*(4000 * Time.deltaTime));
        }  if (Input.GetKey("a")){
            legoman.AddForce(transform.right*( -4000 * Time.deltaTime));
        } if (Input.GetKey("s")){
            legoman.AddForce(transform.forward*( -4000 * Time.deltaTime));
        } if (Input.GetKey("d")){
            legoman.AddForce(transform.right*(4000 * Time.deltaTime));
        } if (Input.GetKey("space") ){
            legoman.AddForce(transform.up*(4000 * Time.deltaTime));
        } if (Input.GetKey(KeyCode.LeftShift) ){
            legoman.AddForce(transform.up*(-4000 * Time.deltaTime));
        }
        } else {
            legoman.drag=1;

        if(Mathf.Sqrt((legoman.velocity.x * legoman.velocity.x) + (legoman.velocity.z * legoman.velocity.z)) > 15 ){
            legoman.velocity = Vector3.ClampMagnitude(legoman.velocity, 15);
        }  

            if(Input.GetKey("w")){
            legoman.AddForce(transform.forward*(1000 * Time.deltaTime));
        }  if (Input.GetKey("a")){
            legoman.AddForce(transform.right*( -1000 * Time.deltaTime));
        } if (Input.GetKey("s")){
            legoman.AddForce(transform.forward*( -1000 * Time.deltaTime));
        } if (Input.GetKey("d")){
            legoman.AddForce(transform.right*(1000 * Time.deltaTime));
        } if (Input.GetKey("space")&& canPress ){
            legoman.AddForce(transform.up*(10000 * Time.deltaTime));
            canPress = false;
            StartCoroutine(wait());
        } if (Input.GetKey(KeyCode.LeftShift) ){
            legoman.AddForce(transform.up*(-200 * Time.deltaTime));
        }
        }
    }


    IEnumerator wait(){
        yield return new WaitForSeconds(0.2f);
        canPress = true;
     }
}
