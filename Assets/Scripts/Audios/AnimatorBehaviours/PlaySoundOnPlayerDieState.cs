using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnPlayerDieState : StateMachineBehaviour
{
    private static readonly float DeadSoundEffectAnimationCount = 0.7f;
    private bool HasPlayDeadSoundEffect;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HasPlayDeadSoundEffect = false;
        SoundManager.PlaySound(SoundType.Player, (int)PlayerSoundType.Attacked);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!HasPlayDeadSoundEffect && stateInfo.normalizedTime > DeadSoundEffectAnimationCount)
        {
            HasPlayDeadSoundEffect = true;
            SoundManager.PlaySound(SoundType.Player, (int)PlayerSoundType.Dead, 0.7f);
        }
    }
}
