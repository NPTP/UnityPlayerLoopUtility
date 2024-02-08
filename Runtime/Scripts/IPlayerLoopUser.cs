namespace NPTP.PlayerLoopUtility
{
    /// <summary>
    /// Implement any of the below interfaces that implement IPlayerLoopUser to hook into PlayerLoop.
    /// Call the subscribe method to hook in, unsubscribe to unhook.
    /// Fill out the virtual methods as desired.
    /// Example in a non-monobehaviour class:
    /// 
    /// public class TestClass
    /// {
    ///     public TestClass()
    ///     {
    ///         SubscribeToPlayerLoopUpdate();
    ///     }
    ///
    ///     public void PlayerLoopUpdate()
    ///     {
    ///         // Update logic goes here that will execute every frame, even in a non-MonoBehaviour class.
    ///     }
    ///
    ///     public void Terminate()
    ///     {
    ///         // NOTE: It's highly recommended to tear down any usage by unsubscribing before
    ///         // exiting play mode, as some PlayerLoop functionality may continue in edit mode.
    ///         UnsubscribeFromPlayerLoopUpdate();
    ///     }
    /// }
    /// 
    /// </summary>
    public interface IPlayerLoopUser { }
    
    /// <summary>
    /// Interface for hooking into an updated before Unity's regular Update call.
    /// </summary>
    public interface IPlayerLoopEarlyUpdater : IPlayerLoopUser
    {
        protected void SubscribeToPlayerLoopEarlyUpdate() => PlayerLoopSubscriber.Subscribe(this);
        protected void UnsubscribeFromPlayerLoopEarlyUpdate() => PlayerLoopSubscriber.Unsubscribe(this);
        void PlayerLoopEarlyUpdate();
    }
    
    /// <summary>
    /// Interface for hooking into Unity's regular Update call.
    /// </summary>
    public interface IPlayerLoopUpdater : IPlayerLoopUser
    {
        protected void SubscribeToPlayerLoopUpdate() => PlayerLoopSubscriber.Subscribe(this);
        protected void UnsubscribeFromPlayerLoopUpdate() => PlayerLoopSubscriber.Unsubscribe(this);
        void PlayerLoopUpdate();
    }
    
    /// <summary>
    /// Interface for hooking into Unity's physics FixedUpdate call.
    /// </summary>
    public interface IPlayerLoopFixedUpdater : IPlayerLoopUser
    {
        protected void SubscribeToPlayerLoopFixedUpdate() => PlayerLoopSubscriber.Subscribe(this);
        protected void UnsubscribeFromPlayerLoopFixedUpdate() => PlayerLoopSubscriber.Unsubscribe(this);
        void PlayerLoopFixedUpdate();
    }
    
    /// <summary>
    /// Interface for hooking into a less-than-late update before LateUpdate.
    /// </summary>
    public interface IPlayerLoopPreLateUpdater : IPlayerLoopUser
    {
        protected void SubscribeToPlayerLoopPreLateUpdate() => PlayerLoopSubscriber.Subscribe(this);
        protected void UnsubscribeFromPlayerLoopPreLateUpdate() => PlayerLoopSubscriber.Unsubscribe(this);
        void PlayerLoopPreLateUpdate();
    }
    
    /// <summary>
    /// Interface for hooking into an update after LateUpdate.
    /// </summary>
    public interface IPlayerLoopPostLateUpdater : IPlayerLoopUser
    {
        protected void SubscribeToPlayerLoopPostLateUpdate() => PlayerLoopSubscriber.Subscribe(this);
        protected void UnsubscribeFromPlayerLoopPostLateUpdate() => PlayerLoopSubscriber.Unsubscribe(this);
        void PlayerLoopPostLateUpdate();
    }
}