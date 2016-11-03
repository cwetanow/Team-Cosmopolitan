using System;
using System.ComponentModel.DataAnnotations;

namespace Factory.LoadXML.Models
{
    [Serializable]
    public class SpaceshipsXML
    {
        [StringLength(50)]
        public string SpaceshipName { get; set; }

        [StringLength(50)]
        public string Captain { get; set; }

        [StringLength(50)]
        public string HomePlanet { get; set; }

        public int NumberOfCrewMembers { get; set; }

        [StringLength(50)]
        public string MissionType { get; set; }

        public DateTime Commission { get; set; }

        [StringLength(20)]
        public string MissionStatus { get; set; }
    }
}
