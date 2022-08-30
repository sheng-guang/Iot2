using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockIfCondition : BlockRootWidth
{
    public static string StaticResName => "Condition";
    public override string ResName => StaticResName;
    public override bool isNewLineStart => true;

}
