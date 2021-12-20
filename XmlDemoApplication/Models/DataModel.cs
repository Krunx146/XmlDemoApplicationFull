using System;
using System.Xml.Serialization;

namespace XmlDemoApplication.Models
{
    [Serializable, XmlRoot(ElementName = "catalog")]
    public class DataModel
    {
        [XmlElement("book")]
        public Book[] Catalog { get; set; }
    }

    [XmlType("book")]
    public class Book
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("genre")]
        public string Genre { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("publish_date")]
        public DateTime? PublishDate { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlIgnore]
        public string BorrowedBy { get; set; }

        [XmlIgnore]
        public bool IsBorrowed { get => !string.IsNullOrEmpty(BorrowedBy); }
    }
}
