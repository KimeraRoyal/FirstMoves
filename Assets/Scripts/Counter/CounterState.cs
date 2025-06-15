using System;

namespace Kitty
{
    public enum CounterState
    {
        PreCountdown,
        Countdown,
        PostCountdown
    }

    [Flags]
    public enum CounterStates
    {
        None = 0x0,
        PreCountdown = 0x1,
        Countdown = 0x2,
        PostCountdown = 0x4,
        All = 0x7
    }
    
    public static class CounterStateExtensions
    {
        public static CounterStates ToFlags(this CounterState _state)
            => (CounterStates)(1 << (int)_state);
        
        public static bool IsValid(this CounterStates _flags, CounterState _state)
            => (_flags & _state.ToFlags()) > 0;
    }
}