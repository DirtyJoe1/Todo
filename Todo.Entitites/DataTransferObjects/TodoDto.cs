using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Entitites.DataTransferObjects
{
    public class Coordinate
    {
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
    public class TodoDto
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Date { get; set; }
        public bool IsCompleted { get; set; }
        public Coordinate Coordinate { get; set; }
    }
    public class TodoListDto
    {
        public List<TodoDto> Data { get; set; }
    }
}
