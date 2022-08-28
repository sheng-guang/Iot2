using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
partial class Block//size
{
    public virtual void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
    RectTransform rt;
    public virtual bool isNewLineStart => false;
    public virtual float tailWidth => 10f;
    public virtual float width => rt.sizeDelta.x+tailWidth;
    public virtual float height => rt.sizeDelta.y;

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


    public virtual Vector3 InputFocusePoint => transform.position +Vector3.right*width;
    [SerializeField]
    private string resName;
    public virtual string ResName => resName;
    public Block up;
    public virtual void AfterEnter() { }

    public virtual void EnsureChildAfter() { }
    public virtual void FreshSize() { }

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
