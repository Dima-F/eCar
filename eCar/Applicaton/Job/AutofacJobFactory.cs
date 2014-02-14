using Autofac;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;

namespace eCar.Applicaton.Job
{
    /// <summary>
    /// ���������������� ������� ����� (������).�������� �� ���������� Quartz.dll.���������������� � ������� IoC Autofac.
    /// ������������ ����� NewJob ��������� � �������� ���������� ������ ��������� (����� ���� TriggerFiredBundle) � ����������
    /// ������ ���� IJob.
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