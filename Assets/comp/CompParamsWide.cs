using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompParamsWide : CompParams
{
    public override Vector3 getScale(int index)
    {
        if (index == 0) return Vector3.one*2;
        return Vector3.one;
    }
    public override Vector3 getLoCalPositon(int index)
    {
        if (index == 0) return Vector3.zero;
        var line = (index + 1) / 2;
        var lineIndex = (index + 1) % 2;
        return new Vector3(lineIndex * 100, -line * 50)+new Vector3(200,0);
    }
}
