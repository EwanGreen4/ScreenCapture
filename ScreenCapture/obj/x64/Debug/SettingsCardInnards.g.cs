﻿#pragma checksum "C:\Users\lezlo\Source\Repos\ScreenCapture\ScreenCapture\SettingsCardInnards.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "189AC199F11EB47E95A00FE46741B0AE141BEA986D7540847C6A570C47B8E23A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ScreenCapture
{
    partial class SettingsCardInnards : 
        global::Windows.UI.Xaml.Controls.UserControl, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_ContentControl_Content(global::Windows.UI.Xaml.Controls.ContentControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.Content = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_ContentControl_ContentTemplate(global::Windows.UI.Xaml.Controls.ContentControl obj, global::Windows.UI.Xaml.DataTemplate value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::Windows.UI.Xaml.DataTemplate) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Xaml.DataTemplate), targetNullValue);
                }
                obj.ContentTemplate = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_ContentControl_ContentTemplateSelector(global::Windows.UI.Xaml.Controls.ContentControl obj, global::Windows.UI.Xaml.Controls.DataTemplateSelector value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::Windows.UI.Xaml.Controls.DataTemplateSelector) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Xaml.Controls.DataTemplateSelector), targetNullValue);
                }
                obj.ContentTemplateSelector = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_FontIcon_Glyph(global::Windows.UI.Xaml.Controls.FontIcon obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Glyph = value ?? global::System.String.Empty;
            }
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class SettingsCardInnards_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            ISettingsCardInnards_Bindings
        {
            private global::ScreenCapture.SettingsCardInnards dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ContentControl obj2;
            private global::Windows.UI.Xaml.Controls.FontIcon obj3;
            private global::Windows.UI.Xaml.Controls.TextBlock obj4;
            private global::Windows.UI.Xaml.Controls.TextBlock obj5;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj2ContentDisabled = false;
            private static bool isobj2ContentTemplateDisabled = false;
            private static bool isobj2ContentTemplateSelectorDisabled = false;
            private static bool isobj3GlyphDisabled = false;
            private static bool isobj4TextDisabled = false;
            private static bool isobj5TextDisabled = false;

            private SettingsCardInnards_obj1_BindingsTracking bindingsTracking;

            public SettingsCardInnards_obj1_Bindings()
            {
                this.bindingsTracking = new SettingsCardInnards_obj1_BindingsTracking(this);
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 41 && columnNumber == 13)
                {
                    isobj2ContentDisabled = true;
                }
                else if (lineNumber == 42 && columnNumber == 13)
                {
                    isobj2ContentTemplateDisabled = true;
                }
                else if (lineNumber == 43 && columnNumber == 13)
                {
                    isobj2ContentTemplateSelectorDisabled = true;
                }
                else if (lineNumber == 23 && columnNumber == 17)
                {
                    isobj3GlyphDisabled = true;
                }
                else if (lineNumber == 28 && columnNumber == 52)
                {
                    isobj4TextDisabled = true;
                }
                else if (lineNumber == 33 && columnNumber == 21)
                {
                    isobj5TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 2: // SettingsCardInnards.xaml line 36
                        this.obj2 = (global::Windows.UI.Xaml.Controls.ContentControl)target;
                        break;
                    case 3: // SettingsCardInnards.xaml line 20
                        this.obj3 = (global::Windows.UI.Xaml.Controls.FontIcon)target;
                        break;
                    case 4: // SettingsCardInnards.xaml line 28
                        this.obj4 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 5: // SettingsCardInnards.xaml line 29
                        this.obj5 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
            }

            public void Recycle()
            {
                return;
            }

            // ISettingsCardInnards_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::ScreenCapture.SettingsCardInnards)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::ScreenCapture.SettingsCardInnards obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_RightControl(obj.RightControl, phase);
                        this.Update_RightControlTemplate(obj.RightControlTemplate, phase);
                        this.Update_RightControlTemplateSelector(obj.RightControlTemplateSelector, phase);
                        this.Update_IconGlyph(obj.IconGlyph, phase);
                    }
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_Title(obj.Title, phase);
                        this.Update_Subtitle(obj.Subtitle, phase);
                    }
                }
            }
            private void Update_RightControl(global::System.Object obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // SettingsCardInnards.xaml line 36
                    if (!isobj2ContentDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ContentControl_Content(this.obj2, obj, null);
                    }
                }
            }
            private void Update_RightControlTemplate(global::Windows.UI.Xaml.DataTemplate obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // SettingsCardInnards.xaml line 36
                    if (!isobj2ContentTemplateDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ContentControl_ContentTemplate(this.obj2, obj, null);
                    }
                }
            }
            private void Update_RightControlTemplateSelector(global::Windows.UI.Xaml.Controls.DataTemplateSelector obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // SettingsCardInnards.xaml line 36
                    if (!isobj2ContentTemplateSelectorDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ContentControl_ContentTemplateSelector(this.obj2, obj, null);
                    }
                }
            }
            private void Update_IconGlyph(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // SettingsCardInnards.xaml line 20
                    if (!isobj3GlyphDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_FontIcon_Glyph(this.obj3, obj, null);
                    }
                }
            }
            private void Update_Title(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // SettingsCardInnards.xaml line 28
                    if (!isobj4TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj4, obj, null);
                    }
                }
            }
            private void Update_Subtitle(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // SettingsCardInnards.xaml line 29
                    if (!isobj5TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj5, obj, null);
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private class SettingsCardInnards_obj1_BindingsTracking
            {
                private global::System.WeakReference<SettingsCardInnards_obj1_Bindings> weakRefToBindingObj; 

                public SettingsCardInnards_obj1_BindingsTracking(SettingsCardInnards_obj1_Bindings obj)
                {
                    weakRefToBindingObj = new global::System.WeakReference<SettingsCardInnards_obj1_Bindings>(obj);
                }

                public SettingsCardInnards_obj1_Bindings TryGetBindingObject()
                {
                    SettingsCardInnards_obj1_Bindings bindingObject = null;
                    if (weakRefToBindingObj != null)
                    {
                        weakRefToBindingObj.TryGetTarget(out bindingObject);
                        if (bindingObject == null)
                        {
                            weakRefToBindingObj = null;
                            ReleaseAllListeners();
                        }
                    }
                    return bindingObject;
                }

                public void ReleaseAllListeners()
                {
                }

            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 4: // SettingsCardInnards.xaml line 28
                {
                    this.TextBlockTitle = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5: // SettingsCardInnards.xaml line 29
                {
                    this.TextBlockSubtitle = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1: // SettingsCardInnards.xaml line 1
                {                    
                    global::Windows.UI.Xaml.Controls.UserControl element1 = (global::Windows.UI.Xaml.Controls.UserControl)target;
                    SettingsCardInnards_obj1_Bindings bindings = new SettingsCardInnards_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element1, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

