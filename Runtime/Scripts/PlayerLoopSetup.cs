using System;
using System.Collections.Generic;

namespace NPTP.PlayerLoopUtilities
{
    public abstract class PlayerLoopSetup
    {
        public event Action OnUpdate;
        public HashSet<Action> SubscribedDelegates { get; } = new HashSet<Action>();
        public void UpdateFunction() => OnUpdate?.Invoke();
        
        public abstract Type UpdateType { get; }
    }
    
    public class PlayerLoopSetup<T> : PlayerLoopSetup
    {
        public override Type UpdateType { get; } = typeof(T);
    }
}