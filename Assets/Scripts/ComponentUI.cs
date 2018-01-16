using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 家居小部件展示界面的UI。
/// </summary>
public class ComponentUI : MonoBehaviour {

    public Button turnBackButton;
    public Button showDeskButton;
    public Button showBackrestButton;
    public Button showLegButton;
    public Button resetButton;
    public Text noteInComponent;
    public Image logoInComponent;
    public Text nameOfComponent;

    public BGImg bgImg;

    // Use this for initialization
    void Start ()
    {
        showDeskButton.onClick.AddListener(OnDeskButton);
        showBackrestButton.onClick.AddListener(OnBackrestButton);
        showLegButton.onClick.AddListener(OnLegButton);
        resetButton.onClick.AddListener(OnReturnState);
        turnBackButton.onClick.AddListener(OnTurnBack);
    }

    public void Show()
    {
        turnBackButton.enabled = true;
        HideGrphic(resetButton.GetComponent<Image>(), false);
        HideGrphic(showLegButton.GetComponent<Image>(), false);
        HideGrphic(showBackrestButton.GetComponent<Image>(), false);
        HideGrphic(showDeskButton.GetComponent<Image>(), false);
        HideGrphic(turnBackButton.GetComponent<Image>(), false);
        HideGrphic(logoInComponent, false);
        HideGrphic(noteInComponent, false);
    }

    /// <summary>
    /// 在桌面部件点击了之后，显示桌面部件的模型，并将代表桌面部件的UI置灰。
    /// </summary>
    public void OnDeskButton()
    {
        bgImg.OnDeskButton();
        showDeskButton.interactable = false;
        showBackrestButton.interactable = true;
        showLegButton.interactable = true;

        RefreshComponentName();
    }

    /// <summary>
    /// 在代表圈椅靠背的UI点击了之后，显示桌椅靠背部分的模型，并将代表靠背部分的UI置灰
    /// </summary>
    private void OnBackrestButton()
    {
        bgImg.OnBackrestButton();

        showDeskButton.interactable = true;
        showBackrestButton.interactable = false;
        showLegButton.interactable = true;

        RefreshComponentName();
    }

    /// <summary>
    /// 在代表圈椅脚的UI点击了之后，显示椅脚部分的模型，并将代表椅脚部分的UI置灰
    /// </summary>
    private void OnLegButton()
    {
        bgImg.OnLegButton();

        showDeskButton.interactable = true;
        showBackrestButton.interactable = true;
        showLegButton.interactable = false;

        RefreshComponentName();
    }

    private void OnReturnState()
    {
        DecomAnimatorController.Return();
    }

    /// <summary>
    /// 点击了返回按钮，回退到整体家居展示的界面
    /// </summary>
    private void OnTurnBack()
    {
        //返回整体家居界面
        //if (go) Destroy(go);
        turnBackButton.enabled = false;

        HideGrphic(showLegButton.GetComponent<Image>(), true);
        HideGrphic(showBackrestButton.GetComponent<Image>(), true);
        HideGrphic(showDeskButton.GetComponent<Image>(), true);
        HideGrphic(turnBackButton.GetComponent<Image>(), true);
        HideGrphic(logoInComponent, true);
        HideGrphic(noteInComponent, true);
        HideGrphic(nameOfComponent, true);

        resetButton.GetComponent<Image>().DOFade(0, 1).OnComplete(() =>
        {
            //HideImage(EnterBtn.gameObject, false);
            //HideImage(BackBtn.gameObject, false);           
            //HideImage(logoInFurniture.gameObject, false);
            //HideText(noteInFurniture.gameObject, false);
            nameOfComponent.text = null;

            //显示整体家具展示界面的UI
            bgImg.furnitureUI.gameObject.SetActive(true);
            //EnterBtn.enabled = true;
        });

        showDeskButton.interactable = true;
        showBackrestButton.interactable = true;
        showLegButton.interactable = true;

        bgImg.OnTurnBack();
    }

    private void RefreshComponentName()
    {
        if (nameOfComponent.text != null)
        {
            nameOfComponent.transform.GetComponent<Text>().DOFade(0, 0.5f).OnComplete(() =>
            {
                nameOfComponent.text = ComponentAttributes.nameOfComponent;
                nameOfComponent.transform.GetComponent<Text>().DOFade(1, 0.5f);
            });
        }
        else
        {
            nameOfComponent.text = ComponentAttributes.nameOfComponent;
            nameOfComponent.transform.GetComponent<Text>().DOFade(1, 1);
        }
    }

    private void HideGrphic(Graphic graphic, bool isHide)
    {
        int time = 1;
        int alpha = isHide ? 0 : 1;
        graphic.DOFade(alpha, time);
    }
}
