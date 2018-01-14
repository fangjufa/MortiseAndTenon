using LitJson;
using UnityEngine;
using System.Collections.Generic;
public class ClipNameManager {

    public JsonData data;
    public ClipNameManager()
    {
        string config = Resources.Load<TextAsset>("Config").text;
        data = JsonMapper.ToObject(config);
    }

    public string GetClipName(string name)
    {
        string result = "";
        int len = data.Count;
        for (int i = 0; i < len; i++)
        {
            if (data[i].Keys.Contains(name))
            {
                result = data[i][name].ToString();
                break;
            }
        }           
        return result;
    }

    private static ClipNameManager instance;

    public static ClipNameManager Instance
    {
        get
        {
            if (instance == null) instance = new ClipNameManager();
            return ClipNameManager.instance;
        }
    }
}