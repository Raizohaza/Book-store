﻿#pragma checksum "C:\Users\ngoc\source\repos\DoAn1\DoAn1\UpdateUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D0A54F77B5D992419BFACC1C336FF57D"
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
            case 2: // UpdateUserControl.xaml line 28
                {
                    this.Back = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Back).Click += this.Back_Click;
                }
                break;
            case 3: // UpdateUserControl.xaml line 32
                {
                    this.addTen_Truyen = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4: // UpdateUserControl.xaml line 34
                {
                    this.addNoiDung = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5: // UpdateUserControl.xaml line 36
                {
                    this.addGia = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6: // UpdateUserControl.xaml line 38
                {
                    this.addTacGia = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7: // UpdateUserControl.xaml line 41
                {
                    this.btnDone = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnDone).Click += this.btnDone_Click;
                }
                break;
            case 8: // UpdateUserControl.xaml line 43
                {
                    this.btnAddImg = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnAddImg).Click += this.btnAddImg_Click;
                }
                break;
            case 9: // UpdateUserControl.xaml line 47
                {
                    this.addSoLuong = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 10: // UpdateUserControl.xaml line 49
                {
                    this.lvAddImg = (global::Windows.UI.Xaml.Controls.ListView)(target);
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

