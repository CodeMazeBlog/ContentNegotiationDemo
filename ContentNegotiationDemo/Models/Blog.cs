using System.Collections.Generic;

namespace ContentNegotiationDemo.Models
{
    public class Blog
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<BlogPost> BlogPosts { get; set; }

        public Blog()
        {
            BlogPosts = new List<BlogPost>();
        }
    }
}
