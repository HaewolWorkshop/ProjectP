using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace HaewolWorkshop
{
    [CustomEditor(typeof(FSMPlayer<>), true)]
    public class FSMPlayerEditor : Editor
    {
        private struct State
        {
            public int id;
            public string name;
        }

        private Type targetType = null;

        private int statePopupIndex = 0;

        private void Awake()
        {
            targetType = target.GetType();
            while (targetType.BaseType != typeof(MonoBehaviour))
            {
                targetType = targetType.BaseType;
            }
        }

        public override void OnInspectorGUI()
        {
            ShowStateInfo();

            EditorGUILayout.Space();

            base.OnInspectorGUI();
        }

        private void ShowStateInfo()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            var currentStateName = GetTypeName(GetValue("currentState"));
            var previousStateName = GetTypeName(GetValue("previousState"));
            var globalStateName = GetTypeName(GetValue("globalState"));

            var ids = new List<int>();
            var names = new List<string>();
            SetStateList((dynamic)GetValue("states"), ids, names, currentStateName);


            EditorGUILayout.LabelField($"GLOBAL : {globalStateName}");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"{previousStateName} -> ");

            int newValue = EditorGUILayout.Popup(statePopupIndex, names.ToArray());

            if (newValue != statePopupIndex)
            {
                statePopupIndex = newValue;
                targetType.GetMethod("ChangeState").Invoke(target, new object[1] { ids[statePopupIndex] });
            }

            EditorGUILayout.EndHorizontal();



        }

        private object GetValue(string fieldName)
        {
            return targetType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default)?.GetValue(target);
        }

        private string GetTypeName(object type)
        {
            return type?.ToString()?.Split('.')[^1] ?? "None";
        }

        private void SetStateList<T>(Dictionary<int, T> states, List<int> ids, List<string> names, string currentStateName)
        {
            var keys = states.Keys.ToList();
            var values = states.Values.ToList();
            for (int i = 0; i < states.Count; i++)
            {
                var name = GetTypeName(values[i]);
                names.Add(name);

                ids.Add(keys[i]);

                if (name == currentStateName)
                {
                    statePopupIndex = i;
                }
            }
        }
    }
}