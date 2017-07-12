using Proggasoft.Data.Hybrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TinyMceDemo.Models
{
    public class Answer : Entity
    {
        public string AnswerBody { get; set; }
        public bool IsRight { get; set; }
        public Guid QuestionID { get; set; }
    }
}