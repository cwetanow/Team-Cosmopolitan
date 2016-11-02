using System;
using System.ComponentModel.DataAnnotations;

namespace Factory.LoadXML.Models
{
    [Serializable]
    public class Mission
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string MissionName { get; set; }

        public DateTime Commission { get; set; }

        [StringLength(20)]
        public string MissionStatus { get; set; }
    }
}
