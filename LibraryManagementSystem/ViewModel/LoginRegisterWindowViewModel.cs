using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModel
{
    public class LoginRegisterWindowViewModel :ViewModelBase
    {

        private ViewModelBase _currentChildView;
        //private bool _isViewVisible = true;
        //public bool IsViewVisible
        //{
        //    get
        //    {
        //        return _isViewVisible;
        //    }

        //    set
        //    {
        //        _isViewVisible = value;
        //        OnPropertyChanged(nameof(IsViewVisible));
        //    }
        //}

        public ICommand ShowLoginRegisterViewCommand {  get; }
        public ICommand ShowLoginViewCommand { get; }  
        public ICommand ShowRegisterViewCommand { get; }
        public ViewModelBase CurrentChildView
        {
            get { return _currentChildView; }
            set { _currentChildView = value; OnPropertyChanged(nameof(CurrentChildView)); }
        }
        public LoginRegisterWindowViewModel() 
        {     
            //trebuie initializate toate paginile in memorie inainte de hide/show
            ShowLoginRegisterViewCommand = new ViewModelCommand(ExecuteShowLoginRegisterViewCommand);
            ShowLoginViewCommand = new ViewModelCommand(ExecuteShowLoginViewCommand);
            ShowRegisterViewCommand = new ViewModelCommand(ExecuteShowRegisterViewCommand);

            //default view       
            //ExecuteShowLoginRegisterViewCommand(null);
        }
        private void ExecuteShowLoginRegisterViewCommand(object obj)
        {

            //CurrentChildView = new LoginRegisterViewModel();
            CurrentChildView = null;
            //IsViewVisible = false;
        }  
        private void ExecuteShowRegisterViewCommand(object obj)
        {
            CurrentChildView=new RegisterViewModel();
        }
        private void ExecuteShowLoginViewCommand(object obj)
        {
            CurrentChildView =new LoginViewModel();
        }
        
    }
}
