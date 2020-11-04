using System;
using System.Collections.Generic;
using System.Text;

namespace GitHubBuckets
{
    public class Category
    {
        public string Name { get; set; }
        public string download_url { get; set; }

        
        public string FormattedName { get => Name.Replace(".txt", ""); }
    }
}
