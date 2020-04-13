using Cvl.ApplicationServer.UI.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Base.Model
{
    public class BaseObject
    {
        public const string Bazowe = "Propercje bazowe";
        
        [DataForm(GroupName = Bazowe, Order = 10000, Description = "Unikalny w całym systemie identyfikator obiektu"
        , IsReadOnly = true)]
        public int Id { get; set; }

        [DataForm(GroupName = Bazowe, Order = 10001,
        Description = "Czy dany obiekt jest usunięty(archiwalny). W systemie wszystkie usuwane pliki zamieniane są na archwalne i dostępne są dla uprawnionych osób"
        + ", które mogą je przywrócić.")]
        public bool IsDeleted { get; set; }

        #region Data - create, modyfy, use
        [DataForm(GroupName = Bazowe, Order = 10002, Description = "Data utworzenia obiektu"
        , IsReadOnly = true)]
        public DateTime CreatedDate { get; set; }

        [DataForm(GroupName = Bazowe, Order = 10003, Description = "Data ostatniej modyfikacji obiektu"
        , IsReadOnly = true)]
        public DateTime ModifiedDate { get; set; }

        [DataForm(GroupName = Bazowe, Order = 10004, Description = "Ostatnia data odczytu obiektu"
        , IsReadOnly = true)]
        public DateTime ReadedDate { get; set; }

        #endregion

        #region user- CreatedBy, ModifiedBy, read
        [DataForm(GroupName = Bazowe, Order = 10005, Description = "Użytkownik tworzący obiekt"
        , IsReadOnly = true)]
        public string CreatedBy { get; set; }

        [DataForm(GroupName = Bazowe, Order = 10003, Description = "Użytkownik modyfikujący ostatnio obiekt"
        , IsReadOnly = true)]
        public string ModifiedBy { get; set; }

        [DataForm(GroupName = Bazowe, Order = 10007, Description = "Ostatni użytkownik odczytujący lub zapisujący obiekt"
        , IsReadOnly = true)]
        public string ReadedBy { get; set; }

        #endregion

        #region Versioning
        [DataForm(GroupName = Bazowe, Order = 10008,
        Description = "Rewizja obiektu"
        , IsReadOnly = true)]
        public int Revision { get; set; }

        #endregion

        #region Full-text search

        [DataForm(GroupName = Bazowe, Order = 10008,
        Description = "Indeks wyszukiwania tekstowego obiektów")]
        public virtual string FullTextSearchSimpleIndex { get { return ""; } }

        #endregion
    }
}
