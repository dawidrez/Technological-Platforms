using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab3
{
    [XmlType("car")]
    public class Car
    {
        public string model;
        public int year;
        [XmlElement(ElementName = "engine")]
        public Engine motor;
        public Car() {}

        public Car(string model, Engine engine,int year) {
            this.model = model;
            this.motor = engine;
            this.year = year;
        }

     

    }
}
