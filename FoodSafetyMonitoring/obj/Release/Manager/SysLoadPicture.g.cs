﻿#pragma checksum "..\..\..\Manager\SysLoadPicture.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B5AE2E6D5356B4EEA16177EBC42F54B1"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using FoodSafetyMonitoring.Manager.UserControls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace FoodSafetyMonitoring.Manager {
    
    
    /// <summary>
    /// SysLoadPicture
    /// </summary>
    public partial class SysLoadPicture : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 38 "..\..\..\Manager\SysLoadPicture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox _title;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Manager\SysLoadPicture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button _btnSave;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Manager\SysLoadPicture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FileNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Manager\SysLoadPicture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button _browse;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Manager\SysLoadPicture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button _update;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Manager\SysLoadPicture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image1;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ZRDDcsSystem;component/manager/sysloadpicture.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Manager\SysLoadPicture.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this._title = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this._btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\Manager\SysLoadPicture.xaml"
            this._btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.FileNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this._browse = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\Manager\SysLoadPicture.xaml"
            this._browse.Click += new System.Windows.RoutedEventHandler(this.btBrowse_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this._update = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\Manager\SysLoadPicture.xaml"
            this._update.Click += new System.Windows.RoutedEventHandler(this.btUpdate_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.image1 = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
