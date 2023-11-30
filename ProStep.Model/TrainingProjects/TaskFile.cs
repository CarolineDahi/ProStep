﻿using ProStep.Model.Base;
using ProStep.Model.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.TrainingProjects
{
    public class TaskFile : BaseEntity
    {
        public TaskFile()
        {
        }

        public Guid TaskId { get; set; }
        public Task Task { get; set; }

        public Guid FileId { get; set; }
        public _File File { get; set; }
    }
}
