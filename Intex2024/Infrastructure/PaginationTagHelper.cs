using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Intex2024.Data;

namespace Intex2024.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        public PaginationInfo PageModel { get; set; }
        public string PageAction { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (PageModel == null || PageModel.TotalPages < 2)
            {
                return; // No need to render pagination if only one page or no data
            }

            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");

            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                var li = new TagBuilder("li");
                var a = new TagBuilder("a");

                a.Attributes["href"] = ViewContext.HttpContext.Request.Path + "?" + PageAction + "=" + i;
                a.InnerHtml.Append(i.ToString());

                li.AddCssClass("page-item");
                a.AddCssClass("page-link");

                if (i == PageModel.CurrentPage)
                {
                    li.AddCssClass("active");
                }

                li.InnerHtml.AppendHtml(a);
                ul.InnerHtml.AppendHtml(li);
            }

            output.Content.AppendHtml(ul);
        }
    }
}
