#pragma checksum "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Company\CompanyDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1159e0859b57d2d9f289eb808ac4eb45ba627bae"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Company_CompanyDetails), @"mvc.1.0.view", @"/Views/Company/CompanyDetails.cshtml")]
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
#line 1 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\_ViewImports.cshtml"
using sPlannedIt;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\_ViewImports.cshtml"
using sPlannedIt.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Company\CompanyDetails.cshtml"
using sPlannedIt.Logic;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1159e0859b57d2d9f289eb808ac4eb45ba627bae", @"/Views/Company/CompanyDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4fbb292a4a7e3d9016aaab5ae2c45ae50515c788", @"/Views/_ViewImports.cshtml")]
    public class Views_Company_CompanyDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<sPlannedIt.Logic.Models.Company>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Company\CompanyDetails.cshtml"
  
    ViewData["Title"] = "CompanyDetails";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"card\">\r\n    <div class=\"card-header\">\r\n        <h1>");
#nullable restore
#line 9 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Company\CompanyDetails.cshtml"
       Write(Model.CompanyID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n    </div>\r\n    <div class=\"card-body\">\r\n        <h5 class=\"card-title\">Company Name: ");
#nullable restore
#line 12 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Company\CompanyDetails.cshtml"
                                        Write(Model.CompanyName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n    </div>\r\n    <div class=\"card-footer\">\r\n        <h6>Employees:</h6>\r\n");
#nullable restore
#line 16 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Company\CompanyDetails.cshtml"
         if (Model.Employees.Any())
        {
            foreach (string id in Model.Employees)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>");
#nullable restore
#line 20 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Company\CompanyDetails.cshtml"
              Write(CompanyManager_Logic.GetEmployee(id));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 21 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Company\CompanyDetails.cshtml"
            }
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<sPlannedIt.Logic.Models.Company> Html { get; private set; }
    }
}
#pragma warning restore 1591