using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCharginState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       SoundManager.PlaySound(SoundType.Player, (int)PlayerSoundType.ChargingBubble);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.StopSound(SoundType.Player, (int)PlayerSoundType.ChargingBubble);
    }
}
