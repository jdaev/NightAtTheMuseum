using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLinks : MonoBehaviour
{
    public static GameLinks instance;
    public  UIManager uIManager;

    void Awake(){
        instance = this;
        
    }
}
