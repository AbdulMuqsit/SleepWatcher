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

                var gone = VisualStateManager.GoToElementState(_root, "Free", false);
            };
            InitializeComponent();
            

        }

        private FrameworkElement _root;

        public override void OnApplyTemplate()
        {
            _root = (FrameworkElement)GetTemplateChild("RootElement");

        }
    }
}
