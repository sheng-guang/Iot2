using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public interface ITypeDef
{
    string DefName { get; }
}
public static class ParamDef
{
    class one : ITypeDef
    {
        public string DefName { get; set; }
    }
    static Dictionary<string, List<ITypeDef>> dic = new Dictionary<string, List<ITypeDef>>();
    public static void Add(string type,string value)
    {
        var l = getList(type);
        l.Add(new one() { DefName = value });
    }
    public static void Add(string type, ITypeDef value)
    {
        var l = getList(type);
        l.Add(value);
    }
    public static List<ITypeDef> getList(string type)
    {
        //Debug.Log(" get " + type);
        if (dic.ContainsKey(type)==false) dic[type] = new List<ITypeDef>();
        return dic[type];
    }

    static Dictionary<string, List<Action>> OnChange = new Dictionary<string, List<Action>>();
    public static void Listen(string t,Action a)
    {
        //Debug.Log("listen " + t);
        if (OnChange.ContainsKey(t) == false) OnChange[t] = new List<Action>();
        OnChange[t].Add(a);
        a.Invoke();
    }
    public static void TriggerChange(string t)
    {
        //Debug.Log("trigger    "+t);

        OnChange.TryGetValue(t, out var re);
        if (re == null) return;
        //Debug.Log("Fresh" +re.Count);
        foreach (var item in re)
        {
            item?.Invoke();
        }
    }
}
public class CompOneParam : MonoBehaviour, IPointerClickHandler, InputEnter,BlockUp
{


   protected RectTransform rt;
    protected Block up;
    public virtual void Awake()
    {
        up = GetComponentInParent<Block>();
        rt = GetComponent<RectTransform>();
    }
    public TextMeshProUGUI TypeText;
    public Vector3 InputFocusePoint => transform.position+Vector3.right* 200*transform.lossyScale.x;
    public virtual void SetType(OneParam p)
    {
        ParamName = p.Name;
        Type = p.Type;
       if(TypeText) TypeText.text = p.Name + "\n(" + p.Type+")";
    }
    public string ParamName;
    public string Type;
    public Transform CreatedPar => transform.GetChild(0);
    public virtual void enter(string toName)
    {
        var pre = Creater.getPre(toName);
        if (pre == null) return ;
        if (pre.returnType != Type) return;
        var ne = Instantiate(pre, CreatedPar);
        created = ne;
        ne.AfterEnter();
        ne.SetUp(this);
        //ne.transform.SetSiblingIndex(index);
        ne.transform.localPosition = Vector3.zero;
        ne.transform.localScale = Vector3.one;
        InputManager.Focus(created);
    }

    public virtual void enter(KeyCode s)
    {
        if(s== KeyCode.Backspace) {if(created) DestroyImmediate(created); }
    }
   
    public Block created = null;
    public void OnPointerClick(PointerEventData eventData)
    {
        InputManager.Focus(this);
    }

    public void MoveDirection(int index, KeyCode key)
    {
    }

    public Block EntarChild(int index, string resName)
    {
        print("no creat");
        return null;
    }

    public void DeletChild(Block b)
    {
        //print("here");
        if (created) Destroy(created.gameObject);
        InputManager.Focus(this);
    }

    public void freshSize(bool getComp, bool minTimes)
    {
        if (created)
        {
            created.FreshSize(getComp, minTimes);
            created.transform.localPosition = Vector3.zero;
            created.transform.localScale = Vector3.one;
        }
    }
    public float width => created ? created.width:200;
    public float height => created ? created.height : 100;

}
