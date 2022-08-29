using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
[RequireComponent(typeof(CompName))]
partial class Block//size
{

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
        InputManager.Focus(this);
    }
}

partial class Block//enter
{
    public virtual Transform ChildRoot => null;
    public int Index => transform.GetSiblingIndex();

    public virtual void enter(KeyCode key)
    {
        if (key == KeyCode.Return) enter(BlockNewLine.BlockResName);
        if (key == KeyCode.Backspace) Delet();
        if (key == KeyCode.RightArrow || key == KeyCode.LeftArrow
            || key == KeyCode.UpArrow || key == KeyCode.DownArrow)
           up. MoveDirection(Index,key);
    }
    public virtual void enter(string FullName)
    {
        var ne = up.CreatChild(Index + 1, FullName);
        if (ne) InputManager.Focus(ne);
    }
    public void Delet() => up.DeletChild(this);

}
partial class Block
{
    public virtual Block EnsureChild(int index, string toName) => null;
    public virtual Block CreatChild(int index, string toName) => null;
    public virtual void DeletChild(Block b) { }
    public virtual void MoveDirection(int index,KeyCode key) { }
}
public partial class Block : MonoBehaviour
{
    public virtual Record GetRecord() { return null; }
    public virtual void SetRecord(Record r) { }


    public virtual Vector3 InputFocusePoint => transform.position +Vector3.right*widthWihtTail;
    [SerializeField]
    public string _resName;
    public virtual string ResName => _resName;
    public void SetUp(Block b) => up = b;
    public Block up { get; private set; }
    public virtual void AfterEnter()
    {
        this.LinkWithGo(gameObject);
        gameObject.SetActive(true);
    }

    public virtual void EnsureChildAfter() { }
    public virtual void FreshSize(bool getComp,float minTimes) { }

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
