using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonFunctionality : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onLeft;
    public UnityEvent onRight;

    public static bool attackButtonActive = false;
    public static bool moveButtonActive = false;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            onLeft.Invoke();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            onRight.Invoke();
        }
    }


    void Start()
    {
        
    }

    
    void Update()
    {

    }

    public void AttackButtonActive()
    {
        attackButtonActive = true;
        moveButtonActive = false;
        Debug.Log("ATTACK BUTTON ACTIVE");
    }

    public void MoveButtonActive()
    {
        moveButtonActive = true;
        attackButtonActive = false;
        Debug.Log("MOVE BUTTON ACTIVE");
    }

    public void MoveButtonDeactive()
    {
        moveButtonActive = false;
        Debug.Log("MOVE BUTTON DEACTIVE");
    }

    public void AttackButtonDeactive()
    {
        attackButtonActive = false;
    }
}
