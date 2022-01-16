﻿using System;

namespace Application.Queries.Project
{
    public class GetProjectsQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int TeamId { get; set; }
    }
}