using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Link <key,T>
{
    static Dictionary<key, T> dic = new Dictionary<key, T>();
    public static void LinkKey( T value, key key)
    {
        dic[key] = value;

    }
    public static T GetValue(key key)
    {
        dic.TryGetValue(key,out var re);
        return re;
    }
}

public static class LinkBlock
{
public static void LinkWithGo(this Block b,GameObject g)
    {
        Link<GameObject,Block>.LinkKey(b,g);
        Link<Transform,Block>.LinkKey(b,g.transform);
    }
    public static Block GetBlock(this GameObject g)
    {
        return Link<GameObject,Block>.GetValue(g);
    }
    public static Block GetBlock(this Transform g)
    {
        return Link<Transform, Block>.GetValue(g);
    }
}