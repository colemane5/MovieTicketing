using SharedResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieTicketingApp.ViewModels
{
    public class LoginViewModel(ICommand loginCommand) : ViewModelBase
    {
        public ICommand LoginCommand { get; } = loginCommand;
		private string _email = "";
		public string Email
		{
			get => _email;
			set
			{
				_email = value;
				OnPropertyChanged(nameof(Email));
			}
		}
	}
}
