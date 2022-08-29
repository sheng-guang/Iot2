using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BlockRoot : Block
{
    public override void EnsureChildAfter()
    {
        base.EnsureChildAfter();

        EnsureChild(0, BlockNewLine.BlockResName);
        for (int i = 0; i < ChildRoot.childCount; i++)
        {
            var to = ChildRoot.GetChild(i).GetBlock();
            if (to) to.EnsureChildAfter();
        }
    }


    public Transform ChildRoot_;
    public override Transform ChildRoot => ChildRoot_;
    public override Block EnsureChild(int index, string toName)
    {
        if (TryGetChildBlock(index, out var re) && re.ResName == toName) return re;
        return CreatChild(index, toName);
    }
    public override Block CreatChild(int index, string toName)
    {
        var pre = Creater.getPre(toName);
        if (pre == null) return null;

        var ne = Instantiate(pre, ChildRoot);
        ne.AfterEnter();
        ne.SetUp(this);
        ne.transform.SetSiblingIndex(index);
        ne.transform.localPosition = Vector3.zero;
        return ne;
    }
    public override void DeletChild(Block b)
    {
        base.DeletChild(b);
        if (b.Index == 0) return;
        var to = b.Index - 1;
        InputManager.Focus(GetChildBlock(to));
        DestroyImmediate(b.gameObject);
    }
    public override void MoveDirection(int index, KeyCode key)
    {
        base.MoveDirection(index, key);
        if (key == KeyCode.RightArrow) InputManager.Focus(GetChildBlock(index + 1));
       else if (key == KeyCode.LeftArrow) InputManager.Focus(GetChildBlock(index - 1));

        else if (key == KeyCode.DownArrow) InputManager.Focus(GetChildBlock(index + 1));
        else if (key == KeyCode.UpArrow) InputManager.Focus(GetChildBlock(index - 1));

    }
}
partial class BlockRoot
{
    public UIFreshSize fresh;
    public override void FreshSize(bool getComp, float minTimes)
    {
        fresh.FreshSize(getComp,minTimes);
    }
   
}
