using Microsoft.AspNetCore.Razor.TagHelpers;
using BankApp.Web.Data.Contexts;

namespace BankApp.Web.TagHelpers
{
    [HtmlTargetElement("getAccountCount")]
    public class AccountCountTagHelper : TagHelper
    {
        private readonly BankContext _context;
        public int UserId { get; set; }
        public AccountCountTagHelper(BankContext context) 
        { 
            _context = context;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var accountCount = _context.Accounts.Count(a => a.UserId == UserId);
            var html = $"<span class='badge bg-danger'>{accountCount}</span>";

            output.Content.SetHtmlContent(html);
        }
    }
}
