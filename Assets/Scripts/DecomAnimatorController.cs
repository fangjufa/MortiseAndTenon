using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecomAnimatorController : MonoBehaviour {

    private int i = 1;

    private static Animator animator;
    private static Animator _animator;

    private static GameObject thisObject;

    private string one;
    private static string two;

    private int count;

    private float time;

    private static bool isDisintegration;
    private bool isCanDo;

    // Use this for initialization
    void Start () {
        isCanDo = true;
        //isTurnBack = false;
        isDisintegration = false;
    }

    private void Update()
    {
        if (this.GetComponent<ComponentAttributes>())
        {
            if (DecomController.ready == this.GetComponent<ComponentAttributes>().nambersOfComponent)
            {
                GameObject.Find("Canvas").GetComponent<BGImg>().Reload();
                DecomController.ready = 0;
            }
        }
    }

    public void PlayOver()
    {
        //if (!this.GetComponent<ModularFurniture>())
        //{
            isDisintegration = true;
            isCanDo = true;

            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<Animator>().enabled = false;

            foreach (Transform child in this.transform)
            {
                if (child.GetComponent<MeshCollider>())
                    child.GetComponent<MeshCollider>().enabled = true;
            }
        //}
        //else
        //{
        //    this.GetComponent<ModularFurniture>().Replace();
        //    //this.gameObject.AddComponent<ComponentAttributes>();
        //    Destroy(this.GetComponent<DecomAnimatorController>());
        //}
    }

    public void TurnBack()
    {
        isCanDo = true;
        DecomController.ready = 0;
        GameObject.Find("Canvas").GetComponent<BGImg>().Reload();
    }

    public void PlayStart()
    {
        print(i);
        gameObject.AddComponent<AudioSource>();
        gameObject.GetComponent<AudioSource>().loop = false;
        if (i == 1)
        {
            gameObject.GetComponent<AudioSource>().clip = gameObject.GetComponent<AudioClipS>().split;
            i += 1;
        }
        else if (i == 2)
        {
            gameObject.GetComponent<AudioSource>().clip = gameObject.GetComponent<AudioClipS>().merge;
            i -= 1;
        }
        gameObject.GetComponent<AudioSource>().Play();

        isCanDo = false;
        DecomController.ready = 0;
        foreach (Transform child in this.transform)
        {
            if (child.GetComponent<MeshCollider>())
                child.GetComponent<MeshCollider>().enabled = false;
        }
    }

    public static void Return()
    {
        if (animator && isDisintegration)
        {
            thisObject.GetComponent<Animator>().enabled = true;
            thisObject.GetComponent<BoxCollider>().enabled = true;
            isDisintegration = false;
            animator.Play(two);
        }
    }

    void OnMouseDown() {
        one = BGImg.transitName;
        two = BGImg.transitName + " 0";
        if (BGImg.transitName != null)
        {
            animator = GetComponent<Animator>();
            _animator = animator;
        }
        if (animator)
        {
            if (Input.GetMouseButtonDown(0) && isCanDo == true)
            {
                thisObject = gameObject;
                //gameObject.GetComponent<Animator>().enabled = true;
                count++;
                //当第一次点击鼠标，启动计时器  
                if (count == 1)
                {
                    time = Time.time;                 
                }
                //当第二次点击鼠标，且时间间隔满足要求时双击鼠标  
                if (2 == count && Time.time - time <= 0.5f && !isDisintegration)
                {
                    animator.Play(one);
                    isDisintegration = true;
                    count = 0;
                }
            }
            if (Time.time - time > 0.5f)
            {
                count = 0;
            }
        }

    }

    public void PlayAnimation()
    {
        animator = this.GetComponent<Animator>();
        animator.Play(this.gameObject.name);
    }
}
