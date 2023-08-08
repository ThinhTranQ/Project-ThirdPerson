using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MainGame.Utils
{
    public static class ToolUtils
    {
        private static readonly DateTime unixTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long GetCurrentUnixTimestampSeconds()
        {
            return (long)(DateTime.UtcNow - unixTime).TotalSeconds;
        }

        /// <summary>
        /// Check network and return true if network enable
        /// </summary>
        /// <returns></returns>
        public static bool IsInternetAvailable()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Delay Any void function
        /// </summary>
        /// <param name="monoBehavior"> script where want delay</param>
        /// <param name="functionWantDelay">function want delay</param>
        /// <param name="timeDelay"></param>
        /// <returns></returns>
        public static Coroutine DelayFunction(this MonoBehaviour monoBehavior, Action functionWantDelay,
            float? timeDelay = null)
        {
            return monoBehavior.StartCoroutine(IEWait(functionWantDelay, timeDelay));
        }

        private static IEnumerator IEWait(Action functionWantDelay, float? timeDelay)
        {
            if (timeDelay.HasValue)
            {
                yield return new WaitForSeconds(timeDelay.Value);
            }
            else
            {
                yield return null;
            }

            functionWantDelay?.Invoke();
        }

        /// <summary>
        /// Convert Integer time to string time with format "hh:mm:ss"
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="isShowHour"></param>
        /// <param name="isShowMinute"></param>
        /// <param name="isShowSec"></param>
        /// <returns></returns>
        public static string ConvertIntToTimeString(int timer, string seperator = " : ", bool isShowHour = true,
            bool isShowMinute = true,
            bool isShowSec = true)
        {
            string time = "";
            if (isShowHour)
            {
                time = Mathf.Floor(timer / 3600).ToString("00");
                if (isShowMinute)
                {
                    time += seperator;
                }
            }

            if (isShowMinute)
            {
                time += Mathf.Floor((timer % 3600) / 60).ToString("00");
                if (isShowSec)
                {
                    time += seperator;
                }
            }

            if (isShowSec)
            {
                time += Mathf.Floor(timer % 60).ToString("00");
            }

            return time;
        }


        /// <summary>
        /// Getting a vector given an angle and bearing from another vector
        /// </summary>
        /// <param name="originalVector">original vector</param>
        /// <param name="angle">angle between 2 vector in degres</param>
        /// <param name="upperAxis">is upper axis</param>
        /// <returns></returns>
        public static Vector3 GetVectorByRotateVectorWithAngle(Vector3 originalVector, float angle, Vector3 upperAxis)
        {
            return Quaternion.AngleAxis(angle, upperAxis) * originalVector;
        }

        /// <summary>
        /// Random a position on NavMesh around origin position 
        /// </summary>
        /// <param name="originPosition"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static Vector3 RandomNavmeshLocation(Vector3 originPosition, float radius)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
            randomDirection += originPosition;
            Vector3 finalPosition = Vector3.zero;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out UnityEngine.AI.NavMeshHit hit, radius, 1))
            {
                finalPosition = hit.position;
            }

            return finalPosition;
        }

        public static Vector3? GetClosestPointValidOnNavmesh(Vector3 position, float radius)
        {
            Vector3? finalPosition = null;
            if (UnityEngine.AI.NavMesh.SamplePosition(position, out UnityEngine.AI.NavMeshHit hit, radius, 1))
            {
                finalPosition = hit.position;
            }

            return finalPosition;
        }

        public static float GetBannerHeight()
        {
            return Mathf.RoundToInt(60 * Screen.dpi / 160);
        }

        public static EventTrigger AddTriggersEvents(this Selectable theSelectable, EventTriggerType eventTriggerType,
            Action<BaseEventData> onTriggerAction = null)
        {
            EventTrigger eventrTrigger = theSelectable.gameObject.AddComponent<EventTrigger>();
            if (onTriggerAction != null)
            {
                EventTrigger.Entry pointerEvent = new EventTrigger.Entry();
                pointerEvent.eventID = eventTriggerType;
                pointerEvent.callback.AddListener((x) => onTriggerAction(x));
                eventrTrigger.triggers.Add(pointerEvent);
            }

            return eventrTrigger;
        }

        public static int GetSDKVersionInt()
        {
            using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
            {
                return version.GetStatic<int>("SDK_INT");
            }
        }

        public static int[] SplitStringToArrayInt(string s, char seperator)
        {
            return Array.ConvertAll(s.Split(new char[] { seperator }, StringSplitOptions.RemoveEmptyEntries),
                new Converter<string, int>(int.Parse));
        }

        public static bool[] SplitStringToArrayBool(string s, char seperator)
        {
            return Array.ConvertAll(s.Split(new char[] { seperator }, StringSplitOptions.RemoveEmptyEntries),
                new Converter<string, bool>(bool.Parse));
        }


        /// <summary>
        ///     save Scriptable Object with input param like "/GenifyStudio/Scenes/Menu/Inventory/Data/HornSkin/"
        /// </summary>
        /// <param name="folderPath">like "/GenifyStudio/Scenes/Menu/Inventory/Data/HornSkin/"</param>
        public static void SaveEditorAssetData(string folderPath)
        {
#if UNITY_EDITOR
            var dir = new DirectoryInfo(Application.dataPath + folderPath);
            var info = dir.GetFiles("*.asset");

            for (var i = 0; i < info.Length; i++)
            {
                var path = "Assets" + folderPath + info[i].Name;
                var data = AssetDatabase.LoadAllAssetsAtPath(path);
                EditorUtility.SetDirty(data[0]);
            }

            Debug.Log("update length " + info.Length);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
        }
    }
}