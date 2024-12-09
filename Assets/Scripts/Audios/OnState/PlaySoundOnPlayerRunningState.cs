using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnPlayerRunningState : StateMachineBehaviour
{
    private int previousLoopCount = -1;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        previousLoopCount = -1;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int currentLoopCount = Mathf.FloorToInt(stateInfo.normalizedTime);

        // Check if a new loop has started
        if (currentLoopCount > previousLoopCount)
        {
            previousLoopCount = currentLoopCount; // Update loop tracker
            SoundManager.PlaySound(SoundType.Player, (int)PlayerSoundType.WalkOnSoil, 0.5f);
        }
    }
}
