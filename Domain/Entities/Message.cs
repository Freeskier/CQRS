using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        [Required]
        public Guid SenderId { get; set; }
        public virtual AppUser Sender { get; set; }
        [Required]
        public Guid ReceiverId { get; set; }
        public virtual AppUser Receiver { get; set; }
        [MaxLength(2000)]
        public string Content { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime DateRead { get; set; }

        public Message()
        {
            DateSent = DateTime.UtcNow;
        }

    }
}