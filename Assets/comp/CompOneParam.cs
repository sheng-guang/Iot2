using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class CompOneParam : MonoBehaviour, IPointerClickHandler, InputEnter,BlockUp
{
    public Block up;
    private void Awake()
    {
        up = GetComponentInParent<Block>();
    }
    public TextMeshProUGUI TypeText;
    public Vector3 InputFocusePoint => transform.position+Vector3.right* 100*transform.lossyScale.x;
    public void SetType(OneParam p)
    {
        ParamName = p.Name;
        Type = p.Type;
       if(TypeText) TypeText.text = p.Name + "\n(" + p.Type+")";
    }
    public string ParamName;
    public string Type;

    public void enter(string toName)
    {
        var pre = Creater.getPre(toName);
        if (pre == null) return ;
        if (pre.returnType != Type) return;
        var ne = Instantiate(pre, transform);
        ne.AfterEnter();
        ne.SetUp(this);
        //ne.transform.SetSiblingIndex(index);
        ne.transform.localPosition = Vector3.zero;
        ne.transform.localScale = Vector3.one * 0.5f;
        created = ne;
        InputManager.Focus(created);
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

    public void MoveDirection(int index, KeyCode key)
    {
    }

    public Block EntarChild(int index, string resName)
    {
        print("no creat");
        return null;
    }

    public void DeletChild(Block b)
    {
        print("here");
        if (created) Destroy(created.gameObject);
        InputManager.Focus(this);
    }
}
