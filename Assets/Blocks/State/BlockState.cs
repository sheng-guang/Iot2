using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BlockState : BlockRootWidth
{
    //public TMP_InputField Name;
    //public string key => Name.text;

    //public virtual void Awake()
    //{
    //    Name.onEndEdit.AddListener(OnChange);
    //   g= GetComponentInParent<CompStateGroup>();

    //}
    //public CompStateGroup g;
    //public void OnChange(string s)
    //{
    //    if (g) g.Put(s, this);
    //}
    //private void OnDestroy()
    //{

    //}
    public void Awake()
    {
        group = transform.parent.GetComponent<CompStateGroup>();
        if (group == null)
        {
            //Destroy(gameObject);
            return;
        }
        NameText.onEndEdit.AddListener(onEndEdit);
        list.Add(this);
        for (int i = 0; i < TarGetRoot.childCount; i++)
        {
            var to = TarGetRoot.GetChild(i);
            Targets.Add(to);
        }
    }
    [Header("state")]

    public string NowName;

    public CompStateGroup group;
    public List<BlockState> list => group.list;
    public Dictionary<string, BlockState> dic => group.dic;

    public int index { get { return transform.GetSiblingIndex(); } }

    [Header("to write")]
    public TMP_InputField NameText;
    public Transform TarGetRoot;
     List<Transform> Targets=new List<Transform>();
    public Vector3 getCloset(Vector3 v)
    {
        var cloest = Targets[0];
        float dis = Vector3.Distance(Targets[0].position, v);
        for (int i = 1; i < Targets.Count; i++)
        {
            var to = Vector3.Distance(Targets[i].position, v);
            if (to > dis) continue;
            dis = to; cloest = Targets[i];
        }
        return cloest.position;
    }
    public BlockState GetPre()
    {
        BlockState re = null;
        int max = int.MinValue;
        foreach (var item in list)
        {
            if (item == null) continue;
            if (item == this) continue;
            if (item.index > index) continue;
            if (item.index < max) continue;
            max = item.index;
            re = item;
        }
        return re;
    }
    public BlockState getNext()
    {
        BlockState re = null;
        int min = int.MaxValue;
        foreach (var item in list)
        {
            if (item == null) continue;
            if (item == this) continue;
            if (item.index < index) continue;
            if (item.index > min) continue;
            min = item.index;
            re = item;
        }
        return re;
    }





    public void onEndEdit(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) s = "";
        s = SetTar(s, this);
        NowName = s;

        NameText.text = s;
        //foreach (var item in dic)
        //{
        //    Debug.Log(item.Key);
        //}
    }
    public string SetTar(string name, BlockState s)
    {
        if (getTar(name) == null) { dic[name] = s; return name; }
        int addtion = 1;
        while (getTar(name + addtion) != null)
        {
            addtion++;
        }
        dic[name + addtion] = s;

        return name + addtion;
    }
    public BlockState getTar(string name)
    {
        dic.TryGetValue(name, out var re);
        //Debug.Log(re);
        if (re && re.NowName != name) return null;
        return re;
    }
}

