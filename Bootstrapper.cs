using Caliburn.Micro;
using CaliburnExperiments.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CaliburnExperiments;

public class Bootstrapper:BootstrapperBase
{

    public IServiceProvider? ServiceProvider { get; set; }

    public IConfiguration? Configuration { get; set; }

    public Bootstrapper()
    {
        Initialize();
    }
    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IWindowManager, WindowManager>();

        services.AddSingleton<IEventAggregator, EventAggregator>();

        GetType().Assembly.GetTypes()
         .Where(type => type.IsClass)
         .Where(type => type.Name.EndsWith("ViewModel"))
         .ToList().ForEach(viewModelType => services.AddTransient(
             viewModelType, viewModelType));

    }

    protected async override void OnStartup(object sender, StartupEventArgs e)
    {
        base.OnStartup(sender, e);
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        ServiceProvider = serviceCollection.BuildServiceProvider();
  
        await DisplayRootViewForAsync<ShellViewModel>();
    }

    protected override object GetInstance(Type service, string key)
    {
        return ServiceProvider.GetRequiredService(service);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
        return ServiceProvider.GetServices(service);
    }
}
