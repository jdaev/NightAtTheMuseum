using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Vector2 scrollDirectionAndSpeed;
    SpriteRenderer sr;
    Vector2 offset;
    private void Awake()
    {

        sr = gameObject.GetComponent<SpriteRenderer>();

    }

    void Start()
    {

    }

    void Update()
    {   
        offset += scrollDirectionAndSpeed * Time.deltaTime;
        sr.material.SetTextureOffset("_MainTex", offset);
    }
}
