using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BakeryApi.Domain.Entities
{
    [Table("ResetPassword")]
public class ResetPassword
{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string token_hash { get; set; }
        public DateOnly expired_at { get; set;}
        public bool is_used { get; set; }
        public DateOnly created_at { get; set; }
        public int rate_limit { get; set; }
        public DateTime last_reset_timeDate { get; set; }

        public User user {get; set;}
       
}

}