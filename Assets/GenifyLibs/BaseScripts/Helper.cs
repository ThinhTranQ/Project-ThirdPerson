using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseScripts
{
    public static class Helper
    {
        private static Camera camera;

        public static Camera Camera
        {
            get
            {
                if (camera == null)
                {
                    camera = Camera.main;
                }

                return camera;
            }
        }

        private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
        
        public static Coroutine DelaySec(this MonoBehaviour monoBehavior, Action functionWantDelay,
            float? timeDelay = null)
        {
            return monoBehavior.StartCoroutine(IEWait(functionWantDelay, timeDelay));
        }
        
        private static IEnumerator IEWait(Action functionWantDelay, float? timeDelay)
        {
            if (timeDelay.HasValue)
                yield return GetWait(timeDelay.Value);
            else
                yield return null;
            functionWantDelay?.Invoke();
        }
        
        public static WaitForSeconds GetWait(float time)
        {
            if (WaitDictionary.TryGetValue(time,out var wait))
            {
                return wait;
            }

            WaitDictionary[time] = new WaitForSeconds(time);
            return WaitDictionary[time]; 
        }
    }
}