using UnityEngine;

namespace Kitty
{
    [CreateAssetMenu(fileName = "New Interactable Data", menuName = "Kitty/Interactable")]
    public class InteractableData : ScriptableObject
    {
        [SerializeField] private string m_prompt = "This is the prompt to display.";

        public string Prompt => m_prompt;
    }
}
