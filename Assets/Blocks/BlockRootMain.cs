using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRootMain : BlockRoot
{
    public override void AfterEnter()
    {
        base.AfterEnter();
    }
    public override void EnsureChildAfter()
    {
        base.EnsureChildAfter();
        
        EnsureChild(0, BlockNewLine.BlockResName) ;
    }
}
