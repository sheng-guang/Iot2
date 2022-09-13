using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class BlockRoot : Block, BlockUp
{
    public void FoChildEnsure()
    {
        for (int i = 0; i < ChildTr.childCount; i++)
        {
            var to = ChildTr.GetChild(i).GetBlock();
            if (to) to.EnsureChildAfter();
        }
    }
    public override void EnsureChildAfter()
    {
        base.EnsureChildAfter();

        EnsureChild(0, BlockNewLine.BlockResName);
        FoChildEnsure();
    }


    public Transform ChildRoot_;
    public override Transform ChildTr => ChildRoot_;
    public  Block EnsureChild(int index, string toName)
    {
        if (TryGetChildBlock(index, out var re) && re.ResName == toName) return re;
        return CreatChild(index, toName);
    }
    public virtual  Block EntarChild(int index, string toName)
    {
        return CreatChild(index, toName);
    }
    public  Block CreatChild(int index, string toName)
    {
        
        var pre = Creater.getPre(toName);
        if (pre == null) return null;

        var ne = Instantiate(pre, ChildTr);
        ne.AfterEnter();
        ne.SetUp(this);
        ne.transform.SetSiblingIndex(index);
        ne.transform.localPosition = Vector3.zero;
        return ne;
    }
    public  void DeletChild(Block b)
    {
        if (b.Index == 0) return;
        var to = b.Index - 1;
        InputManager.Focus(GetChildBlock(to));
        DestroyImmediate(b.gameObject);
    }
    public  void MoveDirection(int index, KeyCode key)
    {
        if (key == KeyCode.RightArrow) InputManager.Focus(GetChildBlock(index + 1));
       else if (key == KeyCode.LeftArrow) InputManager.Focus(GetChildBlock(index - 1));

        else if (key == KeyCode.DownArrow) InputManager.Focus(GetChildBlock(index + 1));
        else if (key == KeyCode.UpArrow) InputManager.Focus(GetChildBlock(index - 1));

    }
}
partial class BlockRoot
{
    public UIFreshSize fresh;
    public override void FreshSize(bool getComp, bool pre)
    {
        fresh.FreshSize(getComp,pre);
    }
    public override BlockRecordNode GetRecord()
    {
        var re = base.GetRecord();
        re.ResName = ResName;
        re.value = this;
        re.children = new List<BlockRecordNode>();
        for (int i = 0; i < ChildTr.childCount; i++)
        {
            var c = ChildTr.GetChild(i);
            var ne = c.GetComponent<Block>().GetRecord();
            //print(ne.NodeName);
            re.children.Add(ne);

        }
        return re;
    }
    public override void ApplyRecord(BlockRecordNode record)
    {
        base.ApplyRecord(record);
        for (int i = ChildTr.childCount - 1; i >= 0; i--)
        {
            var to = ChildTr.GetChild(i);
            Destroy(to.gameObject);
        }
        for (int i = 0; i < record.children.Count; i++)
        {
            var toRecord = record.children[i];

            var ne = CreatChild(i, toRecord.ResName);
            ne.ApplyRecord(toRecord);
            //print("has input  " + (inputer!=null));
        }
    }
}
