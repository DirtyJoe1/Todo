﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Todo.Entitites
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public DateTime TaskDateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public string DisplayTime { get; set; }
        public SolidColorBrush Color { get; set; }
        public string CategoryName { get; set; }
    }
}
