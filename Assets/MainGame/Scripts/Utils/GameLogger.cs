using UnityEngine;

namespace MainGame.Utils
{
    public static class GameLogger
    {
        public static void Log(object message)
        {
            Debug.Log($"<color=blue><b><size=14>GameLog:</size></b></color> {message}");
        }

        public static void LogError(object message)
        {
            Debug.LogError($"<color=red><b><size=14>GameError:</size></b></color> {message}");
        }
        
        public static void LogWarning(object message)
        {
            Debug.LogWarning($"<color=yellow><b><size=14>GameWarning:</size></b></color> {message}");
        }
        
        public static void Print(string msg,Object obj)
        {
            Debug.Log($"<color=red><b><size=14>RedFlag:</size></b></color> {msg}",obj);
        }
    }
}