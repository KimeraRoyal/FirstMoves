using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kitty
{
    [CustomPropertyDrawer(typeof(TaskSelection))]
    public class TaskSelectionDrawer : PropertyDrawer
    {
        private IReadOnlyList<Task> m_tasks;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (m_tasks == null)
            {
                var tasks = Object.FindAnyObjectByType<Tasks>();
                if (!tasks)
                {
                    EditorGUILayout.LabelField("ERROR: No Tasks Object Found");
                    return;
                }

                m_tasks = tasks.AllTasks;
            }
            
            var indexProperty = property.FindPropertyRelative("m_taskIndex");
                
            ShowTaskList(indexProperty,
                new GUIContent("Task", "The assigned task"),
                m_tasks);

            EditorGUI.EndProperty();
        }
        
        private void ShowTaskList(SerializedProperty property, GUIContent label, IReadOnlyList<Task> entries)
        {
            if (property == null)
            {
                return;
            }

            var objectNames = new List<GUIContent>();

            var selectedIndex = property.intValue + 1;

            objectNames.Add(new GUIContent("<None>"));
            foreach (var task in entries)
            {
                objectNames.Add(new GUIContent(task.Name));
            }

            selectedIndex = EditorGUILayout.Popup(label, selectedIndex, objectNames.ToArray());

            property.intValue = selectedIndex - 1;
        }
    }
}