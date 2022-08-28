using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class InputManager : MonoBehaviour,IDeselectHandler,ISelectHandler
{
    public static TMP_InputField InputField;
    public void Init()
    {
        InputField = GetComponent<TMP_InputField>();

    }

    public void OnDeselect(BaseEventData eventData)
    {
        print("deselect InputField");
    }
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        print("slect InputField ");

    }
    public static void OnBlockDeslect(Block b)
    {
    }
    public static void Focus(Block b)
    {
        if (b == null) return;
        //print("focuse on" + b);
        OnFocuse = b;
    }
     static Block OnFocuse;
    string LastString = "";
    public bool LasIsNull => string.IsNullOrEmpty(LastString);
    public void FreshInput()
    {

        var NowStr = InputField.text;
        if ( Input.GetKeyDown(KeyCode.Return))
        {
            if (LasIsNull) { OnFocuse.enter(KeyCode.Return); }
            else { OnFocuse.enter(LastString); }
        }
        if (LasIsNull)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                OnFocuse.enter(KeyCode.Backspace);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                OnFocuse.enter(KeyCode.LeftArrow);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                OnFocuse.enter(KeyCode.RightArrow);
            }
        }

        LastString = NowStr;

    }
    public void FreshFocusPoss()
    {
        if (OnFocuse)
        {
            EventSystem.current.SetSelectedGameObject(InputField.gameObject);
            InputField.transform.position = OnFocuse.InputFocusePoint;
        }
    }

}
