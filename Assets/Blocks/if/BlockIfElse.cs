using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockIfElse : BlockRootWidth
{
    public static string StaticResName => "Else";
    public override string ResName => StaticResName;
    public override bool isNewLineStart => true;
}
