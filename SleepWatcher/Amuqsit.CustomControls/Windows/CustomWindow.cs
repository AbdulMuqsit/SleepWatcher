using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using ChatApp.Infrastructure;
//using System.Threading.Tasks;

namespace Amuqsit.CustomControls.Windows
{
    /// <summary>
    /// Interaction logic for CustomWindow.xaml
    /// </summary>
    public partial class CustomWindow : Window
    {
        
        private Point _cursorOffset;
        private FrameworkElement _borderLeft;
        private FrameworkElement _borderTopLeft;
        private FrameworkElement _borderTop;
        private FrameworkElement _borderTopRight;
        private FrameworkElement _borderRight;
        private FrameworkElement _borderBottomRight;
        private FrameworkElement _borderBottom;
        private FrameworkElement _borderBottomLeft;
        private FrameworkElement _tittleBar;
        private Button _minimizeButton;
        private Button _maximizeButton;
        private Button _closeButton;

        private enum WindowBorderEdge
        {
            Left,
            TopLeft,
            Top,
            TopRight,
            Right,
            BottomRight,
            Bottom,
            BottomLeft
        }

        public CustomWindow()
        {
            SourceInitialized += (sender, e) =>
            {
                IntPtr handle = new WindowInteropHelper(this).Handle;
                var hwndSource = HwndSource.FromHwnd(handle);
                if (hwndSource != null)
                    hwndSource.AddHook(WndProc);
            };
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomWindow),new FrameworkPropertyMetadata(typeof(CustomWindow)));

        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            RegisterTittleBar();
            RegisterBorders();
            RegisterMinimizeButton();
            RegisterMaximizeButton();
            RegisterCloseButton();
        }

        private void RegisterTittleBar()
        {
            _tittleBar = (FrameworkElement)GetTemplateChild("tittleBar");
            if (_tittleBar != null)
                _tittleBar.MouseLeftButtonDown += (sender, e) =>
                {
                    if (e.ClickCount == 2 && ResizeMode == ResizeMode.CanResize)
                    {
                        Application.Current.MainWindow.WindowState = Application.Current.MainWindow.WindowState ==
                                                                     WindowState.Normal
                            ? WindowState.Maximized
                            : WindowState.Normal;
                    }
                    Application.Current.MainWindow.DragMove();

                };
        }


        private void RegisterCloseButton()
        {
            _closeButton = (Button)GetTemplateChild("PART_WindowCaptionCloseButton");

            if (_closeButton != null)
            {
                _closeButton.Click += (sender, e) => Close();
            }
        }

        private void RegisterMaximizeButton()
        {
            _maximizeButton = (Button)GetTemplateChild("PART_WindowCaptionMaximizeButton");

            if (_maximizeButton != null)
            {
                _maximizeButton.Click += (sender, e) =>
                {
                    WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
                };
            }
        }

        private void RegisterMinimizeButton()
        {
            _minimizeButton = (Button)GetTemplateChild("PART_WindowCaptionMinimizeButton");

            if (_minimizeButton != null)
            {
                _minimizeButton.Click += (sender, e) => WindowState = WindowState.Minimized;
            }
        }

        private void RegisterBorderEvents(WindowBorderEdge borderEdge, FrameworkElement border)
        {

            border.MouseLeftButtonDown += (sender, e) =>
            {
                if (WindowState != WindowState.Maximized && ResizeMode == ResizeMode.CanResize)
                {
                    Point cursorLocation = e.GetPosition(this);
                    var cursorOffset = new Point();

                    switch (borderEdge)
                    {
                        case WindowBorderEdge.Left:
                            cursorOffset.X = cursorLocation.X;
                            break;
                        case WindowBorderEdge.TopLeft:
                            cursorOffset.X = cursorLocation.X;
                            cursorOffset.Y = cursorLocation.Y;
                            break;
                        case WindowBorderEdge.Top:
                            cursorOffset.Y = cursorLocation.Y;
                            break;
                        case WindowBorderEdge.TopRight:
                            cursorOffset.X = (Width - cursorLocation.X);
                            cursorOffset.Y = cursorLocation.Y;
                            break;
                        case WindowBorderEdge.Right:
                            cursorOffset.X = (Width - cursorLocation.X);
                            break;
                        case WindowBorderEdge.BottomRight:
                            cursorOffset.X = (Width - cursorLocation.X);
                            cursorOffset.Y = (Height - cursorLocation.Y);
                            break;
                        case WindowBorderEdge.Bottom:
                            cursorOffset.Y = (Height - cursorLocation.Y);
                            break;
                        case WindowBorderEdge.BottomLeft:
                            cursorOffset.X = cursorLocation.X;
                            cursorOffset.Y = (Height - cursorLocation.Y);
                            break;
                    }

                    _cursorOffset = cursorOffset;

                    border.CaptureMouse();
                }
            };

            border.MouseMove += (sender, e) =>
            {
                if (WindowState != WindowState.Maximized && border.IsMouseCaptured && ResizeMode == ResizeMode.CanResize)
                {
                    Point cursorLocation = e.GetPosition(this);

                    double nHorizontalChange = (cursorLocation.X - _cursorOffset.X);
                    double pHorizontalChange = (cursorLocation.X + _cursorOffset.X);
                    double nVerticalChange = (cursorLocation.Y - _cursorOffset.Y);
                    double pVerticalChange = (cursorLocation.Y + _cursorOffset.Y);

                    switch (borderEdge)
                    {
                        case WindowBorderEdge.Left:
                            if (Width - nHorizontalChange <= MinWidth)
                                break;
                            Left += nHorizontalChange;
                            Width -= nHorizontalChange;
                            break;
                        case WindowBorderEdge.TopLeft:
                            if (Width - nHorizontalChange <= MinWidth)
                                break;
                            Left += nHorizontalChange;
                            Width -= nHorizontalChange;
                            if (Height - nVerticalChange <= MinHeight)
                                break;
                            Top += nVerticalChange;
                            Height -= nVerticalChange;
                            break;
                        case WindowBorderEdge.Top:
                            if (Height - nVerticalChange <= MinHeight)
                                break;
                            Top += nVerticalChange;
                            Height -= nVerticalChange;
                            break;
                        case WindowBorderEdge.TopRight:
                            if (pHorizontalChange <= MinWidth)
                                break;
                            Width = pHorizontalChange;
                            if (Height - nVerticalChange <= MinHeight)
                                break;
                            Top += nVerticalChange;
                            Height -= nVerticalChange;
                            break;
                        case WindowBorderEdge.Right:
                            if (pHorizontalChange <= MinWidth)
                                break;
                            Width = pHorizontalChange;
                            break;
                        case WindowBorderEdge.BottomRight:
                            if (pHorizontalChange <= MinWidth)
                                break;
                            Width = pHorizontalChange;
                            if (pVerticalChange <= MinHeight)
                                break;
                            Height = pVerticalChange;
                            break;
                        case WindowBorderEdge.Bottom:
                            if (pVerticalChange <= MinHeight)
                                break;
                            Height = pVerticalChange;
                            break;
                        case WindowBorderEdge.BottomLeft:
                            if (Width - nHorizontalChange <= MinWidth)
                                break;
                            Left += nHorizontalChange;
                            Width -= nHorizontalChange;
                            if (pVerticalChange <= MinHeight)
                                break;
                            Height = pVerticalChange;
                            break;
                    }
                }
            };

            border.MouseLeftButtonUp += (sender, e) => border.ReleaseMouseCapture();
        }

        private void RegisterBorders()
        {
            _borderLeft = (FrameworkElement)GetTemplateChild("PART_WindowBorderLeft");
            _borderTopLeft = (FrameworkElement)GetTemplateChild("PART_WindowBorderTopLeft");
            _borderTop = (FrameworkElement)GetTemplateChild("PART_WindowBorderTop");
            _borderTopRight = (FrameworkElement)GetTemplateChild("PART_WindowBorderTopRight");
            _borderRight = (FrameworkElement)GetTemplateChild("PART_WindowBorderRight");
            _borderBottomRight = (FrameworkElement)GetTemplateChild("PART_WindowBorderBottomRight");
            _borderBottom = (FrameworkElement)GetTemplateChild("PART_WindowBorderBottom");
            _borderBottomLeft = (FrameworkElement)GetTemplateChild("PART_WindowBorderBottomLeft");

            RegisterBorderEvents(WindowBorderEdge.Left, _borderLeft);
            RegisterBorderEvents(WindowBorderEdge.TopLeft, _borderTopLeft);
            RegisterBorderEvents(WindowBorderEdge.Top, _borderTop);
            RegisterBorderEvents(WindowBorderEdge.TopRight, _borderTopRight);
            RegisterBorderEvents(WindowBorderEdge.Right, _borderRight);
            RegisterBorderEvents(WindowBorderEdge.BottomRight, _borderBottomRight);
            RegisterBorderEvents(WindowBorderEdge.Bottom, _borderBottom);
            RegisterBorderEvents(WindowBorderEdge.BottomLeft, _borderBottomLeft);
        }

        private void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            var mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            const int monitorDefaulttonearest = 0x00000002;
            IntPtr monitor = NativeMethods.MonitorFromWindow(hwnd, monitorDefaulttonearest);

            if (monitor != IntPtr.Zero)
            {
                var monitorInfo = new MONITORINFO();
                NativeMethods.GetMonitorInfo(monitor, monitorInfo);

                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;

                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return IntPtr.Zero;
        }
    }

}