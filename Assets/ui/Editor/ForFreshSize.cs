using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Editor<T> : Editor where T : class
{
    public T tar;
    private void Awake()
    {
        tar = target as T;
    }
}

[CustomEditor(typeof(UIFreshSize))]
public class ForUIContent : Editor<UIFreshSize>
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("fresh")) tar.FreshSize(true, true);

        base.OnInspectorGUI();
    }

}

[CustomEditor(typeof(CreateSelfDefBlock))]
public class ForCreateSelfDefBlock : Editor<CreateSelfDefBlock>
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Load")) tar.load();

        base.OnInspectorGUI();
    }

}
[CustomEditor(typeof(CompName))]
public class ForCompName : Editor<CompName>
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("CreatCopy"))
        {
            var index = tar.transform.GetSiblingIndex();
           var ne= Object.Instantiate(tar, tar.transform.parent);
            ne.transform.SetSiblingIndex(index + 1);
            tar.GetComponentInParent<UIFreshSize>().FreshSize(true,true);
            Selection.objects = new Object[] { ne.gameObject };
        }
        if(GUILayout.Button("fresh layout"))
        {
            tar.transform.parent.GetComponentInParent<UIFreshSize>().FreshSize(true, true);
        }
        base.OnInspectorGUI();
    }

}
