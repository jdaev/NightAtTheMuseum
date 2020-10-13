using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Card card;

    bool isSelected = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTap(){
        isSelected = true;
        Image image = this.GetComponent<Image>();
        MemoryGameLinks.instance.gameManager.OnTapCard(this);
    }


}
