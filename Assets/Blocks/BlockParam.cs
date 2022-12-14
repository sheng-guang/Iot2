using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockParam : Block
{
    public override bool OnlyShowType => true;
    //public override void FreshWhileInHub()
    //{
    //    base.FreshWhileInHub();
    //    print("1111   "+name);
    //   var c= GetComponentInChildren<CompOneParamSelect>();
    //    c.SetType(new OneParam() {Type=returnType, UItype = ParamUI.select });
    //}

    public TMP_Dropdown dropdown;
    public virtual void Awake()
    {
        dropdown = GetComponentInChildren<TMP_Dropdown>();
        ParamDef.Listen(returnType, Fresh);
    }
    public void Fresh()
    {
        //print("rop down count "+to.Count);
        var last = dropdown.captionText.text;
        print(last);
        List<TMP_Dropdown.OptionData> d = new List<TMP_Dropdown.OptionData>();
        var to = ParamDef.getList(returnType);
        int toValue = 0;
        for (int i = 0; i < to.Count; i++)
        {
            var toto = to[i];
            if (toto == null || toto.Equals(null)) continue;
            d.Add(new TMP_Dropdown.OptionData(to[i].DefName));
            if (to[i].DefName == last) toValue = i;
        }
        dropdown.options = d;
        dropdown.value = toValue;
        //if (dropdown.value != now) dropdown.value = 0;
    }
}
