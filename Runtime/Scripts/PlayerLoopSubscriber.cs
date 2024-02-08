// -------------------------------------------
// Nick Perrin 2024 - https://github.com/NPTP/
// -------------------------------------------
using System;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace NPTP.PlayerLoopUtility
{
    public static class PlayerLoopSubscriber
    {
        /// <summary>
        /// Subscribe the PlayerLoopUser to the desired part of the PlayerLoop. Double-subscriptions will be silently prevented.
        /// </summary>
        public static void Subscribe(IPlayerLoopUser subscriber) => ChangeSubscription(subscriber, Subscription.Add);
        
        /// <summary>
        /// Unsubscribe the PlayerLoopUser from the PlayerLoop.
        /// </summary>
        public static void Unsubscribe(IPlayerLoopUser subscriber) => ChangeSubscription(subscriber, Subscription.Remove);

        private static void ChangeSubscription(IPlayerLoopUser subscriber, Subscription subscription)
        {
            PlayerLoopSystem playerLoop = PlayerLoop.GetCurrentPlayerLoop();

            if (subscriber is IPlayerLoopEarlyUpdater earlyUpdater)
            {
                int subsystemIndex = GetIndexOfSubsystem(playerLoop, typeof(EarlyUpdate));
                playerLoop.subSystemList[subsystemIndex].updateDelegate -= earlyUpdater.PlayerLoopEarlyUpdate;
                if (subscription is Subscription.Add)
                    playerLoop.subSystemList[subsystemIndex].updateDelegate += earlyUpdater.PlayerLoopEarlyUpdate;
            }
            
            if (subscriber is IPlayerLoopUpdater updater)
            {
                int subsystemIndex = GetIndexOfSubsystem(playerLoop, typeof(Update));
                playerLoop.subSystemList[subsystemIndex].updateDelegate -= updater.PlayerLoopUpdate;
                if (subscription is Subscription.Add)
                    playerLoop.subSystemList[subsystemIndex].updateDelegate += updater.PlayerLoopUpdate;
            }
            
            if (subscriber is IPlayerLoopPreLateUpdater preLateUpdater)
            {
                int subsystemIndex = GetIndexOfSubsystem(playerLoop, typeof(PreLateUpdate));
                playerLoop.subSystemList[subsystemIndex].updateDelegate -= preLateUpdater.PlayerLoopPreLateUpdate;
                if (subscription is Subscription.Add)
                    playerLoop.subSystemList[subsystemIndex].updateDelegate += preLateUpdater.PlayerLoopPreLateUpdate;
            }
            
            if (subscriber is IPlayerLoopPostLateUpdater postLateUpdater)
            {
                int subsystemIndex = GetIndexOfSubsystem(playerLoop, typeof(PostLateUpdate));
                playerLoop.subSystemList[subsystemIndex].updateDelegate -= postLateUpdater.PlayerLoopPostLateUpdate;
                if (subscription is Subscription.Add)
                    playerLoop.subSystemList[subsystemIndex].updateDelegate += postLateUpdater.PlayerLoopPostLateUpdate;
            }
            
            if (subscriber is IPlayerLoopFixedUpdater fixedUpdater)
            {
                int subsystemIndex = GetIndexOfSubsystem(playerLoop, typeof(FixedUpdate));
                playerLoop.subSystemList[subsystemIndex].updateDelegate -= fixedUpdater.PlayerLoopFixedUpdate;
                if (subscription is Subscription.Add)
                    playerLoop.subSystemList[subsystemIndex].updateDelegate += fixedUpdater.PlayerLoopFixedUpdate;
            }

            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        private static int GetIndexOfSubsystem(PlayerLoopSystem playerLoop, Type subsystemType)
        {
            int i;
            for (i = 0; i < playerLoop.subSystemList.Length; i++)
            {
                PlayerLoopSystem playerLoopSystem = playerLoop.subSystemList[i];
                if (playerLoopSystem.type == subsystemType)
                {
                    break;
                }
            }

            return i;
        }
    }
}