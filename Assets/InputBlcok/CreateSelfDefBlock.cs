using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSelfDefBlock : MonoBehaviour
{

    public List<Block> Created = new List<Block>();
    public UIFreshSize layout;
    [ContextMenu("load")]
    public void load()
    {
        clearBreated();
        var to = Resources.Load<TextAsset>("blocks");
        char[] splits = { '\n', '\r' };
        var lists = to.text.Split(splits, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in lists)
        {
            if (item.StartsWith("//")) continue;
            createOneBlock(item);
        }
        if (layout == null) layout = GetComponent<UIFreshSize>();
        layout.FreshSize(true,true);
    }
    public void clearBreated()
    {
        foreach (var item in Created)
        {
            if (item == null) continue;
            //Destroy(item.gameObject);
            StartCoroutine(destory(item.gameObject));
        }
        Created.Clear();
    }
    IEnumerator destory(GameObject g)
    {
        DestroyImmediate(g);
        yield return 1;

    }
    [Header("to write")]

    public BlockFunction pre;
    public BlockFunction Pre250width;
    public Transform RootTr;
    public void createOneBlock(string line)
    {
        var ll = line.Split(",");
        bool iswidth = string.IsNullOrWhiteSpace(ll[4]) == false;
        var ne = Instantiate(iswidth? Pre250width:pre, RootTr);
        ne.compIO.pre.gameObject.SetActive(false);
        Created.Add(ne);
        var neName = ne.GetComponent<CompName>();
        neName.SetNameFresh(ll[0], ll[1]);


        var ll2 = ll[2].Split('[', System.StringSplitOptions.RemoveEmptyEntries);
        //print(ll2.Length);
        ne.compIO.type.Clear();
        foreach (var item in ll2)
        {
            var neParam = new OneParam();
            ne.compIO.type.Add(neParam);
            neParam.Type = item;
        }

        var ll3 = ll[3].Split('[', System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < ll3.Length && i < ne.compIO.type.Count; i++)
        {
            ne.compIO.type[i].Name = ll3[i];
        }


        ne.compIO.FreshParams();
    }

}
