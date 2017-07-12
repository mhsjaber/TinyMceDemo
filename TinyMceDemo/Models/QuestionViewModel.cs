using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TinyMceDemo.Models
{
    public class QuestionViewModel
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] AnswerBody { get; set; }
        public int[] IsRight { get; set; }
    }
}