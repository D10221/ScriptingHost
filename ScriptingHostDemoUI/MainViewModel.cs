using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using App;
using ScriptingHost;

namespace ScriptingHostDemoUI
{
    public sealed class MainViewModel:INotifyPropertyChanged
    {
        #region BackingFields

        private string _code;
        private string _scriptResult;
        private bool _running;
        private IEnumerable<ScriptInfo> _scripts;
        private ScriptInfo _selectedScript;

        #endregion


        public MainViewModel(ScriptRunner scriptRunner)
        {
            var scriptManager = new ScriptManager();
            
            Scripts = scriptManager.Scripts;
            
            SelectScriptCmd = new RelayCommand(x=> SelectedScript = x as ScriptInfo);

            ExecuteScript = new RelayCommand(async scriptInfo =>
            {
                Running = true;

                try
                {
                    var target = scriptInfo as ScriptInfo;
                    if (target == null)
                    {
                        ScriptResult = "No Script Provided";
                        return;
                    }

                    var scriptResult = await scriptRunner.Execute(target);

                    ScriptResult = scriptResult as string;
                }
                catch (Exception e)
                {
                    ScriptResult = e.Message;
                }
                finally
                {
                    Running = false;
                }
               

            }, x=> SelectedScript!=null && ScriptResult==null);

            ClearError = new RelayCommand(x =>
            {
                ScriptResult = null;               
            });

           // Code = "Console.WriteLine(\"Hi\");";
        }
        
        public ICommand SelectScriptCmd { get; set; }

        public ScriptInfo SelectedScript
        {
            get { return _selectedScript; }
            set
            {
                if (value == _selectedScript) return;
                _selectedScript = value; 
                OnPropertyChanged();
                RaiseCanExecuteChanged();
            }
        }

        public IEnumerable<ScriptInfo> Scripts
        {
            get { return _scripts; }
            set { _scripts = value; OnPropertyChanged();}
        }

        public bool Running
        {
            get { return _running; }
            set
            {
                if (value.Equals(_running)) return;
                _running = value;
                OnPropertyChanged();
            }
        }

        private void RaiseCanExecuteChanged()
        {
            RaiseCanExecuteChanged(ClearError);
            RaiseCanExecuteChanged(ExecuteScript);
        }

        void RaiseCanExecuteChanged(ICommand cmd)
        {
            var relayCommand = cmd as RelayCommand;
            if (relayCommand != null) relayCommand.RaiseCanExecuteChanged();
        }

        public String Code
        {
            get { return _code; }
            set
            {
                if (value == _code) return;
                _code = value;
                OnPropertyChanged();
                RaiseCanExecuteChanged();
            }
        }

        public string ScriptResult
        {
            get { return _scriptResult; }
            set
            {
                if (Equals(value, _scriptResult)) return;
                _scriptResult = value;
                OnPropertyChanged();
                RaiseCanExecuteChanged();
            }
        }

        public ICommand ExecuteScript { get; private set; }

        public ICommand ClearError { get; private set; }

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}