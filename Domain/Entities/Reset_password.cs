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
        public DateTime expired_at { get; set;}
        public bool is_used { get; set; }
        public DateTime created_at { get; set; }
        public int rate_limit { get; set; } = 0;
        public DateTime last_reset_timeDate { get; set; }

        public int user_id {get;set;}
        public User? user {get; set;}
       
}

}