using Intex2024.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
namespace Mission11_JacobBigler.Infrastructure
{
	[HtmlTargetElement("div", Attributes = "page-model")]
	public class PaginationTagHelper : TagHelper
	{
		private IUrlHelperFactory urlHelperFactory;

		public PaginationTagHelper(IUrlHelperFactory temp)
		{
			urlHelperFactory = temp;
		}

		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext? ViewContext { get; set; }
		public string? PageAction { get; set; }
		public PaginationInfo PageModel { get; set; }

		public bool PageClassEnabled { get; set; } = false;
		public string PageClassNormal { get; set; } = String.Empty;
		public string PageClassSelected { get; set; } = String.Empty;

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			if (ViewContext != null && PageModel != null && PageModel.TotalPages > 1)
			{
				IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

				TagBuilder navTag = new TagBuilder("nav");
				TagBuilder ulTag = new TagBuilder("ul");
				ulTag.AddCssClass("pagination");

				for (int i = 1; i <= PageModel.TotalPages; i++)
				{
					TagBuilder liTag = new TagBuilder("li");
					TagBuilder buttonTag = new TagBuilder("button");
					buttonTag.Attributes["type"] = "button"; // Ensure it's a button type

					// Set the onclick attribute to navigate to the corresponding page
					buttonTag.Attributes["onclick"] = $"window.location.href='{urlHelper.Action(PageAction, new { pageNum = i })}'";

					buttonTag.AddCssClass("page-link");
					buttonTag.InnerHtml.Append(i.ToString());

					liTag.AddCssClass("page-item");

					if (i == PageModel.CurrentPage)
					{
						liTag.AddCssClass(PageClassSelected);
					}
					else if (PageClassEnabled)
					{
						liTag.AddCssClass(PageClassNormal);
					}

					liTag.InnerHtml.AppendHtml(buttonTag);
					ulTag.InnerHtml.AppendHtml(liTag);
				}

				navTag.InnerHtml.AppendHtml(ulTag);
				output.Content.AppendHtml(navTag);
			}
		}
	}
}