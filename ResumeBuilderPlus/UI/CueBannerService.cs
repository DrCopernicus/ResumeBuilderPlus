using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using ResumeBuilderPlus.VVM;

namespace ResumeBuilderPlus.UI
{
    // please see:
    // http://www.ageektrapped.com/blog/the-missing-net-4-cue-banner-in-wpf-i-mean-watermark-in-wpf/
    // for more information
    public class CueBannerService
    {
        public static readonly DependencyProperty CueBannerProperty = DependencyProperty.RegisterAttached("CueBanner", typeof(object), typeof(CueBannerService), new FrameworkPropertyMetadata(null, CueBannerPropertyChanged));

        public static object GetCueBanner(Control control)
        {
            return control.GetValue(CueBannerProperty);
        }

        public static void SetCueBanner(Control control, object value)
        {
            control.SetValue(CueBannerProperty, value);
        }

        private static void CueBannerPropertyChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs args)
        {
            Control control = (Control) dependencyObject;
            control.Loaded += ControlLoaded;
            if (dependencyObject is TextBox || dependencyObject is ComboBox)
            {
                control.GotFocus += ControlGotFocus;
                control.LostFocus += ControlLostFocus;
            }
        }

        private static void ControlGotFocus(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            if (ShouldShowCueBanner(control))
            {
                RemoveCueBanner(control);
            }
        }

        private static void ControlLoaded(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            if (ShouldShowCueBanner(control))
            {
                ShowCueBanner(control);
            }
        }

        private static void ControlLostFocus(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            if (ShouldShowCueBanner(control))
            {
                ShowCueBanner(control);
            }
        }

        private static void RemoveCueBanner(UIElement control)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);

            Adorner[] adorners = layer.GetAdorners(control);
            if (adorners == null) return;
            foreach (Adorner adorner in adorners)
            {
                if (adorner is CueBannerAdorner)
                {
                    adorner.Visibility = Visibility.Hidden;
                    layer.Remove(adorner);
                }
            }
        }

        private static void ShowCueBanner(Control control)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);
            layer.Add(new CueBannerAdorner(control, GetCueBanner(control)));
        }

        private static bool ShouldShowCueBanner(Control c)
        {
            DependencyProperty dp = GetDependencyProperty(c);
            if (dp == null) return true;
            return c.GetValue(dp).Equals("");
        }

        private static DependencyProperty GetDependencyProperty(Control control)
        {
            if (control is ComboBox)
                return ComboBox.TextProperty;
            if (control is TextBoxBase)
                return TextBox.TextProperty;
            return null;
        }
    }
}
