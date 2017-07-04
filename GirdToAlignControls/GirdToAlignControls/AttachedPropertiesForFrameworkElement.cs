using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GirdToAlignControls
{
    public class AttachedPropertiesForFrameworkElement
    {
        public static readonly DependencyProperty ShowGridProperty = DependencyProperty.RegisterAttached(
            "ShowGrid", typeof(bool), typeof(AttachedPropertiesForFrameworkElement),
            new PropertyMetadata(default(bool), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var panel = dependencyObject as Panel;
            if (panel == null)
            {
                throw new ArgumentException("Attached object is not a Panel");
            }

            var backgroundBrush = CreateBrush(panel);
            panel.Background = backgroundBrush;

        }

        private static Brush CreateBrush(FrameworkElement frameworkElement)
        {
            var size = (Size)frameworkElement.GetValue(GridSizeProperty);
            var pen = new Pen(Brushes.DarkGray, 0.1);

            var blackBrush = new DrawingBrush();
            var rectangleGeometry = new RectangleGeometry(new Rect(size));
            pen.Freeze();
            var geometryDrawing = new GeometryDrawing(null, pen, rectangleGeometry);
            blackBrush.Drawing = geometryDrawing;

            geometryDrawing.Freeze();

            // Set Viewport and TileMode
            blackBrush.Viewport = new Rect(size);
            blackBrush.ViewportUnits = BrushMappingMode.Absolute;
            blackBrush.TileMode = TileMode.Tile;

            blackBrush.Freeze();

            return blackBrush;

        }

        public static void SetShowGrid(DependencyObject element, bool value)
        {
            element.SetValue(ShowGridProperty, value);
        }

        public static bool GetShowGrid(DependencyObject element)
        {
            return (bool)element.GetValue(ShowGridProperty);
        }

        public static readonly DependencyProperty GridSizeProperty = DependencyProperty.RegisterAttached(
            "GridSize", typeof(Size), typeof(AttachedPropertiesForFrameworkElement),
            new PropertyMetadata(default(Size), PropertyChangedCallback));


        public static void SetGridSize(DependencyObject element, Size value)
        {
            element.SetValue(GridSizeProperty, value);
        }

        public static Size GetGridSize(DependencyObject element)
        {
            return (Size)element.GetValue(GridSizeProperty);
        }
    }
}