using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRootWidth : BlockRoot
{
    public float DefWidth = 600;

    public override void AfterEnter()
    {
        base.AfterEnter();
        width = DefWidth;
    }
    public override BlockRecordNode GetRecord()
    {
        var re = base.GetRecord();
        re.width = width;
        return re;
    }
    public override void ApplyRecord(BlockRecordNode record)
    {
        base.ApplyRecord(record);
        if (record.width != null)
        {
            width = record.width.Value;
        }
    }
}
