#pragma checksum "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6e1f4bac81c939cb5e98895e7fd494d1e7bc1d41"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Replies_Details), @"mvc.1.0.view", @"/Views/Replies/Details.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e1f4bac81c939cb5e98895e7fd494d1e7bc1d41", @"/Views/Replies/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cffc283eec59fb9ba499a775de41425547b65ab2", @"/Views/_ViewImports.cshtml")]
    public class Views_Replies_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CodeFactoryAPI.Models.Reply>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h3>Details</h3>\r\n\r\n<div>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 13 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Html.DisplayName("ID"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 16 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Html.DisplayFor(model => model.Reply_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 19 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Message));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 22 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Html.DisplayFor(model => model.Message));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 25 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Code));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 28 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Html.DisplayFor(model => model.Code));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 31 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Image1));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            <img");
            BeginWriteAttribute("src", " src=\"", 884, "\"", 913, 1);
#nullable restore
#line 34 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
WriteAttributeValue("", 890, GetImage(Model.Image1), 890, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" height=\"25\" width=\"25\" />\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 37 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Image2));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            <img");
            BeginWriteAttribute("src", " src=\"", 1108, "\"", 1137, 1);
#nullable restore
#line 40 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
WriteAttributeValue("", 1114, GetImage(Model.Image2), 1114, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" height=\"25\" width=\"25\" />\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 43 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Image3));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            <img");
            BeginWriteAttribute("src", " src=\"", 1332, "\"", 1361, 1);
#nullable restore
#line 46 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
WriteAttributeValue("", 1338, GetImage(Model.Image3), 1338, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" height=\"25\" width=\"25\" />\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 49 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Image4));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            <img");
            BeginWriteAttribute("src", " src=\"", 1556, "\"", 1585, 1);
#nullable restore
#line 52 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
WriteAttributeValue("", 1562, GetImage(Model.Image4), 1562, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" height=\"25\" width=\"25\" />\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 55 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Image5));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            <img");
            BeginWriteAttribute("src", " src=\"", 1780, "\"", 1809, 1);
#nullable restore
#line 58 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
WriteAttributeValue("", 1786, GetImage(Model.Image5), 1786, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" height=\"25\" width=\"25\" />\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 61 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.RepliedDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 64 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Html.DisplayFor(model => model.RepliedDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 67 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.User));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 70 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Link($"{Model.User?.Email}", ControllerName.Users, ActionName.Details, Model.User_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 73 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Question));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 76 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
       Write(Link($"{Model.Question?.Title}", ControllerName.Questions, ActionName.Details, Model.Question_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
#nullable restore
#line 81 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Replies\Details.cshtml"
Write(Link("Edit", ControllerName.Replies, ActionName.Edit, Model.Reply_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6e1f4bac81c939cb5e98895e7fd494d1e7bc1d4112185", async() => {
                WriteLiteral("Back to List");
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
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CodeFactoryAPI.Models.Reply> Html { get; private set; }
    }
}
#pragma warning restore 1591
