using CommunityToolkit.Mvvm.ComponentModel;
using GameTracker.ViewModels.Interfaces;

namespace GameTracker.ViewModels
{
    internal partial class CatalogueViewModel : ObservableObject, IViewModel
    {
        public string PageName { get; set; } = "Catalogue";
    }
}