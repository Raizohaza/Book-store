﻿#pragma checksum "C:\Users\Admin\Desktop\DoAn1\DoAn1\PageHome.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "19D9614EA7353DEF96181BB1A319B06A"
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
    partial class PageHome : 
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
            case 2: // PageHome.xaml line 17
                {
                    this.GridHome = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // PageHome.xaml line 38
                {
                    this.test_data = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.test_data).RightTapped += this.test_data_RightTapped;
                    ((global::Windows.UI.Xaml.Controls.ListView)this.test_data).SelectionChanged += this.test_data_SelectionChanged;
                }
                break;
            case 4: // PageHome.xaml line 46
                {
                    this.allContactsMenuFlyout = (global::Windows.UI.Xaml.Controls.MenuFlyout)(target);
                }
                break;
            case 5: // PageHome.xaml line 47
                {
                    this.Edit = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.Edit).Click += this.Edit_Click;
                }
                break;
            case 6: // PageHome.xaml line 48
                {
                    this.Remove = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.Remove).Click += this.Remove_Click;
                }
                break;
            case 8: // PageHome.xaml line 20
                {
                    this.cbbListType = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.cbbListType).RightTapped += this.cbbListType_RightTapped;
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.cbbListType).SelectionChanged += this.cbbListType_SelectionChanged;
                }
                break;
            case 9: // PageHome.xaml line 36
                {
                    this.addCat = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.addCat).Click += this.addCat_Click;
                }
                break;
            case 10: // PageHome.xaml line 22
                {
                    this.cbbCF = (global::Windows.UI.Xaml.Controls.MenuFlyout)(target);
                }
                break;
            case 11: // PageHome.xaml line 24
                {
                    this.cbbEdit = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.cbbEdit).Click += this.cbbEdit_Click;
                }
                break;
            case 12: // PageHome.xaml line 25
                {
                    this.cbbRemove = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.cbbRemove).Click += this.cbbRemove_Click;
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

