using ContentNegotiationDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContentNegotiationDemo.CustomFormatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(Blog).IsAssignableFrom(type) || typeof(IEnumerable<Blog>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<Blog>)
            {
                foreach (var blog in (IEnumerable<Blog>)context.Object)
                {
                    FormatCsv(buffer, blog);
                }
            }
            else
            {
                FormatCsv(buffer, (Blog)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }

        private static void FormatCsv(StringBuilder buffer, Blog blog)
        {
            foreach (var blogPost in blog.BlogPosts)
            {
                buffer.AppendLine($"{blog.Name},\"{blog.Description},\"{blogPost.Title},\"{blogPost.Published}\"");
            }
        }

    }
}
