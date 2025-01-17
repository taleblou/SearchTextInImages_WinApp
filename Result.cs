using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchInImages
{
    internal class Result
    {
        public string Filename { get; set; }
        public string MatchingText { get; set; }
        public string ExtractedText { get; set; }
    }
}
