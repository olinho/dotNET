using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CosmicAdventureDTO
{ 
    [DataContract]
    public class Systemik
    {
        [DataMember]
        public string Name { get; set; }
        private int MinShipPower;
        public int MinShipPower_
        {
            set { MinShipPower = value; }
            get { return MinShipPower; }
        }
        [DataMember]
        public int BaseDistance { get; set; }
        private int Gold;
        public int Gold_
        {
            set { Gold = value; }
            get { return Gold; }
        }
    }

    [DataContract]
    public class Starship
    {
        [DataMember]
        public List<Person> Crew { get; set; } = new List<Person>();
        [DataMember]
        public int Gold { get; set; }
        [DataMember]
        public int ShipPower { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
        public string Nick { get; set; }
        public float Age { get; set; }

    }
}
