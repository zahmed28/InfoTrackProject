using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SearchReadService.Domain
{
    public class SearchResults
    {
        [Key] // Denotes this as the Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Configures this to be an auto-generated value

        public int Id { get; set; }

        public string Query { get; set; }

        public string ResultURL { get; set; }
        public string RankingIndices { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
