using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameLinks : MonoBehaviour
{
    // Start is called before the first frame update
    public static MemoryGameLinks instance;
    public  MemoryGameManager gameManager;
    public  MemoryGameUIManager uIManager;

    void Awake(){
        instance = this;
        
    }
}
