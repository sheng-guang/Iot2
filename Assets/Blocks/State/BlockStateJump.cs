using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using UnityEngine.Timeline;
using System.Reflection;
public enum stateJumpKind
{
    none,
    pre,
    next,
    again,
    toTarget,
}
public class BlockStateJump : Block
{
    public override void ApplyRecord(BlockRecordNode record)
    {
        base.ApplyRecord(record);
        if (point != null && record.LocalX.HasValue)
        {
            point.Center.transform.localPosition = new Vector3(record.LocalX.Value, record.LocalY.Value);
        }
    }
    public override BlockRecordNode GetRecord()
    {
        var re = base.GetRecord();
        re.LocalX = point.Center.localPosition.x;
        re.LocalY = point.Center.localPosition.y;
        return re;
    }
    public UIpointLine pointpre => UIpointLine.ins;
    public UIpointLine point;
    public BlockState master;

    [Header("to write")]
    public stateJumpKind jumpKind;
    public Transform startPoint;
    public TMP_InputField tarName;
    public virtual  void Awake()
    {
        master = GetComponentInParent<BlockState>();
        if (master != null) point = Instantiate(pointpre, transform);
    }
    private void Update()
    {
        if (master == null) return;
        if (point == null) return;
        BlockState tar = null;
        //print("toget tar");
        if (jumpKind == stateJumpKind.next) tar = master.getNext();
        else if (jumpKind == stateJumpKind.pre) tar = master.GetPre();
        else if (jumpKind == stateJumpKind.toTarget) tar = master.getTar(tarName.text);
        //print(tar);

        if (tar == null)
        {
            point.setPoint(-Vector3.one * 100, -Vector3.one * 200);
            return;
        }
        //Vector3 center = startPoint.position;
        //center.x = master.block.width + master.block.transform.position.x +100 ;

        point.setPoint(startPoint.position, tar.getCloset(point.Center.position));
    }
    private void OnDestroy()
    {
if(point)        Destroy(point.gameObject);
    }
}
