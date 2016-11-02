using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Factory.LoadXML.Models
{
    [Serializable]
    public class Spaceship
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(50)]
        public string SpaseshipName { get; set; }

        [StringLength(50)]
        public string CaptainsName { get; set; }

        [StringLength(50)]
        public string HomePlanet { get; set; }
        
        public int NumberOfCrewMembers { get; set; }
        
        [XmlArrayItem(ElementName = "Mission")]
        public List<Mission> Missions { get; set; }
    }
}
