using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace BiuBiuBoom.Utils
{
    public class UtilsClass
    {
        public static float GetAngleFromVectorFloat(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
        }

        // Get Mouse Position in World with Z = 0f
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
            vec.z = 0f;
            return vec;
        }

        public static Vector3 GetMouseWorldPositionWithZ()
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        }

        public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
        }

        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }

        public static Vector3 GetDirToMouse(Vector3 fromPosition)
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            return (mouseWorldPosition - fromPosition).normalized;
        }

        public static void ChangeLayer(Transform transform, int layer)
        {
            if (transform.childCount > 0)//如果子物体存在
            {
                for (int i = 0; i < transform.childCount; i++)//遍历子物体是否还有子物体
                {
                    ChangeLayer(transform.GetChild(i), layer);//这里是只将最后一个无子物体的对象设置层级
                }
                transform.gameObject.layer = layer;//将存在的子物体遍历结束后需要把当前子物体节点进行层级设置
            }
            else                    //无子物体
            {
                transform.gameObject.layer = layer;
            }
        }

        public class Debug
        {
            public static void DrawRect2D(Vector3 from, Vector3 end, Color color)
            {
                Vector3 a = new Vector3(end.x, from.y, 0);
                Vector3 b = new Vector3(from.x, end.y, 0);
                UnityEngine.Debug.DrawLine(from, a, color);
                UnityEngine.Debug.DrawLine(a, end, color);
                UnityEngine.Debug.DrawLine(end, b, color);
                UnityEngine.Debug.DrawLine(b, from, color);
            }

            public static void DrawRect2D(Vector3 middle, float radius, Color color)
            {
                Vector3 from = new Vector3(middle.x - radius, middle.y - radius, 0);
                Vector3 end = new Vector3(middle.x + radius, middle.y + radius, 0);
                DrawRect2D(from, end, color);
            }
        }

        /// <summary>
        /// color 转换hex
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ColorToHex(Color color)
        {
            int r = Mathf.RoundToInt(color.r * 255.0f);
            int g = Mathf.RoundToInt(color.g * 255.0f);
            int b = Mathf.RoundToInt(color.b * 255.0f);
            int a = Mathf.RoundToInt(color.a * 255.0f);
            string hex = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);
            return hex;
        }

        /// <summary>
        /// hex转换到color
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Color HexToColor(string hex)
        {
            byte br = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte bg = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte bb = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte cc = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            float r = br / 255f;
            float g = bg / 255f;
            float b = bb / 255f;
            float a = cc / 255f;
            return new Color(r, g, b, a);
        }
    }
}