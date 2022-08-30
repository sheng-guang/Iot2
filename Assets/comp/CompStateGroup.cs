using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompStateGroup : MonoBehaviour
{
    public Dictionary<string, BlockState> dic = new Dictionary<string, BlockState>();
    public List<BlockState> list = new List<BlockState>();
    //Dictionary<string, BlockState> dic = new Dictionary<string, BlockState>();
    //public void Put(string key,BlockState s)
    //{
    //    dic[key] = s;
    //}
    //public BlockState Get(string key)
    //{
    //    dic.TryGetValue(key,out var re);
    //    if (re == null) return null;
    //    if (re.key != key) return null;
    //    return re;
    //}
}
