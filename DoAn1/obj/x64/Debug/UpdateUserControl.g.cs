﻿#pragma checksum "C:\Users\Admin\Desktop\DoAn1\DoAn1\UpdateUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C75D115B0448D6334032718129756C85"
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
            case 2: // UpdateUserControl.xaml line 36
                {
                    this.addTen_Truyen = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 3: // UpdateUserControl.xaml line 38
                {
                    this.addNoiDung = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4: // UpdateUserControl.xaml line 40
                {
                    this.addGia = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5: // UpdateUserControl.xaml line 42
                {
                    this.addTacGia = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6: // UpdateUserControl.xaml line 44
                {
                    this.cbbListType = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.cbbListType).SelectionChanged += this.cbbListType_SelectionChanged;
                }
                break;
            case 7: // UpdateUserControl.xaml line 52
                {
                    this.btnDone = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnDone).Click += this.btnDone_Click;
                }
                break;
            case 8: // UpdateUserControl.xaml line 56
                {
                    this.addSoLuong = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 9: // UpdateUserControl.xaml line 57
                {
                    this.btnAddImg = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnAddImg).Click += this.btnAddImg_Click;
                }
                break;
            case 10: // UpdateUserControl.xaml line 58
                {
                    this.avatarImg = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 11: // UpdateUserControl.xaml line 60
                {
                    this.btnAddProducImgs = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnAddProducImgs).Click += this.btnAddProducImgs_Click;
                }
                break;
            case 12: // UpdateUserControl.xaml line 62
                {
                    this.lvManyImg = (global::Windows.UI.Xaml.Controls.ListView)(target);
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

