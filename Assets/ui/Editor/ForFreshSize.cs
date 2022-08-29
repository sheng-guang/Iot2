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
        if (GUILayout.Button("fresh")) tar.FreshSize(true,0.5f);

        base.OnInspectorGUI();
    }

}
