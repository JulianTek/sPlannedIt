#pragma checksum "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Employer\IndexEmployer.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "41aff43b9e9eb3efc11040b7c1caad81a7d185b3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Employer_IndexEmployer), @"mvc.1.0.view", @"/Views/Employer/IndexEmployer.cshtml")]
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
#line 1 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Employer\IndexEmployer.cshtml"
using sPlannedIt.Logic.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"41aff43b9e9eb3efc11040b7c1caad81a7d185b3", @"/Views/Employer/IndexEmployer.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4fbb292a4a7e3d9016aaab5ae2c45ae50515c788", @"/Views/_ViewImports.cshtml")]
    public class Views_Employer_IndexEmployer : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<sPlannedIt.Viewmodels.Homepage_Viewmodels.IndexEmployerViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<h1>IndexEmployee</h1>
<div class=""row"">
    <div class=""col-sm-6"">
        <div class=""card"">
            <div class=""card-header"">
                <h1 class=""card-title"">Actions:</h1>
            </div>
            <div class=""card-body"">
                <a class=""btn btn-primary"">This is a temporary button</a>
            </div>
        </div>
    </div>
    <div class=""col-sm-6"">
");
#nullable restore
#line 16 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Employer\IndexEmployer.cshtml"
         if (Model.TodaysWorkers.Any())
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <table class=\"table table-striped table-hover\">\r\n                <tbody>\r\n                    <tr>\r\n                        <th colspan=\"3\">Working Today</th>\r\n                    </tr>\r\n");
#nullable restore
#line 23 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Employer\IndexEmployer.cshtml"
                     foreach (Shift shift in Model.TodaysWorkers)
                    {


#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr valign=\"top\">\r\n                            <td width=\"100\">");
#nullable restore
#line 27 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Employer\IndexEmployer.cshtml"
                                       Write(shift.ShiftDate.Date);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 28 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Employer\IndexEmployer.cshtml"
                           Write(String.Concat(shift.StartTime + ":00"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 28 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Employer\IndexEmployer.cshtml"
                                                                     Write(String.Concat(shift.EndTime + ":00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td><br></td>\r\n                        </tr>\r\n");
#nullable restore
#line 31 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Employer\IndexEmployer.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n");
#nullable restore
#line 34 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Employer\IndexEmployer.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <table class=\"table table-striped table-hover\">\r\n                <tbody>\r\n                    <tr>\r\n                        <th colspan=\"3\">None working today</th>\r\n                    </tr>\r\n                </tbody>\r\n            </table>\r\n");
#nullable restore
#line 44 "C:\Users\Julia\Documents\GitHub\sPlannedIt\sPlannedIt\Views\Employer\IndexEmployer.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<sPlannedIt.Viewmodels.Homepage_Viewmodels.IndexEmployerViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591