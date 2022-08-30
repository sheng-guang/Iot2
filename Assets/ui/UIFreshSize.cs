using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class RtExtra
{
    public static float width(this RectTransform t) { return t.rect.width; }
    public static void setWidth(this RectTransform rt, float value)
    { rt.sizeDelta = new Vector2(value, rt.height()); }
    public static float height(this RectTransform t) { return t.rect.height; }
    public static void setHeight(this RectTransform rt, float value)
    { rt.sizeDelta = new Vector2(rt.width(), value); }
}
public class UIFreshSize : MonoBehaviour
{
    RectTransform _rt;
    public RectTransform rt { get { if (_rt == null) _rt = GetComponent<RectTransform>(); return _rt; } }

    public RectTransform ChildRoot;
    public float LineSplit = 5;
    public float MaxWidth =>ChildRoot.rect.width;

    public void FreshSize(bool getComp,bool pre)
    {
        if (rt.width() <(pre?PreMinWidth: minWidth)) rt.setWidth(pre ? PreMinWidth : minWidth);
        int NowBlockIndex = 0;

        float NowX = 0;
        float NowY = 0;
        float NowLineMaxHeight = 0;
        int NowlineIndex = 0;
        void ToNewLine()
        {
            NowBlockIndex = 0;
            NowX = 0;
            NowY += NowLineMaxHeight + LineSplit;
            NowLineMaxHeight = 0;
            NowlineIndex++;
        }
        bool TestIfOverWeight(Block b)
        {
            return b.widthWihtTail + NowX > MaxWidth;
        }
        //print("fresh----------------------------");
        for (int i = 0; i < ChildRoot.childCount; i++)
        {
            Block b = null;
            if (getComp) b = ChildRoot.GetChild(i).GetComponent<Block>();
            else b = ChildRoot.GetChild(i).GetBlock();
            if (!b)
            {
                if (ChildRoot.GetChild(i).GetComponent<UIFreshSizeNewLine>()) ToNewLine();
                continue;
            }
            if (b.gameObject.activeInHierarchy == false) continue;
            b.FreshSize(getComp,pre);
            //print(b.isNewLineStart);
            //print(NowlineIndex);
            //print(NowBlockIndex);
            if (b.isNewLineStart && (NowBlockIndex != 0))
            {
                ToNewLine();
                //print("new line start");
            }
            if (NowBlockIndex != 0 && TestIfOverWeight(b)) ToNewLine();
            //print(new Vector3(NowX, NowY));
            b.transform.localPosition = new Vector3(NowX, -NowY);
            if (b.height > NowLineMaxHeight) NowLineMaxHeight = b.height;
            //print(b.height);
            NowX += b.widthWihtTail;
            NowBlockIndex++;
        }
        rt.setHeight( NowY + NowLineMaxHeight);
        if (rt.height() < (pre ? PreMinHeight : minHeight))
            rt.setHeight(pre ? PreMinHeight : minHeight);  
    }
    public float minWidth = 200;
    public float minHeight = 150;
    public float PreMinWidth = 200;
    public float PreMinHeight = 105;
    
}