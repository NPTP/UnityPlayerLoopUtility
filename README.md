# UnityPlayerLoopUtility
Small easy-to-use utility for hooking delegates into Unity PlayerLoop updates.

Import this into your project with the Unity Package Manager using the repo's git url: https://github.com/NPTP/UnityPlayerLoopUtility.git

Implement any of the interfaces that implement IPlayerLoopUser to hook into PlayerLoop.
Call the subscribe method to hook in, unsubscribe to unhook.
Fill out the virtual methods as desired.
Note this can be used in MonoBehaviours as well, if desired.

See the Example.cs script for usage:

```
using UnityEngine;

namespace NPTP.PlayerLoopUtility
{
    public class Example : IPlayerLoopUpdater, IPlayerLoopFixedUpdater
    {
        private float updateTimeElapsed;
        private float fixedUpdateTimeElapsed;
        
        public Example()
        {
            // Subscribe wherever you like, in Awake, Start, constructors, on certain game conditions, etc.
            // The subscriber will subscribe to all types of updates that you have interfaces implemented for.
            // In this case, we implement both IPlayerLoopUpdater and IPlayerLoopFixedUpdater, so we'll get
            // delegates hooked in for both with this one subscribe call.
            PlayerLoopSubscriber.Subscribe(this);
        }

        public void PlayerLoopUpdate()
        {
            // Update logic goes here that will execute every frame, even in this non-MonoBehaviour class.
            // (You can use it in MonoBehaviours as well, if you really want to)
            updateTimeElapsed += Time.deltaTime;
        }

        public void PlayerLoopFixedUpdate()
        {
            // FixedUpdate logic goes here that will execute every frame, even in this non-MonoBehaviour class.
            // (You can use it in MonoBehaviours as well, if you really want to)
            fixedUpdateTimeElapsed += Time.fixedDeltaTime;
        }

        public void Terminate()
        {
            // NOTE: It's highly recommended to tear down any usage by unsubscribing before
            // exiting play mode, as some PlayerLoop functionality may continue in edit mode.
            PlayerLoopSubscriber.Unsubscribe(this);
        }
    }
}
```

This package is under the MIT License.