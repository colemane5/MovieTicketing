﻿using SharedResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharedResources.Commands
{
    public class LogoutCommand(Func<NavigationService?> getNavigationService) : ICommand
    {
        private readonly Func<NavigationService?> _getNavigationService = getNavigationService;

#pragma warning disable CS0067 // The event 'LogoutCommand.CanExecuteChanged' is never used
        public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067 // The event 'LogoutCommand.CanExecuteChanged' is never used

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            _getNavigationService()?.ReturnToStart();
        }
    }
}