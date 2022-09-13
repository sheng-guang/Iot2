using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface BlockUp
{
    public void MoveDirection(int index, KeyCode key);
    public Block EntarChild(int index, string resName);
    public void DeletChild(Block b);
}
public partial class BlockFunction
{
    public override BlockRecordNode GetRecord()
    {
        var re = base.GetRecord();
        re.children = new List<BlockRecordNode>();
        for (int i = 0; i < compIO.Created.Count; i++)
        {
            var to = compIO.Created[i];
            re.children.Add(to.GetRecord());

        }
        return re;
    }
    public override void ApplyRecord(BlockRecordNode record)
    {
        base.ApplyRecord(record);
        var childCount = record.children.Count;
        var ioCount = compIO.Created.Count;
        if (childCount != ioCount) Debug.LogError("functionError");
        var realCount = Mathf.Min(childCount, ioCount);
        for (int i = 0; i < realCount; i++)
        {
            compIO.Created[i].ApplyRecord(record.children[i]);
        }

    }
}
public partial class BlockFunction : Block
{
    public CompParams compIO;
    public override int ParamCount => compIO.type.Count;

    public Image icon;
    public bool HasIcon => icon.IsActive();
    public virtual void SetIcon(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) { icon.gameObject.SetActive(false); return; }
        icon.gameObject.SetActive(true);
        icon.sprite = Resources.Load<Sprite>("icon/" + name);
    }
    public bool FirstFullSize = false;
    public void SetParams(List<OneParam> parms)
    {
        compIO.type = parms;

        compIO.Awake();
        compIO.LoadParams();
    }
    public void SetNameFresh(string s,string returnT)
    {
        var neName = GetComponent<CompName>();
        neName.SetNameFresh(s,returnT);
    }
    public override void FreshSize(bool getComp, bool minTimes)
    {
        base.FreshSize(getComp, minTimes);
        for (int i = 0; i < compIO.Created.Count; i++)
        {
            var to = compIO.Created[i];
            to.freshSize(getComp, minTimes);
        }
        float ToX = 0;
        float ToY = 0;
        bool IsFirst = true;
        float maxWidth = 0;
        void ToNexLine()
        {
            ToX += maxWidth;
            maxWidth = 0;
            ToY = 0;
            IsFirst = true;
        }
        if (HasIcon) ToX += 100;
        for (int Toindex=0; Toindex < compIO.Created.Count; Toindex++)
        {
            var to = compIO.Created[Toindex];
            if (Toindex == 0 && FirstFullSize)
            {
                to.transform.localPosition = new Vector3(ToX, -ToY);
                to.transform.localScale = Vector3.one;
                maxWidth = to.width;
                ToNexLine();
                continue;
            }
            if (Toindex == compIO.Created.Count - 1&&IsFirst)
            {
                ToY += 50f;

            }
            to.transform.localPosition = new Vector3(ToX,-ToY);
            to.transform.localScale = Vector3.one * 0.5f;
            maxWidth = Mathf.Max(maxWidth, to.width / 2);
            //print(to.width / 2);
            //print(maxWidth);

            ToY += 50;
            if (IsFirst) { IsFirst = false; }
            else { ToNexLine(); }
        }
        float finalX = ToX + maxWidth;
        if (IsFirst) finalX += 120f;
        else finalX += 20f;
        width = finalX;
    }

}
