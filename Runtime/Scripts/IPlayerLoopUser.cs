// -------------------------------------------
// Nick Perrin 2024 - https://github.com/NPTP/
// -------------------------------------------
namespace NPTP.PlayerLoopUtility
{
    /// <summary>
    /// Implementing this base interface has no effect. Use the inheritors below.
    /// </summary>
    public interface IPlayerLoopUser { }
    
    /// <summary>
    /// Interface for hooking into an updated before Unity's regular Update call.
    /// </summary>
    public interface IPlayerLoopEarlyUpdater : IPlayerLoopUser
    {
        void PlayerLoopEarlyUpdate();
    }
    
    /// <summary>
    /// Interface for hooking into Unity's regular Update call.
    /// </summary>
    public interface IPlayerLoopUpdater : IPlayerLoopUser
    {
        void PlayerLoopUpdate();
    }
    
    /// <summary>
    /// Interface for hooking into Unity's physics FixedUpdate call.
    /// </summary>
    public interface IPlayerLoopFixedUpdater : IPlayerLoopUser
    {
        void PlayerLoopFixedUpdate();
    }
    
    /// <summary>
    /// Interface for hooking into a less-than-late update before LateUpdate.
    /// </summary>
    public interface IPlayerLoopPreLateUpdater : IPlayerLoopUser
    {
        void PlayerLoopPreLateUpdate();
    }
    
    /// <summary>
    /// Interface for hooking into an update after LateUpdate.
    /// </summary>
    public interface IPlayerLoopPostLateUpdater : IPlayerLoopUser
    {
        void PlayerLoopPostLateUpdate();
    }
}