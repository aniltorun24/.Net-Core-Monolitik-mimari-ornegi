using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApplication1.TagHelpers
{
    public class ItalicTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.PreContent.SetHtmlContent("<i>");//<i>ahmet</i>'deki ilk kısımı oluşturmak için
            output.PostContent.SetHtmlContent("</li>");
        }
    }
}
