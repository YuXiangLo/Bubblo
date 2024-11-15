using UnityEngine;

namespace Enemies.JumpSpider
{
    /// <summary>
    /// Interface for the JumpSpider states
    /// </summary>
    public interface IJumpSpiderState
    {
        /// <summary>
        /// Invoked when entering the state
        /// </summary>
        public void Enter();

        /// <summary>
        /// Invoked when updating the state
        /// </summary>
        public void Update();

        /// <summary>
        /// Invoked when exiting the state
        /// </summary>
        public void Exit(); 
    }
}