using System;

namespace Kitty
{
    public enum TimeOfDay
    {
        Morning,
        Day,
        Evening,
        Night
    }
    
    [Flags]
    public enum TimesOfDay
    {
        None =      0x0,
        Morning =   0x1,
        Day =       0x2,
        Evening =   0x4,
        Night =     0x8,
        All =       0xF
    }
    
    public static class TimeOfDayExtensions
    {
        public static TimesOfDay ToFlags(this TimeOfDay _day)
            => (TimesOfDay)(1 << (int)_day);
        
        public static bool IsValid(this TimesOfDay _flags, TimeOfDay _day)
            => (_flags & _day.ToFlags()) > 0;
    }
}