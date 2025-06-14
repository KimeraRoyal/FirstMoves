using System;

namespace Kitty
{
    [Flags]
    public enum DaysOfWeek
    {
        None =      0x0,
        Sunday =    0x1,
        Monday =    0x2,
        Tuesday =   0x4,
        Wednesday = 0x8,
        Thursday =  0x10,
        Friday =    0x20,
        Saturday =  0x40,
        All =       0x7F
    }
    
    public static class DayOfWeekExtensions
    {
        public static DaysOfWeek ToFlags(this DayOfWeek _day)
            => (DaysOfWeek)(1 << (int)_day);
        
        public static bool IsValid(this DaysOfWeek _flags, DayOfWeek _day)
            => (_flags & _day.ToFlags()) > 0;
    }
}