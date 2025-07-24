using System.Windows.Controls;
using System.Windows.Media;

namespace GameTracker.Extensions
{
    public class ScrollViewerExtension : ScrollViewer
    {
        private double _animationStartOffset;
        private double _animationTargetOffset;
        private double _animationDurationSeconds;
        private DateTime _animationStartTime;
        private bool _isAnimating;

        public void AnimateToVerticalOffset(double to, double durationSeconds = 0.5)
        {
            _animationStartOffset = VerticalOffset;
            _animationTargetOffset = to;
            _animationDurationSeconds = durationSeconds;
            _animationStartTime = DateTime.Now;
            _isAnimating = true;

            CompositionTarget.Rendering -= OnCompositionTargetRendering;
            CompositionTarget.Rendering += OnCompositionTargetRendering;
        }

        private void OnCompositionTargetRendering(object? sender, EventArgs e)
        {
            if (!_isAnimating) return;

            var elapsed = (DateTime.Now - _animationStartTime).TotalSeconds;
            var progress = Math.Min(1.0, elapsed / _animationDurationSeconds);

            // Применим сглаживание (SineEase)
            double eased = 0.5 - 0.5 * Math.Cos(Math.PI * progress);
            double currentOffset = _animationStartOffset + (_animationTargetOffset - _animationStartOffset) * eased;

            ScrollToVerticalOffset(currentOffset);

            if (progress >= 1.0)
            {
                _isAnimating = false;
                CompositionTarget.Rendering -= OnCompositionTargetRendering;
            }
        }
    }
}