using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIsetWidth : MonoBehaviour, IDragHandler
{
    public RectTransform rt;
    public float limitX;
    public float limitY;

    public void OnDrag(PointerEventData eventData)
    {
        var mousePoss = Input.mousePosition;
        if (mousePoss.x > Screen.width) mousePoss.x = Screen.width - 8;
        var to = mousePoss - rt.transform.position;
        if (to.x < limitX) to.x = limitX;
        to.y = -rt.sizeDelta.y;
        if (-to.y < limitY) to.y = -limitY;

        if (rt) rt.sizeDelta = new Vector2(to.x, -to.y);


    }
}
