﻿#pragma checksum "C:\Users\ngoc\Source\Repos\DoAn1\DoAn1\UpdateUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0DD496609FB5177ACA17839AAAF74B6F"
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
    partial class UpdateUserControl : 
        global::Windows.UI.Xaml.Controls.UserControl, 
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
            case 2: // UpdateUserControl.xaml line 27
                {
                    this.Back = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Back).Click += this.Back_Click;
                }
                break;
            case 3: // UpdateUserControl.xaml line 31
                {
                    this.addTen_Truyen = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4: // UpdateUserControl.xaml line 33
                {
                    this.addNoiDung = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5: // UpdateUserControl.xaml line 35
                {
                    this.addGia = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6: // UpdateUserControl.xaml line 37
                {
                    this.addTacGia = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7: // UpdateUserControl.xaml line 40
                {
                    this.btnDone = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnDone).Click += this.btnDone_Click;
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

