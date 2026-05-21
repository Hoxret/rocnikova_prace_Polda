using System.Windows;
using System.Windows.Controls;

namespace Vytah
{
    /// <summary>
    /// Attached behavior pro procent-based pozicování prvků
    /// Umožňuje pozicovat prvky pomocí procent místo fixních pixelů
    /// </summary>
    public static class PercentPosition
    {
        // LeftPercent - procento zleva (0-100)
        public static double GetLeftPercent(DependencyObject obj) => (double)obj.GetValue(LeftPercentProperty);
        public static void SetLeftPercent(DependencyObject obj, double value) => obj.SetValue(LeftPercentProperty, value);

        public static readonly DependencyProperty LeftPercentProperty =
            DependencyProperty.RegisterAttached(
                "LeftPercent",
                typeof(double),
                typeof(PercentPosition),
                new PropertyMetadata(0.0, OnPositionChanged));

        // TopPercent - procento shora (0-100)
        public static double GetTopPercent(DependencyObject obj) => (double)obj.GetValue(TopPercentProperty);
        public static void SetTopPercent(DependencyObject obj, double value) => obj.SetValue(TopPercentProperty, value);

        public static readonly DependencyProperty TopPercentProperty =
            DependencyProperty.RegisterAttached(
                "TopPercent",
                typeof(double),
                typeof(PercentPosition),
                new PropertyMetadata(0.0, OnPositionChanged));

        // BottomPercent - procento zdola (0-100)
        public static double GetBottomPercent(DependencyObject obj) => (double)obj.GetValue(BottomPercentProperty);
        public static void SetBottomPercent(DependencyObject obj, double value) => obj.SetValue(BottomPercentProperty, value);

        public static readonly DependencyProperty BottomPercentProperty =
            DependencyProperty.RegisterAttached(
                "BottomPercent",
                typeof(double),
                typeof(PercentPosition),
                new PropertyMetadata(0.0, OnPositionChanged));

        // WidthPercent - šířka jako procento rodičovského prvku
        public static double GetWidthPercent(DependencyObject obj) => (double)obj.GetValue(WidthPercentProperty);
        public static void SetWidthPercent(DependencyObject obj, double value) => obj.SetValue(WidthPercentProperty, value);

        public static readonly DependencyProperty WidthPercentProperty =
            DependencyProperty.RegisterAttached(
                "WidthPercent",
                typeof(double),
                typeof(PercentPosition),
                new PropertyMetadata(0.0, OnSizeChanged));

        // HeightPercent - výška jako procento rodičovského prvku
        public static double GetHeightPercent(DependencyObject obj) => (double)obj.GetValue(HeightPercentProperty);
        public static void SetHeightPercent(DependencyObject obj, double value) => obj.SetValue(HeightPercentProperty, value);

        public static readonly DependencyProperty HeightPercentProperty =
            DependencyProperty.RegisterAttached(
                "HeightPercent",
                typeof(double),
                typeof(PercentPosition),
                new PropertyMetadata(0.0, OnSizeChanged));

        private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                element.Loaded += (s, args) => UpdatePosition(element);
                element.SizeChanged += (s, args) => UpdatePosition(element);
                // Najdi rodiče a attach SizeChanged
                if (element.Parent is FrameworkElement parent)
                {
                    parent.SizeChanged += (s, args) => UpdatePosition(element);
                }
            }
        }

        private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                element.Loaded += (s, args) => UpdateSize(element);
                element.SizeChanged += (s, args) => UpdateSize(element);
                if (element.Parent is FrameworkElement parent)
                {
                    parent.SizeChanged += (s, args) => UpdateSize(element);
                }
            }
        }

        private static void UpdatePosition(FrameworkElement element)
        {
            if (element.Parent is not Grid grid) return;

            double leftPercent = GetLeftPercent(element);
            double topPercent = GetTopPercent(element);
            double bottomPercent = GetBottomPercent(element);

            double left = (grid.ActualWidth * leftPercent) / 100;
            double top = (grid.ActualHeight * topPercent) / 100;
            double bottom = (grid.ActualHeight * bottomPercent) / 100;

            var margin = element.Margin;

            if (leftPercent > 0)
                margin.Left = left;
            if (topPercent > 0)
                margin.Top = top;
            if (bottomPercent > 0)
                margin.Bottom = bottom;

            element.Margin = margin;
        }

        private static void UpdateSize(FrameworkElement element)
        {
            if (element.Parent is not Grid grid) return;

            double widthPercent = GetWidthPercent(element);
            double heightPercent = GetHeightPercent(element);

            if (widthPercent > 0)
                element.Width = (grid.ActualWidth * widthPercent) / 100;

            if (heightPercent > 0)
                element.Height = (grid.ActualHeight * heightPercent) / 100;
        }
    }
}
