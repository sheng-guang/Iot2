using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CompName : MonoBehaviour
{
    public string ResName;
    public TextMeshProUGUI ResNameText;

    private void OnValidate()
    {
       var b= GetComponent<Block>();
        if (b == null) return;
        b._resName = ResName;
        name = ResName;
      if(ResNameText)  ResNameText.text = ResName;
    }
}