// -------------------------------------------
// Nick Perrin 2024 - https://github.com/NPTP/
// -------------------------------------------
using System;
using UpdateFunction = UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction;

namespace Utilities.PlayerLoop
{
    public struct PlayerLoopSubscriber
    {
        public PlayerLoopSubscriber(UpdateFunction updateFunction, Type updateType)
        {
            UpdateFunction = updateFunction;
            UpdateType = updateType;
        }
        
        public UpdateFunction UpdateFunction { get; }
        public Type UpdateType { get; }
    }
}