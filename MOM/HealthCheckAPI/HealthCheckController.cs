namespace MOM.SandBox.HealthCheckAPI
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Diagnostics;

    public class HealthCheckController
    {
        private APIServiceContainer APIContainer { get; set; }

        public HealthCheckController()
        {
            this.APIContainer = new APIServiceContainer();
        }

        public void AddService(AHealthCheck service) { this.APIContainer.Services.Add(service); }

        public void Run()
        {
            DailyTrigger trigger = new DailyTrigger(20,25,53);
            trigger.OnTimeTriggered += () =>
            {
                checkServices(new object()); // The event to be triggered
            };
        }
        // Check services on start up
        public void checkServices() { checkServices(new object()); }
        /// <summary>
        /// Checks the services.
        /// </summary>
        /// <param name="stateInfo">The state information.</param>
        public void checkServices(Object stateInfo)
        {
            Debug.WriteLine("State Info: " + stateInfo);
            String msgConsole = "Service Name\t\t||Running\t\t||Check Stamp" + "\n";
            foreach (AHealthCheck service in APIContainer.Services)
            {                
                String response = service.CheckHealth();
                JObject jsonResponse = JObject.Parse(response);
                Debug.WriteLine("Json Object: " + jsonResponse.ToString());

                service.IsRunning = (bool)jsonResponse["success"];
                service.LastCheck = DateTime.Now;

                if (service.IsRunning)
                {
                    msgConsole += service.ServiceName + "\t\t||\t\t" + service.IsRunning + "\t||\t" + service.LastCheck + "\n";
                } else
                {
                    msgConsole += service.ServiceName + "\t\t||\t\t" + service.IsRunning + "\t||\t" + service.LastCheck + "\n";
                }
            }

            Console.WriteLine(msgConsole);
        }
    }
}
