using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BlockRoot : Block
{
    public Transform ChildRoot_;
    public override Transform ChildRoot => ChildRoot_;
    public override Block EnsureChild(int index, string toName)
    {
        if (TryGetChildBlock(index, out var re) && re.ResName == toName) return re;
        return CreatChild(index, toName);
    }
    public override Block CreatChild(int index, string toName)
    {
        var pre = Creater.getPre(toName);
        if (pre == null) return null;

        var ne = Instantiate(pre, ChildRoot);
        ne.up = this;
        ne.transform.SetSiblingIndex(index);
        ne.transform.localPosition = Vector3.zero;
        return ne;
    }
    public override void DeletChild(Block b)
    {
        base.DeletChild(b);
        if (b.Index == 0) return;
        var to = b.Index - 1;
        InputManager.Focus(GetChildBlock(to));
        DestroyImmediate(b.gameObject);
    }
    public override void MoveDirection(int index, KeyCode key)
    {
        base.MoveDirection(index, key);
        if (key == KeyCode.RightArrow) InputManager.Focus(GetChildBlock(index + 1));
       else if (key == KeyCode.LeftArrow) InputManager.Focus(GetChildBlock(index - 1));
    }
}
partial class BlockRoot
{

    public float LineSplit = 5;
    public float MaxWidth = Screen.width;

    public override void FreshSize()
    {
        base.FreshSize();
        int NowBlockIndex=0;

        float NowX = 0;
        float NowY = 0;
        float NowLineMaxHeight = 0;
        int NowlineIndex = 0;
        void ToNewLine()
        {
            NowBlockIndex = 0;
            NowX = 0;
            NowY += NowLineMaxHeight+LineSplit;
            NowLineMaxHeight = 0;
            NowlineIndex ++;
        }
        bool TestIfOverWeight(Block b)
        {
            return b.width + NowX > MaxWidth;
        }
        for (int i = 0; i < ChildRoot.childCount; i++)
        {
            var b = ChildRoot.GetChild(i).GetComponent<Block>();
            if (!b) continue;
            //print(b.isNewLineStart);
            //print(NowlineIndex);
            //print(NowBlockIndex);
            if (b.isNewLineStart &&  (NowBlockIndex != 0))
            {
                ToNewLine();
                //print("new line start");
            }
            if (NowBlockIndex != 0 && TestIfOverWeight(b)) ToNewLine();
            //print(new Vector3(NowX, NowY));
            b.transform.localPosition = new Vector3(NowX, - NowY);
            if (b.height > NowLineMaxHeight) NowLineMaxHeight = b.height;
            //print(b.height);
            NowX += b.width;
            NowBlockIndex++;

        }

    }
   
}
