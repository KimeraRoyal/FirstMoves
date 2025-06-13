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
            var nameProperty = property.FindPropertyRelative("m_taskName");
                
            ShowTaskList(indexProperty, nameProperty,
                new GUIContent("Task", "The assigned task"));

            EditorGUI.EndProperty();
        }
        
        private void ShowTaskList(SerializedProperty indexProperty, SerializedProperty nameProperty, GUIContent label)
        {
            if (indexProperty == null)
            {
                return;
            }

            var objectNames = new List<GUIContent> { new("<None>") };

            var selectedIndex = indexProperty.intValue + 1;
            var name = nameProperty.stringValue;
            
            for (var i = 0; i < m_tasks.Count; i++)
            {
                objectNames.Add(new GUIContent(m_tasks[i].Name));
                
                if (m_tasks[i].Name != name) { continue; }
                
                selectedIndex = i + 1;
            }

            selectedIndex = EditorGUILayout.Popup(label, selectedIndex, objectNames.ToArray());

            indexProperty.intValue = selectedIndex - 1;
            name = selectedIndex > 0 ? m_tasks[selectedIndex - 1].Name : "";
            
            nameProperty.stringValue = name;
        }
    }
}