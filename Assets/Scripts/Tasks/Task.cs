using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kitty
{
    [Serializable]
    public class Task
    {
        [SerializeField] private string m_name = "Task";

        public string Name => m_name;
        public bool Marked
        {
            get => IsMarked.Invoke();
            set
            {
                if(value) { OnMarked?.Invoke(); }
                else { OnUnmarked?.Invoke(); }
            }
        }

        public Action OnMarked;
        public Action OnUnmarked;

        public Func<bool> IsMarked;
    }
}