﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Courses.Comment
{
    public class UpdateCommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}
