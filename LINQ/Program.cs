using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Car> myCars = new List<Car>(){
                new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
                new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
                new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
                new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
                new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
                new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
                new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
                new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
                new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)
                };
            createXmlFromLinq(myCars);
            Serialize(myCars);
            List<Car> myCarsFromDeserialize = Deserialize();
            CreateXHTMLTable(myCarsFromDeserialize);
            ModifyXML();
            XPathExpressions();
            LinqStatements(myCarsFromDeserialize);

        }


       static private void createXmlFromLinq(List<Car> myCars)
        {
            IEnumerable<XElement> cars =myCars
                                                .Select(car =>
                                                new XElement("car",
                                                    new XElement("model", car.model),
                                                    new XElement("engine",
                                                        new XAttribute("model", car.motor.model),
                                                        new XElement("displacement", car.motor.displacement),
                                                        new XElement("horsePower", car.motor.horsePower)),
                                                    new XElement("year", car.year)));
            XElement rootNode = new XElement("cars", cars);
            rootNode.Save("CarsFromLinq.xml");
        }

        private static void CreateXHTMLTable(List<Car> myCars)
        {
            IEnumerable<XElement> rows = myCars
                .Select(car =>
                new XElement("tr", new XAttribute("style", "border: 1px solid black"),
                    new XElement("td", new XAttribute("style", "border: 1px  solid black"), car.model),
                    new XElement("td", new XAttribute("style", "border: 1px solid  black"), car.motor.model),
                    new XElement("td", new XAttribute("style", "border: 1px solid black"), car.motor.displacement),
                    new XElement("td", new XAttribute("style", "border: 1px solid  black"), car.motor.horsePower),
                    new XElement("td", new XAttribute("style", "border: 1px solid  black"), car.year)));
            XElement table = new XElement("table", new XAttribute("style", "border: 1px double black"), rows);
            XElement template = XElement.Load("template.html");
            XElement body = template.Element("{http://www.w3.org/1999/xhtml}body");
            body.Add(table);
            template.Save("templateWithTable.html");
        }

        private static void ModifyXML()
        {
            XElement newXML = XElement.Load("CarsCollection.xml");
            foreach (var car in newXML.Elements())
            {
                foreach (var field in car.Elements())
                {
                    if (field.Name == "engine")
                    {
                        foreach (var engineElement in field.Elements())
                        {
                            if (engineElement.Name == "horsePower")
                            {
                                engineElement.Name = "hp";
                            }
                        }
                    }
                    else if (field.Name == "model")
                    {
                        var yearField = car.Element("year");
                        XAttribute attribute = new XAttribute("year", yearField.Value);
                        field.Add(attribute);
                        yearField.Remove();
                    }
                }
            }
            newXML.Save("Modified.xml");
        }

        private static void Serialize(List<Car> myCars)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Car>),
                 new XmlRootAttribute("cars"));
            using (TextWriter writer = new StreamWriter("CarsCollection.xml"))
            {
                serializer.Serialize(writer, myCars);
            }
        }
        private static List<Car> Deserialize()
        {
            List<Car> cars = new List<Car>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Car>),new XmlRootAttribute("cars"));
            using (Stream reader = new FileStream("CarsCollection.xml", FileMode.Open))
            {
                cars = (List<Car>)serializer.Deserialize(reader);
            }
            return cars;
        }

        private static void XPathExpressions()
        {
            XElement rootNode = XElement.Load("CarsCollection.xml");
          
            var myXPathExpression1 = "sum(//car/engine[@model!=\"TDI\"]/horsePower) div count(//car/engine[@model!=\"TDI\"])";
            
            double avgHP = (double)rootNode.XPathEvaluate(myXPathExpression1);
            
            Console.WriteLine("Srednia dla benzyny: {0}",avgHP);

            var myXPathExpression2 = "//car[not(following-sibling::car/model = model)]";
            IEnumerable<XElement> models = rootNode.XPathSelectElements(myXPathExpression2);
            foreach (var model in models)
            {
                Console.WriteLine(model.Element("model").Value);
            }
        }

        private static void  LinqStatements(List<Car> myCars)
        {
            var results = myCars
                               .Where(x => x.model.Equals("A6"))
                               .Select(x =>
                                           new {
                                               engineType = x.motor.model.Equals("TDI") == true ? "diesel" : "petrol",
                                               hppl = x.motor.horsePower / x.motor.displacement
                                           });
            var results2 = results
                                  .GroupBy(x => x.engineType)
                                  .Select(g => {
                                      return new
                                      {
                                          nazwa = g.Key,
                                          srednia = g.Average(r => r.hppl)
                                      };
                                  });
            foreach (var result in results2)
            {
                Console.WriteLine("{0}: {1}", result.nazwa, result.srednia);
            }
        }

    }
}
