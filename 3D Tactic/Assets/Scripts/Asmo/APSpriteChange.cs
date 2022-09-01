using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APSpriteChange : MonoBehaviour
{

    private float amountUsed = .33f;
    private float waitTime = 1f;

    void Start()
    {
        
        
    }


    public void APUsed()
    {
        Image image = GetComponent<Image>();

        image.fillAmount -= amountUsed;

        //this.gameObject.GetComponent<Image>().fillAmount -= full / amountUsed;
    }

}
