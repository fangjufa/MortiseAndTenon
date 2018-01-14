using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickAnim : MonoBehaviour {

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

        //if (!this.GetComponentInParent<ModularComponentController>() || !this.GetComponentInParent<ModularFurniture>() && this.GetComponent<MeshCollider>())
        //{
        //    this.GetComponent<MeshCollider>().enabled = false;
        //}    
    }

    private void PlayStart()
    {
        canPlay = false;
    }

    private void PlayOver()
    {
        Destroy(gameObject.GetComponent<AudioSource>());
        if (animator)
        {
            //this.GetComponent<LittleComponentAttributes>().level = LittleComponentAttributes.Level.ZERO; 

            //if (!this.GetComponentInParent<ModularComponentController>())
            //{
                this.GetComponentInParent<ComponentAttributes>().UpdateList();
                DecomController.ready += 1;
                
            //}
            //else
            //{
            //    this.GetComponentInParent<ModularComponentController>().UpdateList();
            //    this.GetComponentInParent<ModularComponentController>().readyNumbers += 1;
            //}

            //if (this.GetComponentInParent<ModularFurniture>() && this.GetComponentInParent<ModularFurniture>().readModular)
            //{
            //    this.GetComponentInParent<ModularFurniture>().readyModular += 1;
            //}
            Destroy(this.GetComponent<MeshCollider>());
            animator.enabled = false;
        }
    }

    public void OnMouseDown()
    {
        if (animator)
        {
            if (canPlay && isReady)
            {
                gameObject.AddComponent<AudioSource>();
                gameObject.GetComponent<AudioSource>().loop = false;
                gameObject.GetComponent<AudioSource>().clip = gameObject.GetComponent<AudioClipS>().合并;
                gameObject.GetComponent<AudioSource>().Play();
                animator.enabled = true;
                animator.Play(gameObject.name);
                canPlay = false;
                this.GetComponent<MeshCollider>().enabled = false;
            }
        }
    }
}
