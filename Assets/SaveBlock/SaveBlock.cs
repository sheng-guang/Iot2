using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Newtonsoft.Json;
using System;


public class SaveBlock : MonoBehaviour
{
    public Block RootBlock;
    public Button saveButton;
    public Button LoadButton;
    public Button GenerateCodeButton;
    public TMP_InputField nameText;
    public string ResourcesPath = "codes";
    private void Awake()
    {
        saveButton.onClick.AddListener(SaveCode);
        LoadButton.onClick.AddListener(LoadCode);
        GenerateCodeButton.onClick.AddListener(GenerateCode);
    }
    [ContextMenu("save")]
    public void SaveCode()
    {
        if (string.IsNullOrWhiteSpace(ResourcesPath)) return;
        print("save");

        var re = RootBlock.GetRecord();

        string jsonString = JsonConvert.SerializeObject(
            re,
            Formatting.Indented,
            new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

        var p = GetNowPath();
        print(p);
        //Debug.Log(jsonString);
        File.WriteAllText(p, jsonString);

    }
    public string GetNowPath()
    {
        var nam = nameText.text;
        if (string.IsNullOrWhiteSpace(nam)) nam = "-";
        return Application.dataPath + "/Resources/" + ResourcesPath + "/" + nam + ".json";
    }
    public void LoadCode()
    {
        var s = File.ReadAllText(GetNowPath());
        var root = JsonConvert.DeserializeObject<BlockRecordNode>(s);
        RootBlock.ApplyRecord(root);
    }
    public void GenerateCode()
    {

        return;
    }
}
