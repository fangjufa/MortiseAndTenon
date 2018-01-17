using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂在爆炸的小部件上的脚本，接收鼠标的点击事件并播放动画等。
/// </summary>
public class MouseClickAnim : MonoBehaviour
{

    public Animator animator;

    public int readyPosition;

    public bool isReady;

    private bool canPlay;

    // Use this for initialization
    void Start () {

        if (this.GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
            animator.enabled = false;
            canPlay = true;
        }  
    }

    //在开始播放合并动画时执行的函数。该函数是直接绑定在动画片段(animation clip)上的。
    private void PlayStart()
    {
        //在播放动画时，不能够再次点击。要等到播放完之后才能被点击。
        canPlay = false;
    }

    //在播放合并动画之后执行的函数。该函数是直接绑定在动画片段(animation clip)上的。
    private void PlayOver()
    {
        Destroy(gameObject.GetComponent<AudioSource>());
        if (animator)
        {

            this.GetComponentInParent<ComponentAttributes>().UpdateList();
            DecomController.ready += 1;
                
            Destroy(this.GetComponent<MeshCollider>());
            animator.enabled = false;
        }
    }

    /// <summary>
    /// 鼠标点击之后，播放合并动画,并播放声音
    /// </summary>
    public void OnMouseDown()
    {
        if (animator)
        {
            if (canPlay && isReady)
            {
                //播放声音
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.loop = false;
                audioSource.clip = gameObject.GetComponent<AudioClipS>().merge;
                audioSource.Play();

                //播放动画
                animator.enabled = true;
                animator.Play(gameObject.name);
                canPlay = false;

                this.GetComponent<MeshCollider>().enabled = false;
            }
        }
    }
}
