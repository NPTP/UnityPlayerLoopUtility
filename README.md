# UnityPlayerLoopUtility
Small easy-to-use utility for hooking delegates into Unity PlayerLoop updates.

Import this into your project with the Unity Package Manager using the repo's git url: https://github.com/NPTP/UnityPlayerLoopUtility.git

Subscribe to the static events of PlayerLoopUtility to use the desired update type from any class: static, non-Unity object, MonoBehaviour etc.

See the Example.cs script for usage:

```
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
            
            // NOTE: In the editor, the Player Loop Utility will automatically unsubscribe all delegates before exiting
            // play mode as a safety precaution, since some PlayerLoop functionality can continue into edit mode.
            // See the section of the PlayerLoopUtility inside editor tags for details.
            
            // If you want to stop all PlayerLoop custom updates across your whole project, you can also call:
            PlayerLoopUtility.UnsubscribeAll();
        }
    }
}
```

This package is under the MIT License.