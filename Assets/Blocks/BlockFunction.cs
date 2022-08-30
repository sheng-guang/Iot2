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
    public CompParams func;
    public override int ParamCount => func.p.Count;
    public string _returnType;
    public override string returnType => _returnType;

}
