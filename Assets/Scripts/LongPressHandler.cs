using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongPressHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Serializable]
    public class ButtonPressEvent : UnityEvent { }
    [SerializeField]
    private float counter;

    private bool longPressed;
    public ButtonPressEvent OnLongPress = new ButtonPressEvent();

    private void Update()
    {
        if (longPressed)
            counter += Time.deltaTime;

        if (counter >= 2)
        {
            OnLongPress.Invoke();
            counter = 0f;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        longPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        longPressed = false;
        counter = 0f;
    }
}
