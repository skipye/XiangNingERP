using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelProject
{
    public class SJ_WorkModel
    {
        public int Id { get; set; }
        public string BOM_path { get; set; }
        public string paper_path { get; set; }
        public string picture { get; set; }
        public decimal? volume { get; set; }
    }
}
