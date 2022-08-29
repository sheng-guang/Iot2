using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CompOneParam : MonoBehaviour, IPointerClickHandler, InputEnter
{
    public Vector3 InputFocusePoint => transform.position;

    public void enter(string toName)
    {
        var pre = Creater.getPre(toName);
        if (pre == null) return ;

        var ne = Instantiate(pre, transform);
        ne.AfterEnter();
        //ne.SetUp(this);
        //ne.transform.SetSiblingIndex(index);
        ne.transform.localPosition = Vector3.zero;
        ne.transform.localScale = Vector3.one * 0.5f;
        created = ne;
    }

    public void enter(KeyCode s)
    {
        if(s== KeyCode.Backspace) {if(created) DestroyImmediate(created); }
    }
    
    public Block created = null;
    public void OnPointerClick(PointerEventData eventData)
    {
        InputManager.Focus(this);
    }
}
