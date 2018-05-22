namespace MOM.SandBox.HealthCheckAPI
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    
    /// <summary>
    /// A class for a daily trigger of health check api envent
    /// <Ref>https://stackoverflow.com/questions/4529019/how-to-use-the-net-timer-class-to-trigger-an-event-at-a-specific-time</Ref>
    /// </summary>
    public class DailyTrigger
    {
        // Time span trigger
        private readonly TimeSpan triggerHour;

        public DailyTrigger(int hour, int minutes = 0, int seconds = 0)
        {
            triggerHour = new TimeSpan(hour, minutes, seconds);
            
            InitiateAsync();
        }

        async void InitiateAsync()
        {
            while (true)
            {
                var triggerTime = DateTime.Today + triggerHour - DateTime.Now;
                if (triggerTime < TimeSpan.Zero)
                    triggerTime = triggerTime.Add(new TimeSpan(24, 0, 0)); // Add another 24 hours
                // When trigger time hits 0 this Task will trigger an event
                await Task.Delay(triggerTime); 
                OnTimeTriggered?.Invoke();
            }
        }

        public event Action OnTimeTriggered;

        public void InitTimer()
        {
            DateTime time = DateTime.Now;
            int second = time.Second;
            int minute = time.Minute;
            if (second != 0)
            {
                minute = minute > 0 ? minute-- : 59;
            }

            if (minute == 0 && second == 0)
            {
                TimeSpan span = new TimeSpan(18, 60 - minute, 60 - second);
                
            }
        }
    }
}
