namespace MOM.SandBox.HealthCheckAPI
{
    using MOM.WebServiceControllers;
    using System.Collections.Generic;

    public class APIServiceContainer
    {
        // Collections
        public ICollection<AHealthCheck> Services { get; set; }

        // Constructors
        public APIServiceContainer()
        {
            this.Services = new HashSet<AHealthCheck>();
            GenerateCollectionOfServices();
        }
        /// <summary>
        /// Adds the service.
        /// </summary>
        /// <param name="service">The service.</param>
        public void AddService(AHealthCheck service)
        {
            if (service == null)
            {
                return;
            }
            this.Services.Add(service);
        }
        /// <summary>
        /// Generates the collection of services.
        /// Add new services here to be part of the program startup
        /// </summary>
        private void GenerateCollectionOfServices()
        {
            // AddService(new TestService());
            AddService(new AttendanceLogWS());
            AddService(new CourseAccessWS());
            AddService(new GodkodeService());
        }
    }
}
