using System;
using System.Collections.Generic;
using System.Xml.Linq;

using Factory.LoadXML.Models;
using System.Globalization;

namespace Factory.LoadXML
{
    public class FactoryXmlImporter
    {
        public static ICollection<SpaceshipMissionsXmlModel> ImportSpaceships(string filePath)
        {
            var spaceships = new List<SpaceshipMissionsXmlModel>();

            var xmlDoc = XDocument.Load(filePath);
            var spaceshipElements = xmlDoc.Root.Elements();

            foreach (var spaceship in spaceshipElements)
            {
                var sp = new SpaceshipMissionsXmlModel();

                var spaceshipName = spaceship.Element("SpaceshipName");
                if (spaceshipName != null)
                {
                    sp.SpaceshipName = spaceshipName.Value;
                }

                var captainsName = spaceship.Element("Captain");
                if (captainsName != null)
                {
                    sp.Captain = captainsName.Value;
                }

                var homePlanet = spaceship.Element("HomePlanet");
                if (homePlanet != null)
                {
                    sp.HomePlanet = homePlanet.Value;
                }

                var numberOfCrewMembers = spaceship.Element("NumberOfCrewMembers");
                if (numberOfCrewMembers != null)
                {
                    sp.NumberOfCrewMembers = int.Parse(numberOfCrewMembers.Value);
                }

                var missionType = spaceship.Element("MissionType");
                if (missionType != null)
                {
                    sp.MissionType = missionType.Value;
                }

                var commission = spaceship.Element("Commission");
                if (commission != null)
                {
                    sp.Commission = DateTime.Parse(commission.Value);
                }

                var missionStatus = spaceship.Element("MissionStatus");
                if (missionStatus != null)
                {
                    sp.MissionStatus = missionStatus.Value;
                }

                spaceships.Add(sp);
            }

            return spaceships;
        }
    }
}
