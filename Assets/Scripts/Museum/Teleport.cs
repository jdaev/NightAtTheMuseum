﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Teleport : MonoBehaviour
{
    public string sceneName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider){

        if(collider.gameObject.CompareTag("Player")){
            SceneManager.LoadScene(sceneName);
        }
    }
}
