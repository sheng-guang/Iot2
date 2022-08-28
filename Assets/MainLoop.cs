using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoop : MonoBehaviour
{
    public Transform BlockPreList;
    public Block RootBlock;
    void Start()
    {
        for (int i = 0; i < BlockPreList.childCount; i++)
        {
            var c = BlockPreList.GetChild(i);
            var b = c.GetComponent<Block>();
            Creater.AddBlcok(b);
        }
        manager.Init();
        Update();
        InputManager.Focus(RootBlock.GetChildBlock(0));
    }
    private void OnEnable()
    {
        
    }
    public InputManager manager;
    void Update()
    {
        manager.FreshInput();
        RootBlock.EnsureChildAfter();
        RootBlock.FreshSize();
        manager.FreshFocusPoss();
    }
}
