using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnPlayerenterLevelState : StateMachineBehaviour
{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.PlaySound(SoundType.Player, (int)PlayerSoundType.SaveBubblo, 0.7f);
    }
}
