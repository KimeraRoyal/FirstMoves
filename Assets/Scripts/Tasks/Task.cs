using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kitty
{
    [Serializable]
    public class Task
    {
        [SerializeField] private string m_name = "Task";
        [SerializeField] private string m_description = "You have to do the task";
        [FormerlySerializedAs("m_markedOff")] [SerializeField] private bool m_marked;

        public string Name => m_name;
        public string Description => m_description;
        public bool Marked
        {
            get => m_marked;
            set
            {
                if(m_marked == value) { return; }

                m_marked = value;
                if(m_marked) { OnMarked?.Invoke(); }
                else { OnUnmarked?.Invoke(); }
            }
        }

        public Action OnMarked;
        public Action OnUnmarked;
    }
}