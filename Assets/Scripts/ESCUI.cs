using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

//按下了ESC键弹出的UI及其功能。
public class ESCUI : MonoBehaviour {

    //重新加载按钮
    public Button reloadButton;

    //继续按钮
    public Button continueButton;

    //退出按钮
    public Button exitButton;
    
    //退出UI的父物体。
    //public GameObject escPanel;

    public BGImg bgImg;

    // Use this for initialization
    void Start ()
    {
        reloadButton.onClick.AddListener(ReloadScence);
        continueButton.onClick.AddListener(ContinueGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void OnEnable()
    {
        //reloadButton
        GetComponent<Image>().DOFade(1, 0.5f).OnComplete(() =>
        {
            Time.timeScale = 0;
        });
    }

    private void ReloadScence()
    {
        //为了在重新加载场景时，按钮不要被多次点击响应。
        //reloadButton.interactable = false;
        //exitButton.enabled = false;
        //continueButton.enabled = false;
        SceneManager.LoadScene("Loading");
    }

    private void ContinueGame()
    {
        //exitButton.enabled = false;
        //reloadButton.enabled = false;
        //continueButton.interactable = false;
        Time.timeScale = 1;
        reloadButton.GetComponent<Image>().DOFade(0, 0.5f);
        continueButton.GetComponent<Image>().DOFade(0, 0.5f);
        exitButton.GetComponent<Image>().DOFade(0, 0.5f);
        transform.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(() =>
        {
            //boxCollider.SetActive(false);
            gameObject.SetActive(false);

            //应该将该消息发送到UI控制脚本做统一控制。
            bgImg.boxCollider.SetActive(false);
        });

    }

    private void ExitGame()
    {
        //exitButton.interactable = false;
        Application.Quit();
    }
}
