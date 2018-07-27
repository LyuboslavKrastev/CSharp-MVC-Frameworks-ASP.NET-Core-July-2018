//using Microsoft.AspNetCore.Mvc.TagHelpers;
//using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using Microsoft.AspNetCore.Razor.TagHelpers;
//using System.Text;

//namespace BookLibrary.App.Helpers
//{
//    [HtmlTargetElement("form-control")]
//    public class FormControlHelper : TagHelper
//    {
//        private readonly IHtmlGenerator generator;

//        public FormControlHelper(IHtmlGenerator generator)
//        {
//            this.generator = generator;
//        }

//        [HtmlAttributeName("asp-for")]
//        public ModelExpression For { get; set; }

//        public override async void Process(TagHelperContext context, TagHelperOutput output)
//        {
//            var result = new StringBuilder();

//            result.AppendLine(@"<div class=""form-group row"">");

//            await new LabelTagHelper(this.generator).ProcessAsync(context, output);

//            result.AppendLine("</div>");
//            output.Content.SetHtmlContent(result.ToString());
//        }
//    }
//}
