#pragma checksum "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9cdb51b11d31c235b62a882938c33b0a39203206"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Questions_Delete), @"mvc.1.0.view", @"/Views/Questions/Delete.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9cdb51b11d31c235b62a882938c33b0a39203206", @"/Views/Questions/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f543f12f3617776d6d75d58479ed0202d4588165", @"/Views/_ViewImports.cshtml")]
    public class Views_Questions_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CodeFactoryAPI.Models.Question>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
  
    ViewData["Title"] = "Delete";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h3>Are you sure you want to delete this?</h3>\r\n<div>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 12 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 15 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 18 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Code));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 21 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Code));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 24 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Image1));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            <img");
            BeginWriteAttribute("src", " src=\"", 725, "\"", 754, 1);
#nullable restore
#line 27 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
WriteAttributeValue("", 731, GetImage(Model.Image1), 731, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" height=\"25\" width=\"25\"");
            BeginWriteAttribute("alt", " alt=\"", 778, "\"", 784, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 30 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Image2));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            <img");
            BeginWriteAttribute("src", " src=\"", 956, "\"", 985, 1);
#nullable restore
#line 33 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
WriteAttributeValue("", 962, GetImage(Model.Image2), 962, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" height=\"25\" width=\"25\"");
            BeginWriteAttribute("alt", " alt=\"", 1009, "\"", 1015, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 36 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Image3));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            <img");
            BeginWriteAttribute("src", " src=\"", 1187, "\"", 1216, 1);
#nullable restore
#line 39 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
WriteAttributeValue("", 1193, GetImage(Model.Image3), 1193, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" height=\"25\" width=\"25\"");
            BeginWriteAttribute("alt", " alt=\"", 1240, "\"", 1246, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 42 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Image4));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            <img");
            BeginWriteAttribute("src", " src=\"", 1418, "\"", 1447, 1);
#nullable restore
#line 45 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
WriteAttributeValue("", 1424, GetImage(Model.Image4), 1424, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" height=\"25\" width=\"25\"");
            BeginWriteAttribute("alt", " alt=\"", 1471, "\"", 1477, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 48 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Image5));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            <img");
            BeginWriteAttribute("src", " src=\"", 1649, "\"", 1678, 1);
#nullable restore
#line 51 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
WriteAttributeValue("", 1655, GetImage(Model.Image5), 1655, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" height=\"25\" width=\"25\"");
            BeginWriteAttribute("alt", " alt=\"", 1702, "\"", 1708, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 54 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 57 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 60 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.AskedDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 63 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayFor(model => model.AskedDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 66 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.User));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 69 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayFor(model => model.User.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 72 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Tag1));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 75 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Tag1.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 78 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Tag2));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 81 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Tag2.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 84 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Tag3));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 87 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Tag3.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 90 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Tag4));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 93 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Tag4.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 96 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Tag5));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 99 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Tag5.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9cdb51b11d31c235b62a882938c33b0a3920320615938", async() => {
                WriteLiteral("\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "9cdb51b11d31c235b62a882938c33b0a3920320616205", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 104 "C:\Users\SOHAM\Desktop\Git\CodeFactory\CodeFactoryWeb\Views\Questions\Delete.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Question_ID);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n        <input type=\"submit\" value=\"Delete\" class=\"btn btn-danger\" /> |\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9cdb51b11d31c235b62a882938c33b0a3920320618006", async() => {
                    WriteLiteral("Back to List");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CodeFactoryAPI.Models.Question> Html { get; private set; }
    }
}
#pragma warning restore 1591
