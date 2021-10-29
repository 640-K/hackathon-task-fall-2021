using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatorController : MonoBehaviour
{
    public Animator animator;

    private AnimationClipData currentClipData;



    private void Update()
    {
        
    }

    //public bool TransitionTo(string state)
    //{
    //    animator.Play(state);

    //    animator.GetAnim
    //}


    public class AnimationClipData
    {
        public string name;
        public bool canBeInterrupted;
        public bool fixedDuration;
        public float duration;

        public float transitionTime;
    }
}
