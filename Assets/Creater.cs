using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Creater
{
 
    public static void AddBlcok(Block b)
    {
  
        dic[b.ResName] = b;
    }
   static Dictionary<string, Block> dic = new Dictionary<string, Block>();
    public static Block getPre(string s)
    {
        dic.TryGetValue(s, out var re);
        return re;
    }
}
