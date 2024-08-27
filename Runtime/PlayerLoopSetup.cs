// -------------------------------------------
// Nick Perrin 2024 - https://github.com/NPTP/
// -------------------------------------------
using System;
using System.Collections.Generic;

namespace NPTP.PlayerLoopUtility
{
    internal abstract class PlayerLoopSetup
    {
        public event Action OnUpdate;
        public HashSet<Action> SubscribedDelegates { get; } = new();
        public bool HasSubscribers => SubscribedDelegates.Count > 0;
        public void UpdateFunction() => OnUpdate?.Invoke();
        
        public abstract Type UpdateType { get; }
    }
    
    internal class PlayerLoopSetup<T> : PlayerLoopSetup
    {
        public override Type UpdateType { get; } = typeof(T);
    }
}