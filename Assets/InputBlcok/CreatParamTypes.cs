//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class CreatParamTypes : MonoBehaviour
//{
//    [Header("to write")]

//    public BlockParam pre;
//    public List<BlockParam> created;
//    IEnumerator destory(GameObject g)
//    {
//        DestroyImmediate(g);
//        yield return 1;

//    }
//    public void clearBreated()
//    {
//        foreach (var item in Created)
//        {
//            if (item == null) continue;
//            //Destroy(item.gameObject);
//            StartCoroutine(destory(item.gameObject));
//        }
//        Created.Clear();
//    }
//}
