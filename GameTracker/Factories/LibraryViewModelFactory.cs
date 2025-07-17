using GameTracker.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTracker.Factories
{
    internal class LibraryViewModelFactory : IViewModelFactory
    {
        private IServiceProvider _services;

        public LibraryViewModelFactory(IServiceProvider services)
        {
            _services = services;
        }

        public object CreateViewModel()
        {
            return _services.GetRequiredService<LibraryViewModel>();
        }
    }
}