using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    class Timer
    {
        Stopwatch sw = Stopwatch.StartNew();
        TimeSpan time;
        private int seconds = 1;
        //public Timer(double minutes)
        //{
        //    this.time = TimeSpan.FromMinutes(minutes);
        //    seconds = time.Seconds;
        //}
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
