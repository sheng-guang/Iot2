using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GenerateCode : MonoBehaviour
{
    public BlockRootMain main;
    public TMP_InputField codeText;

    public void GenCode()
    {
        var code = new CodeFile();
        main.GenCode(code);
        codeText.text = code.getCode();
    }
}
