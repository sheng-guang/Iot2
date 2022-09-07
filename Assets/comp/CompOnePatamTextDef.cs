using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CompOnePatamTextDef : CompOneParam,ITypeDef
{


    public TMP_InputField defText;

    public string DefName => defText.text;
    public override void Awake()
    {
        base.Awake();
        //print(" awake def  " + up.returnType);

        ParamDef.Add(up.returnType,this);
        defText.onEndEdit.AddListener(OnEndEdit);
    }
    public void OnEndEdit(string s)
    {
        ParamDef.TriggerChange(up.returnType);
    }
    public void OnDestroy()
    {
        ParamDef.getList(up.returnType).Remove(this);
        ParamDef.TriggerChange(up.returnType);
    }

}
