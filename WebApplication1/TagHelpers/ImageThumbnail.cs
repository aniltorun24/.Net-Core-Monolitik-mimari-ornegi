using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApplication1.TagHelpers
{
    [HtmlTargetElement("thumbnail")]//tag helperı istediğimiz isimde olışturmamızı sağlar
    public class ImageThumbnail:TagHelper
    {
        public string ImageSrc { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";//<img/>bunu yapmış olduk bu komutla.
            string fileName = ImageSrc.Split('.')[0];//gelen yolu noktadan ikiye böl ve 0. indexi al yani noktadan önceki kısmı 
            string fileExtensions = Path.GetExtension(ImageSrc);//Bu komutla ImageSrc olarak gelecek fotonun yolunu türünü bulacak
                                                                //(.jpeg-.png)
            output.Attributes.SetAttribute("src", $"{fileName}-100x100{fileExtensions}");//en baştaki src attributenin ne isminde
                                                                                         //olacağını belirtirken virgülden sonrası ne yapacağını belirtti.
        }
    }
}
