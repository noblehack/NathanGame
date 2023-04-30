using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiLogic : MonoBehaviour
{
public GameObject menu;
public GameObject crossHair;
public gameLogic logicScript;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        crossHair.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){
            menu.SetActive(true);
            crossHair.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            
        }
    }

    public void quitPressed(){
        Application.Quit();
    }
    public void backPressed(){
        menu.SetActive(false);
        crossHair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void select5Pressed(){
        menu.SetActive(false);
        crossHair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        logicScript.setupGame(5);
         
    }

    public void select7Pressed(){
        
        menu.SetActive(false);
        crossHair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
         logicScript.setupGame(7);
    }

    public void select9Pressed(){
        
        menu.SetActive(false);
        crossHair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
         logicScript.setupGame(9);
    }

    public void selectCustomPressed(){
        
        menu.SetActive(false);
        crossHair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
         logicScript.setupGame(12);//for now
    }




    // IEnumerator wait(){
    //     yield return new WaitForSeconds(0.2f);
    //     canPress = true;
    //  }
}
