using System;
using INeverFall.Monster;
using UnityEngine;

namespace INeverFall
{
    public static class Extensions
    {
        public static void ResetLocal(this Transform origin)
        {
            origin.localPosition = Vector3.zero; 
            origin.localRotation = Quaternion.identity; 
            origin.localScale = Vector3.one;
        }

        public static T DemandComponent<T>(this GameObject origin) where T : Component
        {
            if (!origin.TryGetComponent<T>(out var component))
            {
                component = origin.AddComponent<T>();
            }
            return component;
        }

        public static Transform FindChildRecursively(this Transform origin, string name)
        {
            if (origin.name.Equals(name))
                return origin;

            foreach (Transform child in origin)
            {
                Transform result = child.FindChildRecursively(name);
                if (result != null)
                    return result;
            }

            return null;
        }
        
        public static bool IsSpecificAnimationPlaying(this Animator animator, string animationName, int layerIndex = 0)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
            return stateInfo.IsName(animationName);
        }

        public static bool IsSpecificAnimationPlaying(this Animator animator, BossAnimation animationType, int layerIndex = 0)
        {
            string animationName = Utils.GetBossAttackAnimationName(animationType);
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
            return stateInfo.IsName(animationName);
        }

        public static AnimationClip GetAnimationClipByName(this Animator animator, string clipName)
        {
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;

            foreach (var clip in ac.animationClips)
            {
                if (string.Equals(clip.name, clipName))
                {
                    //Debug.Log($"I got [{clipName}] !!");
                    return clip;
                }
            }
            return null;
        }

        public static float GetCurrentAnimationLength(this Animator animator, int layerIndex = 0)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
            return stateInfo.length;
        }

        public static void PlayAttackAnimation(this Animator animator, BossAnimation animation)
        {
            animator.SetTrigger(Monster.AnimationID.AttackTrigger);
            animator.SetInteger(Monster.AnimationID.Attack, (int)animation);
        }

        public static void AddAnimationEvent(this AnimationClip clip, string functionName, float time, int intParam = 0)
        {
            var animationEvent = new AnimationEvent
            {
                functionName = functionName,
                time = time,
                intParameter = intParam,
            };
            clip.AddEvent(animationEvent);
        }
    }
}