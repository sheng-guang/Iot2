using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBlcokHub : MonoBehaviour
{
    UIFreshSize fresh;
    public Transform HideTr;
    public void init()
    {
        fresh = GetComponent<UIFreshSize>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var b = transform.GetChild(i).GetComponent<Block>();
            if (b == false) continue;
            Creater.AddBlcok(b);
            list.Add(b);
        }
        for (int i = 0; i < HideTr.childCount; i++)
        {
            var b = HideTr.GetChild(i).GetComponent<Block>();
            if (b == false) continue;
            Creater.AddBlcok(b);
        }
        HideTr.gameObject.SetActive(false);
    }

    public List<Block> list = new List<Block>();
    public List<Block> showing = new List<Block>();
    public bool hasShowing => showing.Count != 0;
    public void SetKey(string key)
    {
        showing.Clear();
        if (string.IsNullOrWhiteSpace(key))
        {
            gameObject.SetActive(false);return;
        }
        gameObject.SetActive(true);
        if (key=="+")
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                bool active = list[i].ResName.ToLower().Contains(key);
                
                list[i].gameObject.SetActive(active);
                if (active) showing.Add(list[i]);

            }   
        }

        fresh.FreshSize(true,true);
    }
    
}
