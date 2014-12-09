using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using ScriptingHost;

namespace ScriptingHostDemoUI
{
    public partial class MainWindow 
    {
        private readonly IDisposable _selectedScriptSchangedSubscription;

        public MainWindow()
        {
            var scriptConfigs = Application.Current.Resources["ScriptConfigs"] as IEnumerable<IScriptConfig>;

            DataContext = new MainViewModel(new ScriptRunner(new ScriptProvider(), new ScriptCsHost(), new ScriptConfigManager(scriptConfigs)));
            InitializeComponent();            

            _selectedScriptSchangedSubscription = OnSelectionChanged().Subscribe(x =>
            {
                var mainViewModel = DataContext as MainViewModel;
                if (mainViewModel != null)
                {
                    var listBox = x.Sender as Selector;
                    if (listBox != null)
                        mainViewModel.SelectedScript = listBox.SelectedItem as ScriptInfo;
                }
            });
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_selectedScriptSchangedSubscription != null) 
                _selectedScriptSchangedSubscription.Dispose();
            base.OnClosing(e);
        }

        private IObservable<EventPattern<SelectionChangedEventArgs>> OnSelectionChanged()
        {
            return Observable.FromEventPattern<SelectionChangedEventHandler,SelectionChangedEventArgs>(x => XScriptList.SelectionChanged += x,
                x => XScriptList.SelectionChanged -= x);
        }

      
    }
}
