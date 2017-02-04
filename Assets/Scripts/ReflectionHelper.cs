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
                //Debug.Log("A) info type: " + info.ToString());
                //Debug.Log("A) info value: " + info.GetValue(obj).ToString());
                info.SetValue(obj, Convert.ChangeType(value, info.FieldType));
                //Debug.Log("A) info result: " + info.GetValue(obj).ToString());

                return;
            }

            object target = obj;
            FieldInfo fieldInfo = null;
            Type type = obj.GetType();
            for (int i = 0; i < nameParts.Length ; i++)
            {
                //if (obj == null) { return; }
                //Debug.Log("part: " + i);

                if(type != null)
                {
                    Debug.LogFormat("Dans \"{0}\" je cherche \"{1}\"", type.FullName, nameParts[i]);
                    fieldInfo = type.GetField(nameParts[i]);
                    if(i >= nameParts.Length - 1)
                    {
                        continue;
                    }
                    target = fieldInfo.GetValue(target);
                    Debug.Log(fieldInfo.ToString());
                    type = fieldInfo.FieldType;
                }
            }
            Debug.Log(fieldInfo.FieldType.FullName);
            fieldInfo.SetValue(target, Convert.ChangeType(value, fieldInfo.FieldType));
            Debug.Log(fieldInfo.GetValue(target));
        }

    }
}