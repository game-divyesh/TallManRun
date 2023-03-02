using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UISystem
{
    [RequireComponent(typeof(Text))]
    public class TextFade : Animatable
    {
        public float fromAlpha;
        public float toAlpha;
        public float finalAlpha;
        Text text;
        public override void Awake()
        {
            base.Awake();
            text = GetComponent<Text>();
        }
        public override void OnAnimationStarted()
        {
            base.OnAnimationStarted();
            text.color = text.color.WithAlpha(fromAlpha);
        }
        public override void OnAnimationRunning(float animPerc)
        {
            base.OnAnimationRunning(animPerc);
            text.color = Color.LerpUnclamped(text.color.WithAlpha(fromAlpha), text.color.WithAlpha(toAlpha), animPerc);
        }
        public override void OnAnimationEnded()
        {
            base.OnAnimationEnded();
            text.color = text.color.WithAlpha(finalAlpha);
        }
    }
}

