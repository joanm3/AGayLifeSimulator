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
                info.SetValue(obj, Convert.ChangeType(value, info.FieldType));
            }

            foreach (string part in nameParts)
            {
                if (obj != null)
                {
                    Type type = obj.GetType();
                    //PropertyInfo info = type.GetProperty(part);
                    FieldInfo info = type.GetField(part);
                    if (info != null)
                    {
                        info.SetValue(obj, Convert.ChangeType(value, info.FieldType));
                    }
                }
            }
        }



    }
}