﻿#pragma checksum "C:\Users\Admin\Desktop\DoAn1\DoAn1\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2F38A8B4599767CC76EE24269FB32A77"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAn1
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // MainPage.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                }
                break;
            case 2: // MainPage.xaml line 19
                {
                    this.Menu = (global::Windows.UI.Xaml.Controls.NavigationView)(target);
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.Menu).SelectionChanged += this.Menu_SelectionChanged;
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.Menu).Loaded += this.Menu_Loaded;
                }
                break;
            case 3: // MainPage.xaml line 28
                {
                    this.NavViewSearchBox = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target);
                }
                break;
            case 4: // MainPage.xaml line 31
                {
                    this.CF = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

