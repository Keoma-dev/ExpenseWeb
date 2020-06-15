#pragma checksum "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1137fde467955da0b5fcb9b8cc0a3db95a77f4f6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Statistics_Statistics), @"mvc.1.0.view", @"/Views/Statistics/Statistics.cshtml")]
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
#line 1 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\_ViewImports.cshtml"
using ExpenseWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\_ViewImports.cshtml"
using ExpenseWeb.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1137fde467955da0b5fcb9b8cc0a3db95a77f4f6", @"/Views/Statistics/Statistics.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2983b609b6cff1a9314bbc5cda839556ddc61f9", @"/Views/_ViewImports.cshtml")]
    public class Views_Statistics_Statistics : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ExpenseWeb.Models.ExpenseStatisticsViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
 if (Model == null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>\r\n        Er zijn nog geen uitgaven toegevoegd!\r\n    </p>\r\n");
#nullable restore
#line 8 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<p>
    <table>
        <tr>
            <th style=""width:200px"">
                Omschrijving
            </th>
            <th style=""width:200px"">
                Bedrag
            </th>
            <th style=""width:200px"">
                Datum
            </th>
        </tr>
        <tr>
            <td>
                ");
#nullable restore
#line 26 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
           Write(Model.HoogsteBedrag.Omschrijving);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                € ");
#nullable restore
#line 29 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
             Write(Model.HoogsteBedrag.Bedrag);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 32 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
           Write(Model.HoogsteBedrag.Datum.ToString("dd/mm/yy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </td>
        </tr>
    </table>
</p>
<p>
    <table>
        <tr>
            <th style=""width:200px"">
                Omschrijving
            </th>
            <th style=""width:200px"">
                Bedrag
            </th>
            <th style=""width:200px"">
                Datum
            </th>
        </tr>
        <tr>
            <td>
                ");
#nullable restore
#line 52 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
           Write(Model.LaagsteBedrag.Omschrijving);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                € ");
#nullable restore
#line 55 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
             Write(Model.LaagsteBedrag.Bedrag);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 58 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
           Write(Model.LaagsteBedrag.Datum.ToString("dd/mm/yy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </td>
        </tr>
    </table>
</p>
<p>
    <table>
        <tr>
            <th style=""width:200px"">
                Totaaluitgave
            </th>
            <th style=""width:200px"">
                Maand
            </th>
        </tr>

");
#nullable restore
#line 74 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
         foreach (var expense in SortList(Model.ExpensesPerMonth))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    € ");
#nullable restore
#line 78 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
                 Write(expense.Bedrag);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 81 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
               Write(expense.Datum.ToString("MMMM"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 84 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    </table>
</p>
<p>
    <table>
        <tr>
            <th style=""width:200px"">
                Totaal duurste categorie
            </th>
            <th style=""width:200px"">
                Categorie
            </th>
        </tr>
        <tr>
            <td>
                € ");
#nullable restore
#line 100 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
             Write(Model.DuursteCategorie.Bedrag);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 103 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
           Write(Model.DuursteCategorie.Categorie);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </td>
        </tr>
    </table>
</p>
<p>
    <table>
        <tr>
            <th style=""width:200px"">
                Totaal goedkoopste categorie
            </th>
            <th style=""width:200px"">
                Categorie
            </th>
        </tr>
        <tr>
            <td>
                € ");
#nullable restore
#line 120 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
             Write(Model.GoedkoopsteCategorie.Bedrag);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 123 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
           Write(Model.GoedkoopsteCategorie.Categorie);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n</p>\r\n");
#nullable restore
#line 134 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
     
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 128 "C:\Users\Keoma\source\repos\ExpenseWeb\ExpenseWeb\Views\Statistics\Statistics.cshtml"
               
        public IEnumerable<ExpenseWeb.Domain.Expense> SortList(IEnumerable<ExpenseWeb.Domain.Expense> list)
        {
            IEnumerable<ExpenseWeb.Domain.Expense> SortedList = list.OrderByDescending(e => e.Datum).ToList();
            return SortedList;
        }
    

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ExpenseWeb.Models.ExpenseStatisticsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
