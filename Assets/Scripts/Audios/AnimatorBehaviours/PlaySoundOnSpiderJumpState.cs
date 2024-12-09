using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnSpiderJumpState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       SoundManager.PlaySound(SoundType.Enemy, (int)EnemySoundType.AttackJump);
    }
}
