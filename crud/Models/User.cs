using System.ComponentModel.DataAnnotations;
using System;
 
namespace Monsters.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
	public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    
        // We can provide some hardcoded default values like so:
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    	
        // New User objects will have these values assigned
    	// However, when we query existing data, CreatedAt/UpdatedAt will refer to 
    	// values that are stored in the DB
    }
}