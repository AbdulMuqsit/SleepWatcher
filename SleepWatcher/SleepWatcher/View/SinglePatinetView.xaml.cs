using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace SleepWatcher.View
{
    /// <summary>
    /// Interaction logic for SinglePatinetView.xaml
    /// </summary>
    public partial class SinglePatinetView : UserControl
    {
        public SinglePatinetView()
        {
            Initialized += (sender, args) =>
            {
                ApplyTemplate();

                var vsmGroups = VisualStateManager.GetVisualStateGroups(Root);
                var gone = VisualStateManager.GoToElementState(Root, "Free", false);
                Debug.Write(gone);
            };
            InitializeComponent();
            

        }

        private FrameworkElement Root;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Root = (FrameworkElement)GetTemplateChild("RootElement");
            Debug.Write(Root);

        }
    }
}
