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
  
}
