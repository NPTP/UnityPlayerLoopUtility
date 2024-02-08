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
