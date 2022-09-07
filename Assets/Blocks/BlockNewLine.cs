using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockNewLine : Block
{
    public static string BlockResName => nameof(BlockNewLine);
    public override string ResName => BlockResName;
    public override bool isNewLineStart => true;
    public override bool BeLineCount => false;
}
