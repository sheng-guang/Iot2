using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface BlockUp
{
    public void MoveDirection(int index, KeyCode key);
    public Block EntarChild(int index, string resName);
    public void DeletChild(Block b);
}
public class BlockFunction : Block
{
    public CompParams compIO;
    public override int ParamCount => compIO.type.Count;
    //public string ReturnTypeDefault;
    //public override string returnType => ReturnTypeDefault;

}
