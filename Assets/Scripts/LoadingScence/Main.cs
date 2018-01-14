using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class Main : MonoBehaviour {

    public Text showProgressText;

    private AsyncOperation async;

    private int progress;

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadScence());  
	}
	
	// Update is called once per frame
	void Update () {
        progress = (int)(async.progress * 100);
        showProgressText.text = string.Format("{0}%", progress);
    }

    IEnumerator LoadScence()
    {
        async = SceneManager.LoadSceneAsync("Home");
        yield return async;
    }
}
