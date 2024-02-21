// -------------------------------------------
// Nick Perrin 2024 - https://github.com/NPTP/
// -------------------------------------------
using UnityEngine;

namespace NPTP.PlayerLoopUtilities
{
    /// <summary>
    /// Instantiate this class somewhere in your code to see things working and the log messages appear in your console.
    /// </summary>
    public class Example
    {
        private float updateTimeElapsed;
        private float fixedUpdateTimeElapsed;
        
        public Example()
        {
            // Subscribe wherever you like, in Awake, Start, constructors, on certain game conditions, etc.
            PlayerLoopUtility.OnPlayerLoopUpdate += PlayerLoopUpdate;
            PlayerLoopUtility.OnPlayerLoopFixedUpdate += PlayerLoopFixedUpdate;
        }

        private void PlayerLoopUpdate()
        {
            // Update logic goes here that will execute every frame, even in this non-MonoBehaviour class.
            // (You can use it in MonoBehaviours as well, if you really want to)
            updateTimeElapsed += Time.deltaTime;
            Debug.Log($"Update time elapsed: {updateTimeElapsed}");
        }

        private void PlayerLoopFixedUpdate()
        {
            // FixedUpdate logic goes here that will execute every frame, even in this non-MonoBehaviour class.
            // (You can use it in MonoBehaviours as well, if you really want to)
            fixedUpdateTimeElapsed += Time.fixedDeltaTime;
            Debug.Log($"FixedUpdate time elapsed: {fixedUpdateTimeElapsed}");
        }

        public void Terminate()
        {
            // It's highly recommended to tear down any usage when you're done.
            PlayerLoopUtility.OnPlayerLoopUpdate -= PlayerLoopUpdate;
            PlayerLoopUtility.OnPlayerLoopFixedUpdate -= PlayerLoopFixedUpdate;

            // NOTE: In the editor, the Player Loop Utility will automatically stop any internal delegates attached to
            // the player loop before exiting play mode as a safety precaution.
            // This is done to prevent a side effect where some PlayerLoop functionality can continue into edit mode.
        }
    }
}
