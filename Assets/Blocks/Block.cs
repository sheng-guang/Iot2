using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using System.IO;
using System.Text;
public class CodeFile
{
    public StringBuilder DefineBuilder = new StringBuilder();

    public StringBuilder StackBuilder = new StringBuilder();
    public string getCode()
    {
        var re= StackBuilder.ToString();
        return re;
    }
}
partial class Block
{
    public virtual void GenCode(CodeFile file)
    {

    }
}
partial class Block
{
    public virtual int ParamCount => 0;
    public  string returnType=>ReturnTypeDefault;
    public string ReturnTypeDefault;
    public virtual bool OnlyShowType => false;
    public virtual string GetGoName()
    {
        if (OnlyShowType) { return returnType; }
        return ResName;
    }
    public virtual string GetTextName()
    {
        if (OnlyShowType) { return returnType; }
        var re = ResName;
        if (string.IsNullOrWhiteSpace( returnType)==false) re += " (" + returnType + ")";
        return re;
    }

}
[RequireComponent(typeof(CompName))]
partial class Block//size
{
    public virtual bool BeLineCount => true;
    RectTransform _rt;
    public RectTransform rt { get { if (_rt == null) _rt = GetComponent<RectTransform>(); return _rt; } }

    public virtual bool isNewLineStart => false;
    public virtual float tailWidth => 0;
    public virtual float widthWihtTail => width + tailWidth;
    public virtual float width
    {
        get {return rt.rect.width; }
        set { rt.sizeDelta = new Vector2(value, height); }
    }
    public virtual float height
    {
        get { return rt.rect.height; }
        set { rt.sizeDelta = new Vector2(width, value); }
    }

}
partial class Block : IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        eventData.Use();
        InputManager.Focus(this);
    }
}

partial class Block:InputEnter //enter
{
    public virtual Transform ChildRoot => null;
    public int Index => transform.GetSiblingIndex();

    public virtual void enter(KeyCode key)
    {
        if (key == KeyCode.Return) enter(BlockNewLine.BlockResName);
        if (key == KeyCode.Space) enter(BlockSpace.StaticResName);
        if (key == KeyCode.Tab) enter(BlockTab.StaticResName);
        if (key == KeyCode.Backspace) Delet();
        if (key == KeyCode.RightArrow || key == KeyCode.LeftArrow
            || key == KeyCode.UpArrow || key == KeyCode.DownArrow)
           up. MoveDirection(Index,key);
    }
    public virtual void enter(string FullName)
    {
        var ne = up.EntarChild(Index + 1, FullName);
        if (ne) InputManager.Focus(ne);
    }
    public void Delet()
    {
        up.DeletChild(this);
    }

}

public partial class Block : MonoBehaviour
{
    public virtual Record GetRecord() { return null; }
    public virtual void SetRecord(Record r) { }

    public virtual void FreshWhileInHub() { }

    public virtual Vector3 InputFocusePoint => transform.position +Vector3.right*widthWihtTail*transform.lossyScale.x;
    [SerializeField]
    public string DefResName;
    public virtual string ResName => DefResName;
    public virtual void SetUp(BlockUp b) => up = b;
    public BlockUp up { get; private set; }
    public virtual void AfterEnter()
    {
        this.LinkWithGo(gameObject);
        gameObject.SetActive(true);
    }

    public virtual void EnsureChildAfter() { }
    public virtual void FreshSize(bool getComp,bool minTimes) { }

    //child

    public Block GetChildBlock(int index)
    {
        TryGetChildBlock(index, out var re);
        return re;
    }
    public bool TryGetChildBlock(int index,out Block re)
    {
        re = null;
        if (ChildRoot == null) return false;
        if (index < 0) return false;
        if (ChildRoot.childCount > index == false) return false;
        re= ChildRoot.GetChild(index).GetComponent<Block>();
        return re;
    }


    

}
