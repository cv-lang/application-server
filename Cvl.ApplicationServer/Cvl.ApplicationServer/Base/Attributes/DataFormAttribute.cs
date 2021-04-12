using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Base.Attributes
{
    /// <summary>
    /// Klasa opisująca dane
    /// </summary>
    public class DataFormAttribute : Attribute
    {
        public DataFormAttribute()
        { }

        public DataFormAttribute(string description)
        {
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; } = 100;
        public string GroupName { get; set; } = DefaultGroupName;
        public bool ShowInGrid { get; set; }
        public bool IsReadOnly { get; set; } = false;
        public string Columns { get; set; }
        public double Width { get; set; }

        #region Editor
        public string EditorDataSourceTypeName { get; set; }
        public string Watermark { get; set; }



        #endregion

        #region Default groups

        public static string DefaultGroupName = "Podstawowa";

        #endregion

        #region TypZrodla
        public Type CollectionItemType { get; set; }
        #endregion
    }
}
