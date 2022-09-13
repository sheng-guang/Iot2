using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockIf : BlockRootWidth
{
    public override BlockRecordNode GetRecord()
    {
        var re= base.GetRecord();
        re.IfCount = RuleCount;
        re.HasElse = HasElse;
        return re;
    }
    public override void ApplyRecord(BlockRecordNode record)
    {
        if (record.IfCount.HasValue) RuleCount = record.IfCount.Value;
        if (record.HasElse.HasValue) HasElse = record.HasElse.Value;
        base.ApplyRecord(record);

    }
    public override Block EntarChild(int index, string toName)
    {
        return null;
    }


    public int RuleCount=1;
    public bool HasElse = false;
    public void ChangeCount(int count)
    {
        RuleCount += count;
    }
    public void ChangeElse()
    {
        HasElse = !HasElse;
    }
    public override void EnsureChildAfter()
    {
        if (RuleCount < 1) RuleCount = 1;
        for (int i = 0; i < RuleCount; i++)
        {
            EnsureChild(i*2, BlockIfCondition.StaticResName);
            EnsureChild(i*2+1, BlockIfThen.StaticResName);
        }
        if (HasElse) EnsureChild(RuleCount*2, BlockIfElse.StaticResName);

        for (int i = RuleCount*2+(HasElse?1:0); i < ChildTr.childCount; i++)
        {
           var to= ChildTr.GetChild(i);
            DestroyImmediate(to.gameObject);
        }
        FoChildEnsure();
    }



}
