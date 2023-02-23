using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShorten.Entities
{
    public class ShortenedCustomLink
    {
    public ShortenedCustomLink(string title, string destinationLink)
    {
      var code = Title.Split(" ")[0];
      Title = title;
      ShortenedLink = $"localhost:3000/{code}";
      DestinationLink = destinationLink;
      Code = code;
      CreatedAt = DateTime.Now.ToShortDateString();
    }

        public int Id { get; set; }

        public string Title { get; private set; }

        public string ShortenedLink { get; private set; }

        public string DestinationLink { get; private set; }

        public string Code { get; private set; }

        public string CreatedAt { get; private set; }

        public void Update(string title, string destinationLink)
        {
            Title = title;
            destinationLink = DestinationLink;
        }
    }
}