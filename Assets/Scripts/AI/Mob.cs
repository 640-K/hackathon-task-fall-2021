using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using UnityEngine.Rendering;

namespace Entities{
    public class Mob : Entity
    {

        private Renderer _renderer;

        public override void Start()
        {
            base.Start();
            _renderer = avatarAnimator.gameObject.GetComponent<Renderer>();
            events.onDie.AddListener(Die);


        }


        void Die(uint health)
        {
            avatarAnimator.enabled = false;
            StartCoroutine(Stone());
        }

        IEnumerator Stone()
        {
            _renderer.material.SetFloat("_Range", _renderer.material.GetFloat("_Range") +0.01f);
            yield return new WaitForEndOfFrame();
            if (_renderer.material.GetFloat("_Range") < 1)
                StartCoroutine(Stone());
        }
    }
}