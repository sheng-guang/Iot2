using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class OneParm
{
    public string name;
    public string Type;
}




public class CompParams : MonoBehaviour
{
    public CompOneParam pre;
    private void Awake()
    {
        pre.gameObject.SetActive(false);
    }
    private void OnValidate()
    {
        if (Application.isPlaying) return;
        SetParams();
    }
    public List<OneParm> p;
    public List<CompOneParam> created;
    IEnumerator destory(GameObject g)
    {
        yield return 1;
        DestroyImmediate(g);
    }
    public void SetParams()
    {

        foreach (var item in created)
        {
            if (item) { StartCoroutine(destory(item.gameObject)); }
        }
        created.Clear();
        for (int i = 0; i < p.Count; i++)
        {
            var to = p[i];
            var ne = Instantiate(pre, pre.transform.parent);
            created.Add(ne);
            ne.gameObject.SetActive(true);
            ne.SetType(to);
            ne.transform.localPosition = getLoCalPositon(i);
        }
    }

    public Vector3 getLoCalPositon(int index)
    {
        if (index == 0) return Vector3.zero;
        var line = (index + 1) / 2;
        var lineIndex = (index + 1) % 2;
        return new Vector3(lineIndex * 100, -line * 50);
    }
}
