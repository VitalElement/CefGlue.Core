using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace CefGlue.Avalonia
{
    public class RegisterCustomSchemesEventArgs : EventArgs
    {
        public RegisterCustomSchemesEventArgs(CefSchemeRegistrar register)
        {
            Registrar = register;
        }

        public CefSchemeRegistrar Registrar { get; private set; }
    }

    public delegate void RegisterCustomSchemesHandler(object sender, RegisterCustomSchemesEventArgs e);
}
