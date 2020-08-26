using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Cvl.ApplicationServer.UI.Attributes;

namespace Cvl.ApplicationServer.Tools.Extension
{
    /// <summary>
    /// Rozszerzenie dla obiektu
    /// </summary>
    public static class ObjectExtensions
    {
        public static string GetPropertyDescription(this PropertyInfo pi)
        {
            var art = pi.GetCustomAttributes(typeof(DescriptionAttribute));
            var str = new StringBuilder();
            foreach (DescriptionAttribute a in art)
            {
                str.Append(a.Description);
            }

            //var art2 = pi.GetCustomAttributes(typeof(DisplayAttribute));
            //foreach (DisplayAttribute a in art2)
            //{
            //    str.Append(a.Description);
            //}

            var art3 = pi.GetCustomAttributes(typeof(DataFormAttribute));
            foreach (DataFormAttribute a in art3)
            {
                str.Append(a.Description);
            }

            //str.Append(" | ");
            //str.Append(pi.Name);

            if (pi.PropertyType.IsEnum)
            {
                //pobieram opis enuma
                str.AppendLine();
                str.Append("Rodzaju: ");
                str.AppendLine();
                str.Append(GetClassDescription(pi.PropertyType));
            }

            return str.ToString();
        }

        public static int GetPropertyOrder(this PropertyInfo pi)
        {
            //var art2 = (DisplayAttribute)pi.GetCustomAttribute(typeof(DisplayAttribute));
            //if (art2 == null)
            //{
            //    return 0;
            //}

            var art3 = (DataFormAttribute)pi.GetCustomAttribute(typeof(DataFormAttribute));
            if (art3 != null)
            {
                return art3.Order;
            }

            return 0;
        }

        public static DataFormAttribute GetDataFormAttribute(this PropertyInfo pi)
        {


            var art3 = (DataFormAttribute)pi.GetCustomAttribute(typeof(DataFormAttribute));
            if (art3 != null)
            {
                return art3;
            }

            return null;
        }

        public static string GetPropertyGroup(this PropertyInfo pi)
        {

            var art3 = (DataFormAttribute)pi.GetCustomAttribute(typeof(DataFormAttribute));
            if (art3 != null)
            {
                return art3.GroupName;
            }

            //var art2 = (DisplayAttribute)pi.GetCustomAttribute(typeof(DisplayAttribute));

            //return art2?.GroupName;

            return null;
        }

        public static string GetClassDescription(this Type typ)
        {
            var art = typ.GetCustomAttributes(typeof(DescriptionAttribute));
            var str = new StringBuilder();
            foreach (DescriptionAttribute a in art)
            {
                str.Append(a.Description);
            }

            if (typ.IsEnum)
            {
                var wartosci = Enum.GetValues(typ);
                foreach (var enumVal in wartosci)
                {
                    str.AppendLine();
                    str.Append(enumVal.ToString());
                    str.Append(" - ");

                    var memInfo = typ.GetMember(enumVal.ToString());
                    var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    foreach (DescriptionAttribute a in attributes)
                    {
                        str.Append(a.Description);
                    }
                }
            }

            return str.ToString();
        }

        public static void SetProperty(this object obiekt, string nazwaPropercji, string wartoscTeksotwaPropercji)
        {
            if (nazwaPropercji.Contains("."))
            {
                //mamy propercje zlożoną
                var propercje = nazwaPropercji.Split('.');
                var o = obiekt;
                for (int i = 0; i < propercje.Length - 1; i++)
                {
                    var prop = o.GetType().GetProperty(propercje[i]);
                    o = prop.GetValue(o);
                }

                var propercja = o.GetType().GetProperty(propercje.Last());
                propercja?.SetProperty(o, wartoscTeksotwaPropercji);
            }
            else
            {
                var propercja = obiekt.GetType().GetProperty(nazwaPropercji);
                propercja?.SetProperty(obiekt, wartoscTeksotwaPropercji);
            }
        }

        public static void SetProperty(this PropertyInfo propercja, object obiekt, string wartoscTeksotwaPropercji)
        {
            if (propercja.CanWrite == false)
            {
                return;
            }

            var typPropercji = propercja.PropertyType;
            object wartosc = null;

            if (string.IsNullOrEmpty(wartoscTeksotwaPropercji) == false)
            {
                if (typPropercji == typeof(bool) || typPropercji == typeof(bool?))
                {
                    if (wartoscTeksotwaPropercji == "on" || wartoscTeksotwaPropercji == "true"
                        || wartoscTeksotwaPropercji == "1")
                    {
                        wartosc = true;
                    }
                    else if (wartoscTeksotwaPropercji == "false" || wartoscTeksotwaPropercji == "0")
                    {
                        wartosc = false;
                    }
                    else
                    {
                        wartosc = bool.Parse(wartoscTeksotwaPropercji);
                    }
                }
                else
                if (typPropercji == typeof(decimal) || typPropercji == typeof(decimal?))
                {
                    wartosc = decimal.Parse(wartoscTeksotwaPropercji);
                }
                else
                if (typPropercji == typeof(int) || typPropercji == typeof(int?))
                {
                    wartosc = int.Parse(wartoscTeksotwaPropercji);
                }
                else
                if (typPropercji == typeof(float) || typPropercji == typeof(float?))
                {
                    wartosc = float.Parse(wartoscTeksotwaPropercji);
                }
                else
                if (typPropercji == typeof(double) || typPropercji == typeof(double?))
                {
                    wartosc = double.Parse(wartoscTeksotwaPropercji);
                }
                else
                if (typPropercji == typeof(DateTime) || typPropercji == typeof(DateTime?))
                {
                    wartosc = DateTime.Parse(wartoscTeksotwaPropercji);
                }
                else if (typPropercji == typeof(TimeSpan) || typPropercji == typeof(TimeSpan?))
                {
                    wartosc = TimeSpan.Parse(wartoscTeksotwaPropercji);
                }
                else if (typPropercji.IsEnum)
                {
                    wartosc = Enum.Parse(typPropercji, wartoscTeksotwaPropercji);
                }
                else
                if (typPropercji == typeof(string))
                {
                    wartosc = wartoscTeksotwaPropercji;
                }
            }

            propercja.SetValue(obiekt, wartosc);
        }


    }
}
