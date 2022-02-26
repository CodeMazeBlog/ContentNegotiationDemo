using ContentNegotiation.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContentNegotiation.Controllers;

[Route("api/blog")]
public class BlogController : Controller
{
	public IActionResult Get()
	{
		var blogs = new List<Blog>();
		var blogPosts = new List<BlogPost>();

		blogPosts.Add(new BlogPost
		{
			Title = "Content Negotiation in Web API",
			MetaDescription = "Content negotiation is the process of selecting the best resource for a response when multiple resource representations are available.",
			Published = true
		});

		blogs.Add(new Blog()
		{
			Name = "Code Maze",
			Description = "C#, .NET and Web Development Tutorials",
			BlogPosts = blogPosts
		});

		return Ok(blogs);
	}
}
