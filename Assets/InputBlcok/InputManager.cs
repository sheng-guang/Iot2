using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class InputManager : MonoBehaviour,IDeselectHandler,ISelectHandler
{
    public static TMP_InputField InputField;
    public InputBlcokHub blockList;
    [Header("to write")]
    public TMP_InputField f;

    public void Init()
    {
        InputField = f;
        blockList = GetComponentInChildren<InputBlcokHub>();
        blockList.init();
    }

    public void OnDeselect(BaseEventData eventData)    {        print("deselect InputField");    }
    void ISelectHandler.OnSelect(BaseEventData eventData)    {        print("slect InputField ");    }
    public static void OnBlockDeslect(Block b)    {    }
    public static void Focus(InputEnter b)
    {
        if (b == null) return;
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(InputField.gameObject);
        InputField.text = "";
        //print("focus  " + b.ToString().Replace("(Clone)",""));
        OnFocuse = b;
    }
    public bool IsInputFocuse => EventSystem.current.currentSelectedGameObject == InputField.gameObject;
     static InputEnter OnFocuse;
    string LastString = "";
    public bool SingleKey => string.IsNullOrEmpty(LastString)&&IsInputFocuse;
    public void FreshInput()
    {
        
        var NowStr = InputField.text;
        blockList.SetKey(NowStr);
        if ( Input.GetKeyDown(KeyCode.Return))
        {
            if (SingleKey)
            {
                print("input  enter");
                OnFocuse.enter(KeyCode.Return);
            }
            else
            {
                if(blockList.hasShowing)
                    OnFocuse.enter(blockList.showing[0].ResName);
                InputField.text = "";
            }
        }
        void IfKeyDownSend(KeyCode k)
        {
            if (Input.GetKeyDown(k)) OnFocuse.enter(k);
        }
        if (SingleKey)
        {
            IfKeyDownSend(KeyCode.Backspace);

            IfKeyDownSend(KeyCode.Space);
            IfKeyDownSend(KeyCode.Tab);

            IfKeyDownSend(KeyCode.LeftArrow);
            IfKeyDownSend(KeyCode.UpArrow);
            IfKeyDownSend(KeyCode.DownArrow);
            IfKeyDownSend(KeyCode.RightArrow);

        }

        LastString = NowStr;
    }
    public void FreshFocusPoss()
    {
        if (OnFocuse!=null&&OnFocuse.Equals(null)==false)
        {
            transform.position = OnFocuse.InputFocusePoint;
        }
    }

}
public interface InputEnter
{
Vector3 InputFocusePoint { get; }
    public void enter(string s); 
    public void enter(KeyCode s); 
}