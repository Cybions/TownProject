using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarKey : MonoBehaviour
{
    public GameItem gameItem = null;
    


    // Start is called before the first frame update
    void Start()
    {
        SetNewgameItem(gameItem);
    }



    public void SetNewgameItem(GameItem newItem = null)
    {
        gameItem = newItem;
        if(newItem == null)
        {
            GetComponent<Image>().sprite = null;
            GetComponent<Image>().color = Color.clear;
        }
        else
        {
            GetComponent<Image>().sprite = newItem.GI_sprite;
            GetComponent<Image>().color = Color.white;
        }
        
    }

}
