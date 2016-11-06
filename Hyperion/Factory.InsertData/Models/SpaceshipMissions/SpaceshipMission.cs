using System;
using System.ComponentModel.DataAnnotations;

namespace Factory.InsertData.Models.SpaceshipMissions
{
    public class SpaceshipMission
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string SpaceshipName { get; set; }

        [MaxLength(50)]
        public string Captain { get; set; }

        [MaxLength(50)]
        public string HomePlanet { get; set; }

        public int NumberOfCrewMembers { get; set; }

        [Required]
        [MaxLength(50)]
        public string MissionType { get; set; }

        public DateTime Commision { get; set; }

        [MaxLength(50)]
        public string MissionStatus { get; set; }

    }
}
