using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Connection
    {
        [Required]
        public string ConnectionId { get; init; }
        [Required]
        public Guid UserId { get; init; }
    }
}