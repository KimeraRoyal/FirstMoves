using System.Linq;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(TypingWindow))]
    public class TypingInteractions : MonoBehaviour
    {
        private TypingWindow m_window;

        private InteractionCommand[] m_commands;

        private void Awake()
        {
            m_window = GetComponent<TypingWindow>();
            m_window.OnCurrentTypedMessageSent.AddListener(EvaluateCommand);
        }

        public void AssignCommands(InteractionCommand[] _commands)
        {
            var validCommands = _commands.Where(_command => _command.IsValid()).ToArray();
            if (validCommands.Length < 1) { return; }
            
            m_window.Show();
            m_window.ClearMessages();
            
            m_commands = validCommands;
            foreach (var command in m_commands)
            {
                m_window.TypeMessage(command.Command);
            }
        }

        private void EvaluateCommand(string _command)
        {
            var valid = false;
            foreach (var command in m_commands)
            {
                if(!command.Evaluate(_command)) { continue; }
                command.Execute();
                valid = true;
                break;
            }

            if (valid)
            {
                m_window.Hide();
            }
        }
    }
}