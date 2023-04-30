using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textFollow : MonoBehaviour
{
    public GameObject text;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("character");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        text.transform.LookAt(player.transform);
    }
}
