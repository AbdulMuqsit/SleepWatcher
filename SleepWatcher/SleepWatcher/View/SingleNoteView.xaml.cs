using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SleepWatcher.View
{
    /// <summary>
    /// Interaction logic for SingleNoteView.xaml
    /// </summary>
    public partial class SingleNoteView : UserControl
    {
        public SingleNoteView()
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
            base.OnApplyTemplate();
            _root = (FrameworkElement)GetTemplateChild("grid");

        }
    }
}
