using ContentNegotiation.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace ContentNegotiation.CustomFormatters;

public class CsvOutputFormatter : TextOutputFormatter
{
	public CsvOutputFormatter()
	{
		SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/csv"));
		SupportedEncodings.Add(Encoding.UTF8);
		SupportedEncodings.Add(Encoding.Unicode);
	}

	protected override bool CanWriteType(Type? type)
		=> typeof(Blog).IsAssignableFrom(type)
			|| typeof(IEnumerable<Blog>).IsAssignableFrom(type);

	public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
	{
		var response = context.HttpContext.Response;
		var buffer = new StringBuilder();

		if (context.Object is IEnumerable<Blog>)
		{
			foreach (var Blog in (IEnumerable<Blog>)context.Object)
			{
				FormatCsv(buffer, Blog);
			}
		}
		else
		{
			FormatCsv(buffer, (Blog)context.Object);
		}

		await response.WriteAsync(buffer.ToString(), selectedEncoding);
	}

	private static void FormatCsv(StringBuilder buffer, Blog blog)
	{
		foreach (var blogPost in blog.BlogPosts)
		{
			buffer.AppendLine($"{blog.Name},\"{blog.Description},\"{blogPost.Title},\"{blogPost.Published}\"");
		}
	}
}

