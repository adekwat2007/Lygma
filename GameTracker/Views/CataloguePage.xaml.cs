using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using GameTracker.Extensions;

namespace GameTracker.Views
{
    public partial class CataloguePage : UserControl
    {
        public CataloguePage()
        {
            InitializeComponent();
        }

        private void GamesGrid_OnLoaded(object sender, RoutedEventArgs e)
        {
            var stop = (GradientStop)((Grid)sender).Resources["GameLoadingStop"];
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                RepeatBehavior = RepeatBehavior.Forever,
            };
            stop.BeginAnimation(GradientStop.OffsetProperty, animation);
        }
    }
}