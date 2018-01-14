using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class StartImg : MonoBehaviour {

    public float time = 1;
    public GameObject box;

	// Use this for initialization
	void Start () {
        EventTriggerListener.Get(gameObject).onClick_ = OnMouseClick;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseClick(GameObject go, UnityEngine.EventSystems.PointerEventData data)
    {
        transform.GetComponent<Image>().DOFade(0, time).OnComplete(() =>
        {
            box.SetActive(false);
            gameObject.SetActive(false);
        });
    }

}
