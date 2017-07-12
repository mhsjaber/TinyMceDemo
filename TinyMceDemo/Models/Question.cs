using Proggasoft.Data.Hybrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TinyMceDemo.Models
{
    public class Question : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Answer> Answers { get; set; }
    }
}