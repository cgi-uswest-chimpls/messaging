

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Messaging.Models
{
    [Table("messages")]
    public class Message
    {
        public int Id { get; set; }
         public int FromId { get; set; }
         public int FromUserType { get; set; }
         public int ToId { get; set; }
         public int ToUserType { get; set; }
         public string Title { get; set; }
         public string Content { get; set; }
         public string Attachment { get; set; }

        [Column(TypeName = "TIMESTAMP")] 
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}