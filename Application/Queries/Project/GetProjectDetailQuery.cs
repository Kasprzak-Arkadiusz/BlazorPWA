using System;
using System.Collections.Generic;

namespace Application.Queries.Project
{
    public class GetProjectDetailQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int TeamId { get; set; }
        public List<string> Technologies { get; set; }
    }
}