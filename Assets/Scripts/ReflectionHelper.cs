using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace GayProject.Reflection
{
    public static class ReflectionHelper
    {
        public static object GetFieldValue(this object obj, string propName)
        {
            string[] nameParts = propName.Split('.');
            if (nameParts.Length == 1)
            {
                return obj.GetType().GetField(propName).GetValue(obj);
            }

            foreach (string part in nameParts)
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                //PropertyInfo info = type.GetProperty(part);
                FieldInfo info = type.GetField(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj);
            }
            return obj;
        }

        public static void SetFieldValue(this object obj, string propName, object value)
        {
            string[] nameParts = propName.Split('.');
            if (nameParts.Length == 1)
            {
                FieldInfo info = obj.GetType().GetField(propName);
                Debug.Log("info type: " + info.ToString());
                Debug.Log("info value: " + info.GetValue(obj).ToString());
                info.SetValue(obj, Convert.ChangeType(value, info.FieldType));
                Debug.Log("info result: " + info.GetValue(obj).ToString());

                return;
            }

            for (int i = 0; i < nameParts.Length; i++)
            {
                //if (obj == null) { return; }
                //Debug.Log("part: " + i);

                if (i < nameParts.Length - 1)
                {
                    Type type = obj.GetType();
                    FieldInfo info = type.GetField(nameParts[i]);
                    //Debug.Log(info.ToString());
                    //if (info == null) { return; }
                    obj = info.GetValue(obj);
                }
                else
                {
                    Type type = obj.GetType();
                    FieldInfo info = type.GetField(nameParts[i]);
                    Debug.Log("info type: " + info.ToString());
                    Debug.Log("info value: " + info.GetValue(obj).ToString());

                    //if (info == null) { return; }
                    info.SetValue(obj, Convert.ChangeType(value, info.FieldType), BindingFlags.ExactBinding, null, System.Globalization.CultureInfo.CurrentCulture);
                    Debug.Log("info result: " + info.GetValue(obj).ToString());

                }
            }
        }

    }
}