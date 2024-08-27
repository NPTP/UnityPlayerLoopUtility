// -------------------------------------------
// Nick Perrin 2024 - https://github.com/NPTP/
// -------------------------------------------
using System;
using System.Reflection;
using UnityEngine;

namespace NPTP.PlayerLoopUtility
{
    /// <summary>
    /// Insert static methods into the player loop.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PlayerLoopUpdateAttribute : Attribute
    {
        private readonly UpdateType updateType;
        
        public PlayerLoopUpdateAttribute(UpdateType updateType)
        {
            this.updateType = updateType;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitializeAttributes()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    MethodInfo[] methodInfos = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach (MethodInfo methodInfo in methodInfos)
                    {
                        PlayerLoopUpdateAttribute attribute = methodInfo.GetCustomAttribute<PlayerLoopUpdateAttribute>();
                        if (attribute == null)
                        {
                            continue;
                        }

                        string eventName = attribute.updateType switch
                        {
                            UpdateType.EarlyUpdate => nameof(PlayerLoopUtility.OnPlayerLoopEarlyUpdate),
                            UpdateType.Update => nameof(PlayerLoopUtility.OnPlayerLoopUpdate),
                            UpdateType.PreLateUpdate => nameof(PlayerLoopUtility.OnPlayerLoopPreLateUpdate),
                            UpdateType.PostLateUpdate => nameof(PlayerLoopUtility.OnPlayerLoopPostLateUpdate),
                            UpdateType.FixedUpdate => nameof(PlayerLoopUtility.OnPlayerLoopFixedUpdate),
                            _ => throw new ArgumentOutOfRangeException()
                        };

                        EventInfo eventInfo = typeof(PlayerLoopUtility).GetEvent(eventName);
                        Delegate eventHandler = Delegate.CreateDelegate(eventInfo.EventHandlerType, methodInfo);
                        eventInfo.AddEventHandler(null, eventHandler);
                    }
                }
            }
        }
    }
}
