namespace MOM.SandBox.HealthCheckAPI
{
    using System;
    public abstract class AHealthCheck : IHealthCheck
    {
        public Boolean IsRunning { get; set; }
        public DateTime LastCheck { get; set; }
        public String ServiceName { get; set; }
        protected AHealthCheck(String name)
        {
            this.ServiceName = name;
        }
        public abstract string CheckHealth();
    }
}
