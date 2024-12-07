using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnPitchingState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       SoundManager.PlaySound(SoundType.Player, (int)PlayerSoundType.ThrowBubble);
    }
}
