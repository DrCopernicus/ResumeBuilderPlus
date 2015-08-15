using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ResumeBuilderPlus.UI
{
    // please see:
    // http://www.ageektrapped.com/blog/the-missing-net-4-cue-banner-in-wpf-i-mean-watermark-in-wpf/
    // for more information
    public class CueBannerAdorner : Adorner
    {
        private readonly ContentPresenter contentPresenter;

        public CueBannerAdorner(UIElement adornedElement, object cueBanner) :
            base(adornedElement)
        {
            this.IsHitTestVisible = false;

            contentPresenter = new ContentPresenter();
            contentPresenter.Content = cueBanner;
            contentPresenter.Opacity = 0.7;
            contentPresenter.Margin =
               new Thickness(3 + Control.Margin.Left + Control.Padding.Left,
                             Control.Margin.Top + Control.Padding.Top, 0, 0);
        }

        private Control Control
        {
            get { return (Control)this.AdornedElement; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return contentPresenter;
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            contentPresenter.Measure(Control.RenderSize);
            return Control.RenderSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            contentPresenter.Arrange(new Rect(finalSize));
            return finalSize;
        }
    }
}
