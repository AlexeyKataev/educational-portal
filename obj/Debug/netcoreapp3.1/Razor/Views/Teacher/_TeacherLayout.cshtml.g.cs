#pragma checksum "D:\Dotnet\CollegePortal\Views\Teacher\_TeacherLayout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d04c7d6ad182f1685b83ac756e471d7dc890f081"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Teacher__TeacherLayout), @"mvc.1.0.view", @"/Views/Teacher/_TeacherLayout.cshtml")]
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
#line 1 "D:\Dotnet\CollegePortal\Views\_ViewImports.cshtml"
using Dotnet;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Dotnet\CollegePortal\Views\_ViewImports.cshtml"
using Dotnet.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d04c7d6ad182f1685b83ac756e471d7dc890f081", @"/Views/Teacher/_TeacherLayout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e28dfff7a82c3018e957afc6fd87e7b334de3883", @"/Views/_ViewImports.cshtml")]
    public class Views_Teacher__TeacherLayout : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Dotnet\CollegePortal\Views\Teacher\_TeacherLayout.cshtml"
  
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n    <h1 class=\"display-4 mb-4 text-center\">");
#nullable restore
#line 6 "D:\Dotnet\CollegePortal\Views\Teacher\_TeacherLayout.cshtml"
                                      Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\t<div class=\"container\">\r\n\t\t<div class=\"row\">\r\n\t\t\t");
#nullable restore
#line 9 "D:\Dotnet\CollegePortal\Views\Teacher\_TeacherLayout.cshtml"
       Write(RenderBody());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t</div>\r\n\t</div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
