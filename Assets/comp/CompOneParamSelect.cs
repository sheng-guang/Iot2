using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class CompOneParamSelect : CompOneParam
{
    public TMP_Dropdown dropdown;
    public override void Awake()
    {
        base.Awake();
        dropdown = GetComponentInChildren<TMP_Dropdown>();
        ParamDef.Listen(Type, Fresh);
    }

    public void Fresh()
    {
        //print("rop down count "+to.Count);
        var last = dropdown.captionText.text;
        //print(last);
        List<TMP_Dropdown.OptionData> d = new List<TMP_Dropdown.OptionData>();
        var to = ParamDef.getList(Type);
        int toValue = 0;
        for (int i = 0; i < to.Count; i++)
        {
            var toto = to[i];
            if (toto == null||toto.Equals(null)) continue;
            d.Add(new TMP_Dropdown.OptionData(to[i].DefName));
            if (to[i].DefName == last) toValue = i;
        }
        dropdown.options = d;
        dropdown.value = toValue;
        //if (dropdown.value != now) dropdown.value = 0;
    }

}
