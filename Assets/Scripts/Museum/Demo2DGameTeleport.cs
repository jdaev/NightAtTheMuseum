﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Demo2DGameTeleport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider){

        if(collider.gameObject.CompareTag("Player")){
            SceneManager.LoadScene("Demo2DGameScene");
        }
    }
}
