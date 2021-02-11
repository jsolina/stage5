using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppAPI.Application.Queries.ItemList
{
    public class ItemLstViewModel
    {
        public int Id { get; set; }
        public int IdTask { get; set; }
        public string ItemName { get; set; }
        public string ItemDetails { get; set; }
        public string ItemStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
    }
}
