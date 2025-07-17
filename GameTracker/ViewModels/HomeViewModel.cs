using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameTracker.Enums;
using GameTracker.Factories;
using GameTracker.Models;
using GameTracker.Services;
using GameTracker.ViewModels.Interfaces;
using System.Collections.ObjectModel;

namespace GameTracker.ViewModels
{
    internal partial class HomeViewModel : ObservableObject, IViewModel
    {
        public string PageName { get; set; } = "Home";

        public HomeViewModel()
        {
            
        }
    }
}