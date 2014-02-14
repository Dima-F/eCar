using Autofac;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;

namespace eCar.Applicaton.Job
{
    /// <summary>
    /// Пользовательская фабрика задач (тасков).Основана на библиотеке Quartz.dll.Инициализируется с помощью IoC Autofac.
    /// Единственный метод NewJob принимает в качестве параметров связку триггеров (обект типа TriggerFiredBundle) и возвращает
    /// обекти типа IJob.
    /// </summary>
    public class AutofacJobFactory : SimpleJobFactory
    {
        private readonly IContainer _container;

        public AutofacJobFactory(IContainer container)
        {
            _container = container;
        }

        public  IJob NewJob(TriggerFiredBundle bundle)
        {
            return (IJob)_container.Resolve(bundle.JobDetail.JobType);
        }
    }
}