using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyMceDemo.Models.EF;

namespace TinyMceDemo.Models
{
    public class DetailsViewModel
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Answer> Answers { get; set; }
    }
}