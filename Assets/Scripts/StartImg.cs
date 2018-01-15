using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 最开始进入场景时显示的UI界面。
/// </summary>
public class StartImg : MonoBehaviour {

    //public float time = 1;
    public GameObject box;

	// Use this for initialization
	void Start () {
        EventTriggerListener.Get(gameObject).onClick_ = OnMouseClick;
    }

    private void OnMouseClick(GameObject go, UnityEngine.EventSystems.PointerEventData data)
    {
        transform.GetComponent<Image>().DOFade(0, 1.0f).OnComplete(() =>
        {            
            box.SetActive(false);
            gameObject.SetActive(false);
        });
    }

}
