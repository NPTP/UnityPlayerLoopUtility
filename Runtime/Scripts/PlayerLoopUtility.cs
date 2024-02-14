// -------------------------------------------
// Nick Perrin 2024 - https://github.com/NPTP/
// -------------------------------------------
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;
using Utilities.PlayerLoop;
using UpdateFunction = UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction;

namespace NPTP.PlayerLoopUtilities
{
    public static class PlayerLoopUtility
    {
        private static readonly HashSet<PlayerLoopSubscriber> playerLoopSubscribersSet = new();

        public static event UpdateFunction OnPlayerLoopEarlyUpdate
        {
            add => ChangeSubscription(value, typeof(EarlyUpdate), Subscription.Add);
            remove => ChangeSubscription(value, typeof(EarlyUpdate), Subscription.Remove);
        }

        public static event UpdateFunction OnPlayerLoopUpdate
        {
            add => ChangeSubscription(value, typeof(Update), Subscription.Add);
            remove => ChangeSubscription(value, typeof(Update), Subscription.Remove);
        }

        public static event UpdateFunction OnPlayerLoopPreLateUpdate
        {
            add => ChangeSubscription(value, typeof(PreLateUpdate), Subscription.Add);
            remove => ChangeSubscription(value, typeof(PreLateUpdate), Subscription.Remove);
        }

        public static event UpdateFunction OnPlayerLoopPostLateUpdate
        {
            add => ChangeSubscription(value, typeof(PostLateUpdate), Subscription.Add);
            remove => ChangeSubscription(value, typeof(PostLateUpdate), Subscription.Remove);
        }

        public static event UpdateFunction OnPlayerLoopFixedUpdate
        {
            add => ChangeSubscription(value, typeof(FixedUpdate), Subscription.Add);
            remove => ChangeSubscription(value, typeof(FixedUpdate), Subscription.Remove);
        }
        
        /// <summary>
        /// Detaches ALL custom delegates attached to the PlayerLoop system.
        /// </summary>
        public static void UnsubscribeAll()
        {
            foreach (PlayerLoopSubscriber playerLoopSubscriber in playerLoopSubscribersSet)
            {
                ChangeSubscription(playerLoopSubscriber.UpdateFunction, playerLoopSubscriber.UpdateType, Subscription.Remove);
            }
        }
 
        private static void ChangeSubscription(UpdateFunction updateFunction, Type updateType, Subscription subscription)
        {
            PlayerLoopSystem playerLoop = UnityEngine.LowLevel.PlayerLoop.GetCurrentPlayerLoop();

            int subsystemIndex = GetIndexOfSubsystem(playerLoop, updateType);
            playerLoop.subSystemList[subsystemIndex].updateDelegate -= updateFunction; // Prevent multi-subscriptions
            if (subscription is Subscription.Add)
                playerLoop.subSystemList[subsystemIndex].updateDelegate += updateFunction;
            
            PlayerLoopSubscriber playerLoopSubscriber = new(updateFunction, updateType);
            if (subscription is Subscription.Remove)
                playerLoopSubscribersSet.Remove(playerLoopSubscriber);
            else if (subscription is Subscription.Add)
                playerLoopSubscribersSet.Add(playerLoopSubscriber);
            
            UnityEngine.LowLevel.PlayerLoop.SetPlayerLoop(playerLoop);
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
        
#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void PlayModeStateNotifier()
        {
            EditorApplication.playModeStateChanged -= HandlePlayModeStateChanged;
            EditorApplication.playModeStateChanged += HandlePlayModeStateChanged;
        }

        private static void HandlePlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            if (playModeStateChange is PlayModeStateChange.ExitingPlayMode)
            {
                EditorApplication.playModeStateChanged -= HandlePlayModeStateChanged;
                UnsubscribeAll();
            }
        }
#endif
    }
}