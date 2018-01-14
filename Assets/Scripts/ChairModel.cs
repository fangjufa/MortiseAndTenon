using UnityEngine;
using System.Collections;
using System.Xml;

public class ChairModel  {
    private static ChairModel _instance;
    public static ChairModel Instance
    {
        get 
        {
            if (_instance == null) _instance = new ChairModel();
            return _instance;
        }
    }
    
    private Transform _target = null;
    private string _baozha;
    //private string _realName;

    public Transform Target
    {
        get { return _target; }
        set { _target = value; }
    }
    public string Baozha
    {
        get { return _baozha; }
        set { _baozha = value; }
    }
}
