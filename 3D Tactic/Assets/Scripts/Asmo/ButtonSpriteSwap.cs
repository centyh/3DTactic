using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteSwap : MonoBehaviour
{
    [SerializeField] private SpriteState spriteState;
    [SerializeField] private Button button;

    //public Sprite normalSprite;
    //public Sprite onHoverSprite;
    
    
    public void SpriteChange()
    {
        button.spriteState = spriteState;
    }
    
}

