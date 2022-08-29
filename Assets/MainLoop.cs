using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoop : MonoBehaviour
{
    public Block RootBlock;
    void Start()
    {

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
        RootBlock.FreshSize(false,1);
        manager.FreshFocusPoss();
    }
}
