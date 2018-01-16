using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 整体家具展示界面。
/// </summary>
public class FurnitureUI : MonoBehaviour {

    //进入模块界面的按钮。
    public Button EnterBtn;

    //返回到有个圆圈的界面的按钮
    public Button BackBtn;

    public Text noteInFurniture;
    public Image logoInFurniture;

    public BGImg bgImg;

    // Use this for initialization
    void Start ()
    {
        EnterBtn.onClick.AddListener(IntoComponentScene);
        BackBtn.onClick.AddListener(ShowBG);
    }

    //在点击了返回按钮之后，返回到有个圆圈的界面
    private void ShowBG()
    {
        bgImg.ShowBG();
        //ChairController.PlayHebing();
        ////显示BG
        //isBackToBG = true;
        //bgPanel.SetActive(true);
        ///*----------------隐藏带圈的UI界面----------------*/
        //HideImage(logoInBG.gameObject, false);

        //HideText(ChairBtn.gameObject, false);
        //HideText(nameOfFurniture.gameObject, false);
        ///*--------------------------------------------------------*/

        //camController.GetComponent<CameraController>().BackToBG(ChairBtn);

        //Camera.main.GetComponent<AudioSource>().DOFade(1, 1);

        //BG.DOFade(1, time);//.OnComplete(() =>
    }

    private void IntoComponentScene()
    {
        //进入家居小部件展示场景
        //isChange = true;
        //EnterBtn.enabled = false;

        HideGrphic(EnterBtn.GetComponent<Image>(), true);
        HideGrphic(BackBtn.GetComponent<Image>(), true);
        HideGrphic(noteInFurniture, true);

        //camController.GetComponent<CameraController>().IntoComponent();
        

        logoInFurniture.DOFade(0, 1).OnComplete(() =>
        {
            bgImg.ShowComponentUI();
            //turnBackButton.enabled = true;
            //canvasOne.SetActive(false);
            //HideImage(resetButton.gameObject, false);
            //HideImage(showLegButton.gameObject, false);
            //HideImage(showBackrestButton.gameObject, false);
            //HideImage(showDeskButton.gameObject, false);
            //HideImage(turnBackButton.gameObject, false);
            //HideImage(logoInComponent.gameObject, false);
            //HideText(noteInComponent.gameObject, false);
        });

        bgImg.IntoComponentScene();
        //transitName = roundDeskFurniture.name;
        //Debug.Log(FurnitureList[0].name);
    }

    private void HideGrphic(Graphic graphic, bool isHide)
    {
        int time = 1;
        int alpha = isHide ? 0 : 1;
        graphic.DOFade(alpha, time);
    }

    private void OnEnable()
    {
        HideGrphic(EnterBtn.GetComponent<Image>(), false);
        HideGrphic(BackBtn.GetComponent<Image>(), false);
        HideGrphic(logoInFurniture, false);
        HideGrphic(noteInFurniture, false);
        //nameOfComponent.text = null;
        EnterBtn.enabled = true;
    }
}
