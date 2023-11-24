using System;
using System.Diagnostics;

namespace Match3
{
    class Timer
    {
        private Stopwatch sw = Stopwatch.StartNew();
        private TimeSpan time;
        private int seconds = 1;
        public int TimeTick()
        {
            time = TimeSpan.FromMinutes(seconds) - sw.Elapsed;
            seconds = time.Seconds;

            while (seconds > 0)
            {
                time = TimeSpan.FromMinutes(seconds) - sw.Elapsed;
                seconds = time.Seconds;
            }

            return seconds;
        }

        public bool TimeIsOver()
        {
            if (seconds <= 0)
                return false;
            else
                return true;
        }
    }
}
