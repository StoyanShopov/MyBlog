namespace Blog.Services.Data.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AngleSharp;
    using AngleSharp.Extensions;

    public class AngleSharpExtension
    {
        public static async Task<IEnumerable<string>> GetImageSourceAsync(string html)
        {
            var context = BrowsingContext.New(Configuration.Default);

            var document = await context.OpenAsync(req => req.Content(html));

            var result = new List<string>();

            var imgElements = document.QuerySelectorAll("img")
                .Select(x => x.GetAttribute("src"))
                .ToList();

            result.AddRange(imgElements);

            return result;
        }

        public static async Task<string> UpdateImageSourceAsync(List<string> urls, string html)
        {
            var context = BrowsingContext.New(Configuration.Default);

            var document = await context.OpenAsync(req => req.Content(html));

            var imgElements = document.QuerySelectorAll("img");

            for (int i = 0; i < imgElements.Length; i++)
            {
                imgElements[i].SetAttribute("src", urls[i]);
            }

            return document.ToHtml();
        }
    }
}
