using UnityEngine;
using System.Collections;

public class ChairController : MonoBehaviour
{
    public string BaoZha;
    public string HeBing;
    private static string heBing;
    public Animator _anim;
    private static Animator a_anim;
    private bool IsBaoZha = false;
    private static bool isBaoZha;

    //计时器，在一定的时间内双击有效  
    private float time = 0f;
    //计数器  
    private int count = 0;
    // Use this for initialization
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        a_anim = _anim;
        BaoZha = ChairModel.Instance.Baozha;
        HeBing = ChairModel.Instance.Baozha + " 0";
        heBing = HeBing;

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.AddComponent<ChairAnimatorSupport>();
        }

    }

    private void Update()
    {
        isBaoZha = IsBaoZha;
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        ChairAnimatorSupport cas = gameObject.GetComponentInChildren<ChairAnimatorSupport>();
        if (Input.GetMouseButtonDown(0) && cas.IsCanDo)
        {
            count++;
            //当第一次点击鼠标，启动计时器  
            if (count == 1)
            {
                time = Time.time;
            }
            //当第二次点击鼠标，且时间间隔满足要求时双击鼠标  
            if (2 == count && Time.time - time <= 0.5f)
            {
                if (!IsBaoZha)
                {
                    _anim.Play(BaoZha);
                    IsBaoZha = true;
                }
                else
                {
                    _anim.Play(HeBing);
                    IsBaoZha = false;
                }
                count = 0;
            }
        }
        if (Time.time - time > 0.5f)
        {
            count = 0;
        }
    }

    public static void PlayHebing()
    {
        if(isBaoZha == true)
        a_anim.Play(heBing);
    }
}
