using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent onTriggerInvoke;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OntriggerEnter(Collider collider){
        onTriggerInvoke.Invoke();
    }

    

}
