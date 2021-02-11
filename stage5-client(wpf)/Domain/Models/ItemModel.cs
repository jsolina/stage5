using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public int IdTask { get; set; }
        public string ItemName { get; set; }
        public string ItemDetails { get; set; }
        public string ItemStatus { get; set; }
        public string CreatedAt { get; set; }
        public string LastModified { get; set; }
    }
}
