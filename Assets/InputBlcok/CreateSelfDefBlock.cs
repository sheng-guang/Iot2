using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSelfDefBlock : MonoBehaviour
{

    public List<Block> Created = new List<Block>();
    public UIFreshSize layout;
    public void FreshWhileInHub()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var to = transform.GetChild(i).GetComponent<Block>();
            if (to == null) continue;
            to.FreshWhileInHub();

        }

    }
    [ContextMenu("load")]
    public void load()
    {
        clearBreated();
        var to = Resources.Load<TextAsset>("blocks");
        char[] splits = { '\n', '\r' };
        var lists = to.text.Split(splits, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in lists)
        {
            if (item.StartsWith("//")) continue;
            createOneBlock(item);
        }
        if (layout == null) layout = GetComponent<UIFreshSize>();
        FreshWhileInHub();
        layout.FreshSize(true,true);
    }
    public void clearBreated()
    {
        foreach (var item in Created)
        {
            if (item == null) continue;
            //Destroy(item.gameObject);
            StartCoroutine(destory(item.gameObject));
        }
        Created.Clear();
    }
    IEnumerator destory(GameObject g)
    {
        DestroyImmediate(g);
        yield return 1;

    }
    [Header("to write")]

    public BlockFunction pre;

    public Transform RootTr;
    public void createOneBlock(string line)
    {
        var ll = line.Split(",");

        bool FirstFullSize = string.IsNullOrWhiteSpace(ll[4]) == false;

        string Icon = ll[5];

        var to = pre;
        var ne = Instantiate(to, RootTr);
        Created.Add(ne);

        ne.SetIcon(Icon);
        ne.FirstFullSize = FirstFullSize;
        ne.SetNameFresh(ll[0], ll[1]);


        List<OneParam> parms = new List<OneParam>();
        var ll2 = ll[2].Split('[', System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in ll2)
        {
            var neParam = new OneParam();
            parms.Add(neParam);
            neParam.Type = item;
        }
        var ll3 = ll[3].Split('[', System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < ll3.Length && i < parms.Count; i++)
        {
            parms[i].Name = ll3[i];
        }
        var ll6 = string.IsNullOrWhiteSpace(ll[6])?new string[] { } : ll[6].Split('[');
        for (int i = 0; i < ll6.Length && i < parms.Count; i++)
        {
            if (string.IsNullOrWhiteSpace(ll6[i])) continue;
            if (int.TryParse(ll6[i], out var ii) == false) continue;
            parms[i].UItype = (ParamUI)ii;
        }
        ne.SetParams(parms);

    }

}


public enum ParamUI
{
    text = 1,
    testDef = 2,
    select = 3
}
[Serializable]
public class OneParam
{
    public string Name;
    public string Type;
    public ParamUI UItype = ParamUI.text;
}
