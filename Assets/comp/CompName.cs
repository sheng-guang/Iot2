using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CompName : MonoBehaviour
{
    public void SetNameFresh(string s,string returnT)
    {
        ResName = s;
        returnType = returnT;
        OnValidate();
    }
    public string ResName;
    public string returnType;
    public TextMeshProUGUI ResNameText;

    private void OnValidate()
    {
        Image img = GetComponent<Image>();
        var to = GetComponent<BlockFunction>();
        var to2 = GetComponent<BlockParam>();
        if (img&&(to||to2))
        {
            if (returnType == "bool")
            {
                GetComponent<Image>().color = new Color(191f / 255, 253f / 255, 142f / 255);
            }
            else
            {
                GetComponent<Image>().color = new Color(167f / 255, 236f / 255, 255f / 255);
            }
        }
  

        var b= GetComponent<Block>();
        if (b == null) return;
        b.DefResName = ResName;
        b.ReturnTypeDefault = returnType;
        name = b.GetGoName();
      if(ResNameText)  ResNameText.text = b.GetTextName();
    }
}
