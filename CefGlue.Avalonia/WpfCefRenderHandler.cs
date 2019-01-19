using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace CefGlue.Avalonia
{
    internal sealed class WpfCefRenderHandler : CefRenderHandler
    {
        private readonly AvaloniaCefBrowser _owner;        
        private readonly IUiHelper _uiHelper;

        public WpfCefRenderHandler(AvaloniaCefBrowser owner, IUiHelper uiHelper)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }

            if (uiHelper == null)
            {
                throw new ArgumentNullException("uiHelper");
            }

            _owner = owner;            
            _uiHelper = uiHelper;
        }


        protected override void GetViewRect(CefBrowser browser, out CefRectangle rect)
        {
            rect = new CefRectangle();
            _owner.GetViewRect(ref rect);
        }

        protected override bool GetScreenPoint(CefBrowser browser, int viewX, int viewY, ref int screenX, ref int screenY)
        {
            _owner.GetScreenPoint(viewX, viewY, ref screenX, ref screenY);
            return true;
        }

        protected override bool GetScreenInfo(CefBrowser browser, CefScreenInfo screenInfo)
        {
            return false;
        }

        protected override void OnPopupShow(CefBrowser browser, bool show)
        {
            _owner.OnPopupShow(show);
        }

        protected override void OnPopupSize(CefBrowser browser, CefRectangle rect)
        {
           // _owner.OnPopupSize(rect);
        }

        protected override void OnPaint(CefBrowser browser, CefPaintElementType type, CefRectangle[] dirtyRects, IntPtr buffer, int width, int height)
        {               
            if (type == CefPaintElementType.View)
            {
                _owner.HandleViewPaint(browser, type, dirtyRects, buffer, width, height);
            }
            else if (type == CefPaintElementType.Popup)
            {
                _owner.HandlePopupPaint(width, height, dirtyRects, buffer);
            }
        }

        protected override void OnCursorChange(CefBrowser browser, IntPtr cursorHandle, CefCursorType type, CefCursorInfo customCursorInfo)
        {
            _uiHelper.PerformInUiThread(() =>
            {
                //Cursor cursor = CursorInteropHelper.Create(new SafeFileHandle(cursorHandle, false));
                //_owner.Cursor = cursor;
            });
        }

        protected override void OnScrollOffsetChanged(CefBrowser browser, double x, double y)
        {
        }

        protected override void OnImeCompositionRangeChanged(CefBrowser browser, CefRange selectedRange, CefRectangle[] characterBounds)
        {
        }

        protected override CefAccessibilityHandler GetAccessibilityHandler()
        {
            return new AvaloniaCefAccessibilityHandler();
        }

        protected override void OnAcceleratedPaint(CefBrowser browser, CefPaintElementType type, CefRectangle[] dirtyRects, IntPtr sharedHandle)
        {
        }
    }

}
