using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Djoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public UnityEngine.UI.Image jostickBG;
    public UnityEngine.UI.Image jostick;
    public Camera cam;
    private Transform Transform;
    private Vector2 inputVector;
    public bool isUsed = false;
    public float muveVector = 0;
    // Start is called before the first frame update
    void Start()
    {
        jostickBG = GetComponent<Image>();
        jostick = GetComponent<Image>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        isUsed = true;
        if (jostick != null) 
        { 
        }
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(jostickBG.rectTransform,eventData.position,eventData.pressEventCamera, out pos ))
        {
            pos.x = (pos.x / jostick.rectTransform.sizeDelta.x);
            pos.y = (pos.y / jostick.rectTransform.sizeDelta.y); 
        }
        inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
        jostick.rectTransform.anchoredPosition = 
            new Vector2(
            inputVector.x*25/** (jostickBG.rectTransform.sizeDelta.x)*/, 
            inputVector.y*25 /** (jostickBG.rectTransform.sizeDelta.y)*/
            );
        muveVector = inputVector.x * 100;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isUsed = false;
        muveVector = 0;
        inputVector = Vector2.zero;
        jostick.rectTransform.anchoredPosition = Vector2.zero;
    }
    public float Horisontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }
    public float Vertical()
    {
        if (inputVector.y != 0)
            return inputVector.y;
        else
            return Input.GetAxis("Vertical");
    }
    // Update is called once per frame
}
