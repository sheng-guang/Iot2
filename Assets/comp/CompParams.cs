using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.Progress;



public class CompParams : MonoBehaviour
{
    public Transform preGroup;
    public CompOneParam preText;
    public CompOneParam PreTextDef;
    public CompOneParam PreSelect;

    public BlockFunction funcBlock;
    public void Awake()
    {
        funcBlock = GetComponent<BlockFunction>();
        preGroup.gameObject.SetActive(false);
    }
    //private void OnValidate()
    //{
    //    if (Application.isPlaying) return;
    //    LoadParams();
    //}
    public List<OneParam> type;
    public List<CompOneParam> created;
    IEnumerator destory(GameObject g)
    {
        yield return 1;
        DestroyImmediate(g);
    }
    public void LoadParams()
    {
        for (int i = 0; i < preGroup.parent.childCount; i++)
        {
            var to = preGroup.parent.GetChild(i);
            if (to == preGroup) continue;
            StartCoroutine(destory(to.gameObject));
        }
        created.Clear();
        for (int i = 0; i < type.Count; i++)
        {
            var to = type[i];
            CompOneParam toPre = null;
            if (to.UItype == ParamUI.select) toPre = PreSelect;
            if (to.UItype == ParamUI.testDef) toPre = PreTextDef;
            if (to.UItype == ParamUI.text) toPre = preText;
            var ne = Instantiate(toPre, preGroup.parent);
            created.Add(ne);
            ne.gameObject.SetActive(true);
            ne.SetType(to);
        }
    }




    
}
