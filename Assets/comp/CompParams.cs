using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.Progress;

[Serializable]
public class OneParam
{
    public string Name;
    public string Type;
}


public class CompParams : MonoBehaviour
{
    public CompOneParam pre;
    public BlockFunction funcBlock;
    private void Awake()
    {
        funcBlock = GetComponent<BlockFunction>();
        pre.gameObject.SetActive(false);
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
        for (int i = 0; i < pre.transform.parent.childCount; i++)
        {
            var to = pre.transform.parent.GetChild(i);
            if (to == pre.transform) continue;
            StartCoroutine(destory(to.gameObject));
        }
        created.Clear();
        for (int i = 0; i < type.Count; i++)
        {
            var to = type[i];
            var ne = Instantiate(pre, pre.transform.parent);
            created.Add(ne);
            ne.gameObject.SetActive(true);
            ne.SetType(to);
        }
    }




    
}
