using System.Data.Entity;

using Factory.LoadXML.Models;

namespace Factory.LoadXML
{
    public class FactoryDbContextXML : DbContext
    {
        public FactoryDbContextXML()
            : base("Factory")
        {
        }

        public virtual IDbSet<Spaceship> Spaceship { get; set; }

        public virtual IDbSet<Mission> Mission { get; set; }
    }
}
