using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using GameTracker.ViewModels;

namespace GameTracker.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        #region Команды для кнопок окна

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
        }

        #endregion Команды для кнопок окна

        #region Анимация GridSplitter

        private void MenuSplitter_OnDragStarted(object sender, DragStartedEventArgs e) => AnimateOpacity(SideMenu, 0.5);

        private void MenuSplitter_OnDragCompleted(object sender, DragCompletedEventArgs e) => AnimateOpacity(SideMenu, 1);

        private void AnimateOpacity(UIElement element, double toOpacity)
        {
            var animation = new DoubleAnimation
            {
                To = toOpacity,
                Duration = TimeSpan.FromMilliseconds(300), // Длительность анимации в мс
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            element.BeginAnimation(OpacityProperty, animation);
        }

        #endregion Анимация GridSplitter

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
                await vm.LoadData();
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e) => Keyboard.ClearFocus();
    }
}