using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Todo.Entitites
{
    public class TaskModel
    {
        public DateTime TaskDateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public string VisibleTime { get; set; }
        public SolidColorBrush Color { get; set; }
        public TaskCategory Category { get; set; }
    }
    public class TaskCategory
    {
        public string CategoryName { get; set; }
        public SolidColorBrush Color { get; set; }
    }
}
