#pragma checksum "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Messages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "004ad04230011f3eeecb29b8abc8c05eafbf3c0a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Messages_Index), @"mvc.1.0.view", @"/Views/Messages/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\_ViewImports.cshtml"
using CodeFactoryWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\_ViewImports.cshtml"
using static CodeFactoryWeb.Extra.Addons;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\_ViewImports.cshtml"
using CodeFactoryWeb.Extra;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"004ad04230011f3eeecb29b8abc8c05eafbf3c0a", @"/Views/Messages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cffc283eec59fb9ba499a775de41425547b65ab2", @"/Views/_ViewImports.cshtml")]
    public class Views_Messages_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CodeFactoryAPI.Models.Message>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Messages\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<p>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "004ad04230011f3eeecb29b8abc8c05eafbf3c0a3873", async() => {
                WriteLiteral("Create New");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 14 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Messages\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Messages));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 17 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Messages\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Messeger));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 20 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Messages\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Receiver));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 26 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Messages\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 30 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Messages\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Messages));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 33 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Messages\Index.cshtml"
               Write(Link("Go to", ControllerName.Users, ActionName.Details, item.Messeger_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 36 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Messages\Index.cshtml"
               Write(Link("Go to", ControllerName.Users, ActionName.Details, item.Receiver_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 39 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Messages\Index.cshtml"
               Write(Link("Edit", ControllerName.Messages, ActionName.Edit, item.Message_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                    ");
#nullable restore
#line 40 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Messages\Index.cshtml"
               Write(Link("Details", ControllerName.Messages, ActionName.Details, item.Message_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                    ");
#nullable restore
#line 41 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Messages\Index.cshtml"
               Write(Link("Delete", ControllerName.Messages, ActionName.Delete, item.Message_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 44 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Messages\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CodeFactoryAPI.Models.Message>> Html { get; private set; }
    }
}
#pragma warning restore 1591
