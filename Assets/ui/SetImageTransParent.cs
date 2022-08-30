using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetImageTransParent : MonoBehaviour
{
    [Range(0,1)]
    public float a;
    private void OnValidate()
    {
        var i = GetComponent<Image>();
        if (i == null) return;
        var old = i.color;
        old.a = a;
        i.color = old;
    }
}
