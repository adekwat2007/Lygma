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

        private void AnimateOpacity(UIElement element, double toOpacity, int durationInMs)
        {
            var animation = new DoubleAnimation
            {
                To = toOpacity,
                Duration = TimeSpan.FromMilliseconds(durationInMs), // Длительность анимации в мс
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            element.BeginAnimation(OpacityProperty, animation);
        }
        
        private void AnimateWidth(UIElement element, double toWidth, int durationInMs)
        {
            var animation = new DoubleAnimation
            {
                To = toWidth,
                Duration = TimeSpan.FromMilliseconds(durationInMs), // Длительность анимации в мс
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            element.BeginAnimation(WidthProperty, animation);
        }

        #region Анимация GridSplitter

        private void MenuSplitter_OnDragStarted(object sender, DragStartedEventArgs e) => AnimateOpacity(SideMenu, 0.5, 300);

        private void MenuSplitter_OnDragCompleted(object sender, DragCompletedEventArgs e) => AnimateOpacity(SideMenu, 1, 300);


        #endregion Анимация GridSplitter

        #region Анимация поиска

        private void SearchTextBox_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) =>
            AnimateWidth(SearchTextBox, 250, 200);
        private void SearchTextBox_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) =>
            AnimateWidth(SearchTextBox, 200, 200);
        #endregion

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e) => Keyboard.ClearFocus();
    }
}