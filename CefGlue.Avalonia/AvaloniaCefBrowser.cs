using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Avalonia;
using Xilium.CefGlue;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Avalonia.Input;
using Avalonia.VisualTree;

namespace CefGlue.Avalonia
{
    class AvaloniaCefBrowser : ContentControl
    {
        private bool _disposed;
        private bool _created;

        private Image _browserPageImage;

        private WritableBitmap _browserPageBitmap;

        private int _browserWidth;
        private int _browserHeight;
        private bool _browserSizeChanged;

        private CefBrowser _browser;
        private CefBrowserHost _browserHost;
        private WpfCefClient _cefClient;

        private ToolTip _tooltip;
        private DispatcherTimer _tooltipTimer;

        public string StartUrl { get; set; }
        public bool AllowsTransparency { get; set; }
        public Key Keys { get; private set; }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var size = base.ArrangeOverride(arrangeBounds);

            if (_browserPageImage != null)
            {
                var newWidth = (int)size.Width;
                var newHeight = (int)size.Height;

                if (newWidth > 0 && newHeight > 0)
                {
                    if (!_created)
                    {
                        AttachEventHandlers(this); // TODO: ?

                        // Create the bitmap that holds the rendered website bitmap
                        _browserWidth = newWidth;
                        _browserHeight = newHeight;
                        _browserSizeChanged = true;

                        // Find the window that's hosting us                        
                        Window parentWnd = this.GetVisualRoot() as Window;

                        if (parentWnd != null)
                        {

                            IntPtr hParentWnd = parentWnd.PlatformImpl.Handle.Handle;

                            var windowInfo = CefWindowInfo.Create();
                            windowInfo.SetAsWindowless(hParentWnd, AllowsTransparency);

                            var settings = new CefBrowserSettings();
                            _cefClient = new WpfCefClient(this);

                            // This is the first time the window is being rendered, so create it.
                            CefBrowserHost.CreateBrowser(windowInfo, _cefClient, settings, !string.IsNullOrEmpty(StartUrl) ? StartUrl : "about:blank");

                            _created = true;
                        }
                    }
                    else
                    {
                        // Only update the bitmap if the size has changed
                        if (_browserPageBitmap == null || (_browserPageBitmap.PixelWidth != newWidth || _browserPageBitmap.PixelHeight != newHeight))
                        {
                            _browserWidth = newWidth;
                            _browserHeight = newHeight;
                            _browserSizeChanged = true;

                            // If the window has already been created, just resize it
                            if (_browserHost != null)
                            {
                                _browserHost.WasResized();
                            }
                        }
                    }
                }
            }

            return size;

        }

        private static CefEventFlags GetMouseModifiers()
        {
            CefEventFlags modifiers = new CefEventFlags();

            //if (Mouse.LeftButton == MouseButtonState.Pressed)
            //    modifiers |= CefEventFlags.LeftMouseButton;

            //if (Mouse.MiddleButton == MouseButtonState.Pressed)
            //    modifiers |= CefEventFlags.MiddleMouseButton;

            //if (Mouse.RightButton == MouseButtonState.Pressed)
            //    modifiers |= CefEventFlags.RightMouseButton;

            return modifiers;
        }

        private void AttachEventHandlers(AvaloniaCefBrowser browser)
        {
            browser.GotFocus += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        _browserHost.SendFocusEvent(true);
                    }
                }
                catch (Exception ex)
                {

                }
            };

            browser.LostFocus += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        _browserHost.SendFocusEvent(false);
                    }
                }
                catch (Exception ex)
                {

                }
            };

            browser.PointerLeave += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = 0,
                            Y = 0
                        };

                        mouseEvent.Modifiers = GetMouseModifiers();

                        _browserHost.SendMouseMoveEvent(mouseEvent, true);
                        //_logger.Debug("Browser_MouseLeave");
                    }
                }
                catch (Exception ex)
                {

                }
            };

            browser.PointerMoved += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        Point cursorPos = arg.GetPosition(this);

                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = (int)cursorPos.X,
                            Y = (int)cursorPos.Y
                        };

                        mouseEvent.Modifiers = GetMouseModifiers();

                        _browserHost.SendMouseMoveEvent(mouseEvent, false);

                        //_logger.Debug(string.Format("Browser_MouseMove: ({0},{1})", cursorPos.X, cursorPos.Y));
                    }
                }
                catch (Exception ex)
                {

                }
            };

            browser.PointerPressed += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        Focus();

                        Point cursorPos = arg.GetPosition(this);

                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = (int)cursorPos.X,
                            Y = (int)cursorPos.Y,
                        };

                        mouseEvent.Modifiers = GetMouseModifiers();

                        //if (arg.ChangedButton == MouseButton.Left)
                        //    _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Left, false, arg.ClickCount);
                        //else if (arg.ChangedButton == MouseButton.Middle)
                        //    _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Middle, false, arg.ClickCount);
                        //else if (arg.ChangedButton == MouseButton.Right)
                        //    _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Right, false, arg.ClickCount);

                        //_logger.Debug(string.Format("Browser_MouseDown: ({0},{1})", cursorPos.X, cursorPos.Y));
                    }
                }
                catch (Exception ex)
                {

                }
            };

            browser.PointerReleased += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        Point cursorPos = arg.GetPosition(this);

                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = (int)cursorPos.X,
                            Y = (int)cursorPos.Y,
                        };

                        mouseEvent.Modifiers = GetMouseModifiers();

                        //if (arg.ChangedButton == MouseButton.Left)
                        //    _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Left, true, arg.ClickCount);
                        //else if (arg.ChangedButton == MouseButton.Middle)
                        //    _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Middle, true, arg.ClickCount);
                        //else if (arg.ChangedButton == MouseButton.Right)
                        //    _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Right, true, arg.ClickCount);

                        //_logger.Debug(string.Format("Browser_MouseUp: ({0},{1})", cursorPos.X, cursorPos.Y));
                    }
                }
                catch (Exception ex)
                {
                    //_logger.ErrorException("WpfCefBrowser: Caught exception in MouseUp()", ex);
                }
            };

            browser.PointerWheelChanged += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        Point cursorPos = arg.GetPosition(this);

                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = (int)cursorPos.X,
                            Y = (int)cursorPos.Y,
                        };

                        _browserHost.SendMouseWheelEvent(mouseEvent, 0, (int)arg.Delta.Y);
                    }
                }
                catch (Exception ex)
                {
                    //_logger.ErrorException("WpfCefBrowser: Caught exception in MouseWheel()", ex);
                }
            };

            // TODO: require more intelligent processing
            browser.TextInput += (sender, arg) =>
            {
                if (_browserHost != null)
                {
                    foreach (var c in arg.Text)
                    {
                        CefKeyEvent keyEvent = new CefKeyEvent()
                        {
                            EventType = CefKeyEventType.Char,
                            WindowsKeyCode = (int)c,
                            // Character = c,
                        };

                        //keyEvent.Modifiers = GetKeyboardModifiers();

                        _browserHost.SendKeyEvent(keyEvent);
                    }
                }

                arg.Handled = true;
            };

            // TODO: require more intelligent processing
            browser.KeyDown += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        //_logger.Debug(string.Format("KeyDown: system key {0}, key {1}", arg.SystemKey, arg.Key));
                        CefKeyEvent keyEvent = new CefKeyEvent()
                        {
                            EventType = CefKeyEventType.RawKeyDown,
                            // WindowsKeyCode = KeyInterop.VirtualKeyFromKey(arg.Key == Key.System ? arg.SystemKey : arg.Key),
                            NativeKeyCode = 0,
                            IsSystemKey = arg.Key == Key.System,
                        };

                        //keyEvent.Modifiers = GetKeyboardModifiers();

                        _browserHost.SendKeyEvent(keyEvent);
                    }
                }
                catch (Exception ex)
                {
                    //_logger.ErrorException("WpfCefBrowser: Caught exception in PreviewKeyDown()", ex);
                }

                //arg.Handled = HandledKeys.Contains(arg.Key);
            };

            // TODO: require more intelligent processing
            browser.KeyUp += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        //_logger.Debug(string.Format("KeyUp: system key {0}, key {1}", arg.SystemKey, arg.Key));
                        CefKeyEvent keyEvent = new CefKeyEvent()
                        {
                            EventType = CefKeyEventType.KeyUp,
                            //WindowsKeyCode = KeyInterop.VirtualKeyFromKey(arg.Key == Key.System ? arg.SystemKey : arg.Key),
                            NativeKeyCode = 0,
                            //IsSystemKey = arg.Key == Key.System,
                        };

                        // keyEvent.Modifiers = GetKeyboardModifiers();

                        _browserHost.SendKeyEvent(keyEvent);
                    }
                }
                catch (Exception ex)
                {
                    //_logger.ErrorException("WpfCefBrowser: Caught exception in PreviewKeyUp()", ex);
                }

                arg.Handled = true;
            };

            /*browser._popup.MouseMove += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        Point cursorPos = arg.GetPosition(this);

                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = (int)cursorPos.X,
                            Y = (int)cursorPos.Y
                        };

                        mouseEvent.Modifiers = GetMouseModifiers();

                        _browserHost.SendMouseMoveEvent(mouseEvent, false);

                        //_logger.Debug(string.Format("Popup_MouseMove: ({0},{1})", cursorPos.X, cursorPos.Y));
                    }
                }
                catch (Exception ex)
                {
                    _logger.ErrorException("WpfCefBrowser: Caught exception in Popup.MouseMove()", ex);
                }
            };

            browser._popup.MouseDown += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        Point cursorPos = arg.GetPosition(this);

                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = (int)cursorPos.X,
                            Y = (int)cursorPos.Y
                        };

                        mouseEvent.Modifiers = GetMouseModifiers();

                        _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Left, true, 1);

                        //_logger.Debug(string.Format("Popup_MouseDown: ({0},{1})", cursorPos.X, cursorPos.Y));
                    }
                }
                catch (Exception ex)
                {
                    _logger.ErrorException("WpfCefBrowser: Caught exception in Popup.MouseDown()", ex);
                }
            };

            browser._popup.MouseWheel += (sender, arg) =>
            {
                try
                {
                    if (_browserHost != null)
                    {
                        Point cursorPos = arg.GetPosition(this);
                        int delta = arg.Delta;
                        CefMouseEvent mouseEvent = new CefMouseEvent()
                        {
                            X = (int)cursorPos.X,
                            Y = (int)cursorPos.Y
                        };

                        mouseEvent.Modifiers = GetMouseModifiers();
                        _browserHost.SendMouseWheelEvent(mouseEvent, 0, delta);

                        //_logger.Debug(string.Format("MouseWheel: ({0},{1})", cursorPos.X, cursorPos.Y));
                    }
                }
                catch (Exception ex)
                {
                    _logger.ErrorException("WpfCefBrowser: Caught exception in Popup.MouseWheel()", ex);
                }
            };*/
        }

        internal bool OnTooltip(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                _tooltipTimer.Stop();
                UpdateTooltip(null);
            }
            else
            {
                _tooltipTimer.Tick += (sender, args) => UpdateTooltip(text);
                _tooltipTimer.Start();
            }

            return true;
        }

        private void UpdateTooltip(string text)
        {
            Dispatcher.UIThread.InvokeAsync(
                    () =>
                    {
                        if (string.IsNullOrEmpty(text))
                        {
                            //_tooltip.IsOpen = false;
                        }
                        else
                        {
                            //_tooltip.Placement = PlacementMode.Mouse;
                            _tooltip.Content = text;
                            //_tooltip.IsOpen = true;
                            //_tooltip.Visibility = Visibility.Visible;
                        }
                    });

            _tooltipTimer.Stop();
        }

        public void HandleAfterCreated(CefBrowser browser)
        {
            int width = 0, height = 0;

            bool hasAlreadyBeenInitialized = false;

            Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (_browser != null)
                {
                    hasAlreadyBeenInitialized = true;
                }
                else
                {
                    _browser = browser;
                    _browserHost = _browser.GetHost();
                    // _browserHost.SetFocus(IsFocused);

                    width = (int)_browserWidth;
                    height = (int)_browserHeight;
                }
            });

            // Make sure we don't initialize ourselves more than once. That seems to break things.
            if (hasAlreadyBeenInitialized)
                return;

            if (width > 0 && height > 0)
                _browserHost.WasResized();

            // 			mainUiDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            // 			{
            // 				if (!string.IsNullOrEmpty(this.initialUrl))
            // 				{
            // 					NavigateTo(this.initialUrl);
            // 					this.initialUrl = string.Empty;
            // 				}
            // 			}));
        }

        internal bool GetViewRect(ref CefRectangle rect)
        {
            bool rectProvided = false;
            CefRectangle browserRect = new CefRectangle();

            // TODO: simplify this
            //_mainUiDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            //{
            try
            {
                // The simulated screen and view rectangle are the same. This is necessary
                // for popup menus to be located and sized inside the view.
                browserRect.X = browserRect.Y = 0;
                browserRect.Width = (int)_browserWidth;
                browserRect.Height = (int)_browserHeight;

                rectProvided = true;
            }
            catch (Exception ex)
            {                
                rectProvided = false;
            }
            //}));

            if (rectProvided)
            {
                rect = browserRect;
            }            

            return rectProvided;
        }

        internal void GetScreenPoint(int viewX, int viewY, ref int screenX, ref int screenY)
        {
            Point ptScreen = new Point();

            Dispatcher.UIThread.InvokeAsync(()=>
            {
                try
                {
                    Point ptView = new Point(viewX, viewY);
                    ptScreen = this.PointToScreen(ptView);
                }
                catch (Exception ex)
                {
                    
                }
            }));

            screenX = (int)ptScreen.X;
            screenY = (int)ptScreen.Y;
        }

        internal void HandleViewPaint(CefBrowser browser, CefPaintElementType type, CefRectangle[] dirtyRects, IntPtr buffer, int width, int height)
        {
            // When browser size changed - we just skip frame updating.
            // This is dirty precheck to do not do Invoke whenever is possible.
            if (_browserSizeChanged && (width != _browserWidth || height != _browserHeight)) return;

            Dispatcher.UIThread.InvokeAsync(()=>
            {
                // Actual browser size changed check.
                if (_browserSizeChanged && (width != _browserWidth || height != _browserHeight)) return;

                try
                {
                    if (_browserSizeChanged)
                    {
                        _browserPageBitmap = new WriteableBitmap((int)_browserWidth, (int)_browserHeight, 96, 96, AllowsTransparency ? PixelFormats.Bgra32 : PixelFormats.Bgr32, null);
                        _browserPageImage.Source = _browserPageBitmap;

                        _browserSizeChanged = false;
                    }

                    if (_browserPageBitmap != null)
                    {
                        DoRenderBrowser(_browserPageBitmap, width, height, dirtyRects, buffer);
                    }

                }
                catch (Exception ex)
                {                    
                }
            });
        }

        internal void HandlePopupPaint(int width, int height, CefRectangle[] dirtyRects, IntPtr sourceBuffer)
        {
            if (width == 0 || height == 0)
            {
                return;
            }

            Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        int stride = width * 4;
                        int sourceBufferSize = stride * height;                        

                        foreach (CefRectangle dirtyRect in dirtyRects)
                        {
                            if (dirtyRect.Width == 0 || dirtyRect.Height == 0)
                            {
                                continue;
                            }

                            int adjustedWidth = dirtyRect.Width;

                            int adjustedHeight = dirtyRect.Height;

                            Int32Rect sourceRect = new Int32Rect(dirtyRect.X, dirtyRect.Y, adjustedWidth, adjustedHeight);

                            _popupImageBitmap.WritePixels(sourceRect, sourceBuffer, sourceBufferSize, stride, dirtyRect.X, dirtyRect.Y);
                        }
                    }));
        }

        private void DoRenderBrowser(WriteableBitmap bitmap, int browserWidth, int browserHeight, CefRectangle[] dirtyRects, IntPtr sourceBuffer)
        {
            int stride = browserWidth * 4;
            int sourceBufferSize = stride * browserHeight;            

            if (browserWidth == 0 || browserHeight == 0)
            {
                return;
            }

            foreach (CefRectangle dirtyRect in dirtyRects)
            {
                if (dirtyRect.Width == 0 || dirtyRect.Height == 0)
                {
                    continue;
                }

                // If the window has been resized, make sure we never try to render too much
                int adjustedWidth = (int)dirtyRect.Width;
                //if (dirtyRect.X + dirtyRect.Width > (int) bitmap.Width)
                //{
                //    adjustedWidth = (int) bitmap.Width - (int) dirtyRect.X;
                //}

                int adjustedHeight = (int)dirtyRect.Height;
                //if (dirtyRect.Y + dirtyRect.Height > (int) bitmap.Height)
                //{
                //    adjustedHeight = (int) bitmap.Height - (int) dirtyRect.Y;
                //}

                // Update the dirty region
                Int32Rect sourceRect = new Int32Rect((int)dirtyRect.X, (int)dirtyRect.Y, adjustedWidth, adjustedHeight);
                bitmap.WritePixels(sourceRect, sourceBuffer, sourceBufferSize, stride, (int)dirtyRect.X, (int)dirtyRect.Y);

                // 			int adjustedWidth = browserWidth;
                // 			if (browserWidth > (int)bitmap.Width)
                // 				adjustedWidth = (int)bitmap.Width;
                // 
                // 			int adjustedHeight = browserHeight;
                // 			if (browserHeight > (int)bitmap.Height)
                // 				adjustedHeight = (int)bitmap.Height;
                // 
                // 			int sourceBufferSize = browserWidth * browserHeight * 4;
                // 			int stride = browserWidth * 4;
                // 
                // 			Int32Rect sourceRect = new Int32Rect(0, 0, adjustedWidth, adjustedHeight);
                // 			bitmap.WritePixels(sourceRect, sourceBuffer, sourceBufferSize, stride, 0, 0);
            }
        }

        internal void OnPopupShow(bool show)
        {
            if (_popup == null)
            {
                return;
            }

            Dispatcher.UIThread.InvokeAsync(()=>_popup.IsOpen = show);
        }


    }
}
