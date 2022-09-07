using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class nn
{
   public const string DEFINE = nameof(DEFINE);
   public const string HIGH = nameof(HIGH);
   public const string LOW = nameof(LOW);
   public const string WIFI_Type = nameof(WIFI_Type);


}
public class AwakeAddDefine : MonoBehaviour
{

    private void Awake()
    {
        ParamDef.Add(nn.DEFINE, nn.HIGH);
        ParamDef.Add(nn.DEFINE,nn.LOW);
        ParamDef.Add(nn.DEFINE,nn.WIFI_Type);
        //ParamDef.Add(nn.DEFINE,nn.LOW);
    }
}
