using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Autofac;
using ScriptingHost;
using IContainer = System.ComponentModel.IContainer;

namespace ScriptingHostDemoUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private IContainer _container;
        
        /*void BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();
            
            containerBuilder.RegisterAssemblyTypes(asm).Where(t => t.IsNested).PropertiesAutowired();
            containerBuilder.Register(c => _initObject).As<IScriptParams>();
            containerBuilder.Register(c => _context).As<ITaskContext>();
            containerBuilder.Register(c => _currentTaskConfigurator).As<IConfigureTask>();
            _container = containerBuilder.Build();
        }*/

        public App()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.GetInterfaces().Contains(typeof(IScriptConfig)))
                .As<IScriptConfig>();

            var container = builder.Build();
            Resources.Add("Container", container);
            Resources.Add("ScriptConfigs", container.Resolve<IEnumerable<IScriptConfig>>());

            //var pluginClasses = container.Resolve<IEnumerable<IScriptConfig>>();
        }
    }
            
}
