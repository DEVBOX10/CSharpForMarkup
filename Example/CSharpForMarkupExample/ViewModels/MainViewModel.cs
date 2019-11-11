﻿using System.Threading.Tasks;
using System.Windows.Input;
using QuickCross;

namespace CSharpForMarkupExample.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        readonly App app;
        
        ICommand continueToRegistrationCommand, continueToNestedListCommand, continueToCSharpForMarkupCommand;
        
        public MainViewModel(App app) { this.app = app; }

        public string Title => "CSharpForMarkup and LiveSharp";
        
        public string SubTitle => "Example pages";
        
        public ICommand ContinueToRegistrationCommand => continueToRegistrationCommand ?? (continueToRegistrationCommand = new RelayCommandAsync(ContinueToRegistration));
        public ICommand ContinueToNestedListCommand => continueToNestedListCommand ?? (continueToNestedListCommand = new RelayCommandAsync(ContinueToNestedList));
        public ICommand ContinueToCSharpForMarkupCommand => continueToCSharpForMarkupCommand ?? (continueToCSharpForMarkupCommand = new RelayCommand(ContinueToCSharpForMarkup));

        Task ContinueToRegistration() => app.ContinueToRegistration();
        Task ContinueToNestedList() => app.ContinueToNestedList();
        void ContinueToCSharpForMarkup() => app.OpenUri("https://github.com/VincentH-Net/CSharpForMarkup");
    }
}
