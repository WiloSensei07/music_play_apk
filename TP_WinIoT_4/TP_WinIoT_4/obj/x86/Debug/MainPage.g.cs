﻿#pragma checksum "D:\3iL3\Dev & Win IoT\TP_WinIoT_4\TP_WinIoT_4\TP_WinIoT_4\MainPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DBE2DD5F58231CB6F22A144AEA538C73F12F5560B68A5506CDFF6D6E00B38939"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TP_WinIoT_4
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MainPage.xaml line 21
                {
                    global::Windows.UI.Xaml.Controls.Button element2 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element2).Click += this.Button_Click;
                }
                break;
            case 3: // MainPage.xaml line 34
                {
                    this.volumeslider = (global::Windows.UI.Xaml.Controls.Slider)(target);
                }
                break;
            case 4: // MainPage.xaml line 29
                {
                    this.pause = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 5: // MainPage.xaml line 30
                {
                    this.play = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 6: // MainPage.xaml line 31
                {
                    this.stop = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 7: // MainPage.xaml line 32
                {
                    this.info = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 8: // MainPage.xaml line 23
                {
                    this.listmp3 = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                }
                break;
            case 9: // MainPage.xaml line 25
                {
                    this.file = (global::Windows.UI.Xaml.Controls.Image)(target);
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

