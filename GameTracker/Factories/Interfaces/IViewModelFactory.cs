using GameTracker.Enums;
using GameTracker.ViewModels.Interfaces;

namespace GameTracker.Factories.Interfaces
{
    internal interface IViewModelFactory
    {
        IViewModel CreateViewModel(ViewType viewType);
    }
}
