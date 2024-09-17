using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SmartRocko
{
    public class CoreRocko
    {
        public List<string> lstSongs = new List<string>();
        public CoreRocko()
        {
        }
        public List<string> GetVideosFromDisk(string Direccion, string Extension, bool MakeToUpper)
        {
            lstSongs.Clear();
            string[] vExtensiones = Extension.Split(';');
            foreach (string Exten in vExtensiones)
            {
                foreach (string file in Directory.GetFiles(@Direccion, Exten))
                {
                    if (file.Contains("'") || file.Contains("Á") || file.Contains("É") || file.Contains("Í") || file.Contains("Ó") || file.Contains("Ú") || file.Contains("_"))
                    {
                        File.Copy(file, file.ToUpper().Replace("'", "").Replace("Á","A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("_"," ").ToUpper(), true);
                        File.Delete(file);
                        lstSongs.Add(file.ToUpper().Replace("'", "").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("_", " ").ToUpper());
                    }
                    else
                        lstSongs.Add(file);
                    if (MakeToUpper)
                    {
                        if (HasLowerLetter(file))
                            File.Move(file, file.ToUpperInvariant());
                    }
                }
            }
            lstSongs.Sort();
            return lstSongs;
        }
        private bool HasLowerLetter(string text)
        {
            foreach (char c in text)
            {
                if (char.IsLower(c)) return true;
            }
            return false;
        }
        public static bool SaveSongInList(string Direccion, string sDireccionRoot, string Cancion, string sExtensiones, bool bTipoListado, string sDireccionLista)
        {
            if (bTipoListado)
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    if (!File.Exists(Direccion))
                    {
                        XmlNode node1 = document.CreateNode(XmlNodeType.Element, "Canciones", null);
                        document.AppendChild(node1);
                        document.Save(Direccion);
                    }
                    XmlDocument doc = new XmlDocument();
                    doc.Load(Direccion);
                    XmlNode node = doc.CreateNode(XmlNodeType.Element, "Pista", null);
                    string[] Separados = Cancion.Split('-');
                    XmlNode Artista = doc.CreateNode(XmlNodeType.Element, "Artista", null);
                    XmlNode NombreCancion = doc.CreateNode(XmlNodeType.Element, "NombreCancion", null);
                    XmlNode UbicacionCancion = doc.CreateNode(XmlNodeType.Element, "Direccion", null);
                    XmlNode FechaCreacion = doc.CreateNode(XmlNodeType.Element, "FechaAdicion", null);
                    if (Separados.Count() > 0)
                    {
                        Artista.InnerText = Separados[0].ToString().Substring(Separados[0].ToString().LastIndexOf(@"\") + 1);
                        Artista.InnerText = Artista.InnerText.Substring(0, Artista.InnerText.Length - 1).ToUpper();
                        string[] sListExtensions = sExtensiones.Split(';');
                        foreach (string sExten in sListExtensions)
                        {
                            NombreCancion.InnerText = Separados[1].ToString().Substring(1, Separados[1].Length - 1).ToUpper().Replace(sExten.ToUpper().Replace("*", ""), "").ToUpper();
                        }
                        //NombreCancion.InnerText = Separados[1].ToString().Substring(1, Separados[1].Length - 1).Replace(".mp4", "").ToUpper();
                    }
                    else
                    {
                        Artista.InnerText = Cancion.ToUpper();
                        NombreCancion.InnerText = Cancion.ToUpper();
                    }
                    UbicacionCancion.InnerText = Cancion;
                    FechaCreacion.InnerText = DateTime.Now.ToString();
                    node.AppendChild(Artista);
                    node.AppendChild(NombreCancion);
                    node.AppendChild(UbicacionCancion);
                    node.AppendChild(FechaCreacion);
                    doc.DocumentElement.AppendChild(node);
                    doc.Save(Direccion);
                    IncrementCountInList(sDireccionLista, Cancion);
                    return true;
                }
                catch (Exception error)
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    if (!File.Exists(Direccion))
                    {
                        XmlNode node1 = document.CreateNode(XmlNodeType.Element, "Canciones", null);
                        document.AppendChild(node1);
                        document.Save(Direccion);
                    }
                    XmlDocument doc = new XmlDocument();
                    doc.Load(Direccion);
                    XmlNode node = doc.CreateNode(XmlNodeType.Element, "Pista", null);
                    string[] Separados = Cancion.Split('-');
                    XmlNode Artista = doc.CreateNode(XmlNodeType.Element, "Artista", null);
                    XmlNode NombreCancion = doc.CreateNode(XmlNodeType.Element, "NombreCancion", null);
                    XmlNode UbicacionCancion = doc.CreateNode(XmlNodeType.Element, "Direccion", null);
                    XmlNode FechaCreacion = doc.CreateNode(XmlNodeType.Element, "FechaAdicion", null);
                    string[] sListExtensions = sExtensiones.Split(';');
                    foreach (string sExten in sListExtensions)
                    {
                        if (Cancion.ToUpper().Contains(sExten.Replace("*", "").ToUpper()))
                        {
                            NombreCancion.InnerText = Cancion.ToUpper().Replace(sExten.ToUpper().Replace("*", ""), "").Replace(sDireccionRoot, "").Replace(@"\", "").ToUpper();
                            Artista.InnerText = Cancion.ToUpper();
                        }
                    }
                    UbicacionCancion.InnerText = Cancion;
                    FechaCreacion.InnerText = DateTime.Now.ToString();
                    node.AppendChild(Artista);
                    node.AppendChild(NombreCancion);
                    node.AppendChild(UbicacionCancion);
                    node.AppendChild(FechaCreacion);
                    doc.DocumentElement.AppendChild(node);
                    doc.Save(Direccion);
                    IncrementCountInList(sDireccionLista, Cancion);
                    return true;
                }
                catch (Exception error)
                {
                    return false;
                }
            }
        }
        private static void IncrementCountInList(string sDireccionLista, string Cancion)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@sDireccionLista);
            XmlNodeList nodes = xmlDoc.SelectNodes("//Pista[Direccion='" + @Cancion + "']");
            nodes[0].ChildNodes[3].InnerText = (Convert.ToInt32(nodes[0].ChildNodes[3].InnerText) + 1).ToString();
            xmlDoc.Save(@sDireccionLista);
        }
        public bool GenerateListOfSongs(string Direccion, string sListaCaciones, string sExtensiones, bool bTipoListado, bool bResetTopSongs, bool MakeToUpper)
        {
            if (bTipoListado)
            {
                try
                {
                    GetVideosFromDisk(Direccion, sExtensiones, MakeToUpper);
                    XmlDocument document = new XmlDocument();
                    if (File.Exists(@sListaCaciones))
                    {
                        //File.Delete(@sListaCaciones);
                        if (bResetTopSongs)
                        {
                            XmlDocument docXml = new XmlDocument();
                            docXml.Load(@sListaCaciones);
                            XmlNodeList nodeList = docXml.GetElementsByTagName("Contador");
                            foreach (XmlNode node in nodeList)
                            {
                                node.InnerText = "0";
                            }
                            docXml.Save(@sListaCaciones);
                        }
                        else
                        {
                            XmlDocument docXml = new XmlDocument();
                            docXml.Load(@sListaCaciones);
                        }
                        AddNewSongs(@sListaCaciones, sExtensiones, Direccion);
                    }
                    if (!File.Exists(@sListaCaciones))
                    {
                        XmlNode node = document.CreateNode(XmlNodeType.Element, "Canciones", null);
                        document.AppendChild(node);
                        document.Save(@sListaCaciones);
                    }
                    XmlDocument doc = new XmlDocument();
                    doc.Load(@sListaCaciones);
                    foreach (string pista in lstSongs)
                    {
                        if (doc.SelectNodes("Canciones/Pista").Count < lstSongs.Count())
                        {
                            XmlNode node = doc.CreateNode(XmlNodeType.Element, "Pista", null);
                            string[] Separados = pista.Split('-');
                            XmlNode Artista = doc.CreateNode(XmlNodeType.Element, "Artista", null);
                            XmlNode NombreCancion = doc.CreateNode(XmlNodeType.Element, "NombreCancion", null);
                            XmlNode UbicacionCancion = doc.CreateNode(XmlNodeType.Element, "Direccion", null);
                            XmlNode Contador = doc.CreateNode(XmlNodeType.Element, "Contador", null);
                            XmlNode FechaCreacion = doc.CreateNode(XmlNodeType.Element, "FechaAdicion", null);
                            if (Separados.Count() > 0)
                            {
                                Artista.InnerText = Separados[0].ToString().Substring(Separados[0].ToString().LastIndexOf(@"\") + 1);
                                Artista.InnerText = Artista.InnerText.Substring(0, Artista.InnerText.Length - 1).ToUpper();
                                string[] sListExtensions = sExtensiones.Split(';');
                                foreach (string sExten in sListExtensions)
                                {
                                    NombreCancion.InnerText = Separados[1].ToString().ToUpper().Substring(1, Separados[1].Length - 1).Replace(sExten.ToUpper().Replace("*", ""), "").ToUpper();
                                }
                            }   
                            else
                            {
                                Artista.InnerText = pista.ToUpper();
                                NombreCancion.InnerText = pista.ToUpper();
                            }
                            Contador.InnerText = "0";
                            FechaCreacion.InnerText = File.GetCreationTime(@pista).ToString();
                            UbicacionCancion.InnerText = pista;
                            node.AppendChild(Artista);
                            node.AppendChild(NombreCancion);
                            node.AppendChild(UbicacionCancion);
                            node.AppendChild(Contador);
                            node.AppendChild(FechaCreacion);
                            doc.DocumentElement.AppendChild(node);
                            doc.Save(@sListaCaciones);
                        }
                    }

                    return true;
                }
                catch (Exception error)
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    GetVideosFromDisk(Direccion, sExtensiones, MakeToUpper);
                    XmlDocument document = new XmlDocument();
                    if (File.Exists(@sListaCaciones))
                    {
                        //File.Delete(@sListaCaciones);
                        if (bResetTopSongs)
                        {
                            XmlDocument docXml = new XmlDocument();
                            docXml.Load(@sListaCaciones);
                            XmlNodeList nodeList = docXml.GetElementsByTagName("Contador");
                            foreach (XmlNode node in nodeList)
                            {
                                node.InnerText = "0";
                            }
                            docXml.Save(@sListaCaciones);
                        }
                        else
                        {
                            XmlDocument docXml = new XmlDocument();
                            docXml.Load(@sListaCaciones);
                        }
                        AddNewSongs(@sListaCaciones, sExtensiones, Direccion);
                    }
                    if (!File.Exists(@sListaCaciones))
                    {
                        XmlNode node = document.CreateNode(XmlNodeType.Element, "Canciones", null);
                        document.AppendChild(node);
                        document.Save(@sListaCaciones);
                    }
                    XmlDocument doc = new XmlDocument();
                    doc.Load(@sListaCaciones);
                    foreach (string pista in lstSongs)
                    {
                        if (doc.SelectNodes("Canciones/Pista").Count < lstSongs.Count())
                        {
                            XmlNode node = doc.CreateNode(XmlNodeType.Element, "Pista", null);
                            //string[] Separados = pista.Split('-');
                            XmlNode Artista = doc.CreateNode(XmlNodeType.Element, "Artista", null);
                            XmlNode NombreCancion = doc.CreateNode(XmlNodeType.Element, "NombreCancion", null);
                            XmlNode UbicacionCancion = doc.CreateNode(XmlNodeType.Element, "Direccion", null);
                            XmlNode Contador = doc.CreateNode(XmlNodeType.Element, "Contador", null);
                            XmlNode FechaCreacion = doc.CreateNode(XmlNodeType.Element, "FechaAdicion", null);
                            string[] sListExtensions = sExtensiones.Split(';');
                            foreach (string sExten in sListExtensions)
                            {
                                if (pista.ToUpper().Contains(sExten.Replace("*", "").ToUpper()))
                                {
                                    NombreCancion.InnerText = pista.ToUpper().Replace(sExten.ToUpper().Replace("*", ""), "").Replace(Direccion, "").Replace(@"\", "").ToUpper();
                                    Artista.InnerText = pista.ToUpper();
                                }
                            }
                            Contador.InnerText = "0";
                            FechaCreacion.InnerText = File.GetCreationTime(@pista).ToString();
                            UbicacionCancion.InnerText = pista;
                            node.AppendChild(Artista);
                            node.AppendChild(NombreCancion);
                            node.AppendChild(UbicacionCancion);
                            node.AppendChild(Contador);
                            node.AppendChild(FechaCreacion);
                            doc.DocumentElement.AppendChild(node);
                            doc.Save(@sListaCaciones);
                        }
                    }

                    return true;
                }
                catch (Exception error)
                {
                    return false;
                }
            }
        }
        void AddNewSongs(string sListaCaciones, string sExtensiones, string Direccion)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@sListaCaciones);
            XmlNodeList xmlNL = doc.GetElementsByTagName("Pista");
            if (lstSongs.Count > xmlNL.Count)
            {
                foreach (string pista in lstSongs)
                {
                    if (doc.SelectNodes("//Canciones/Pista[Direccion='" + pista + "']").Count == 0)
                    {
                        XmlNode node = doc.CreateNode(XmlNodeType.Element, "Pista", null);
                        //string[] Separados = pista.Split('-');
                        XmlNode Artista = doc.CreateNode(XmlNodeType.Element, "Artista", null);
                        XmlNode NombreCancion = doc.CreateNode(XmlNodeType.Element, "NombreCancion", null);
                        XmlNode UbicacionCancion = doc.CreateNode(XmlNodeType.Element, "Direccion", null);
                        XmlNode Contador = doc.CreateNode(XmlNodeType.Element, "Contador", null);
                        XmlNode FechaCreacion = doc.CreateNode(XmlNodeType.Element, "FechaAdicion", null);
                        string[] sListExtensions = sExtensiones.Split(';');
                        foreach (string sExten in sListExtensions)
                        {
                            if (pista.ToUpper().Contains(sExten.Replace("*", "").ToUpper()))
                            {
                                NombreCancion.InnerText = pista.ToUpper().Replace(sExten.ToUpper().Replace("*", ""), "").Replace(Direccion, "").Replace(@"\", "").ToUpper();
                                Artista.InnerText = pista.ToUpper();
                            }
                        }
                        Contador.InnerText = "0";
                        FechaCreacion.InnerText = File.GetCreationTime(@pista).ToString();
                        UbicacionCancion.InnerText = pista;
                        node.AppendChild(Artista);
                        node.AppendChild(NombreCancion);
                        node.AppendChild(UbicacionCancion);
                        node.AppendChild(Contador);
                        node.AppendChild(FechaCreacion);
                        doc.DocumentElement.AppendChild(node);
                        doc.Save(@sListaCaciones);
                    }
                }
            }
        }
        public DataTable ReadList(string Direccion)
        {
            DataSet ds = new DataSet();
            if (File.Exists(@Direccion))
            {
                System.IO.FileStream fsReadXml = new System.IO.FileStream(@Direccion, System.IO.FileMode.Open);
                try
                {
                    ds.ReadXml(fsReadXml);
                    if (ds.Tables.Count > 0)
                    {
                        ds.Tables[0].DefaultView.Sort = "NombreCancion Asc";
                        return ds.Tables[0];
                    }
                    else return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    fsReadXml.Close();
                }
            }
            else
            {
                XmlDocument document = new XmlDocument();
                XmlNode node1 = document.CreateNode(XmlNodeType.Element, "Canciones", null);
                document.AppendChild(node1);
                document.Save(@Direccion);
                return null;
            }
        }
        public DataTable ReadListWaitingList(string Direccion)
        {
            DataSet ds = new DataSet();
            if (File.Exists(@Direccion))
            {
                System.IO.FileStream fsReadXml = new System.IO.FileStream(@Direccion, System.IO.FileMode.Open);
                try
                {
                    ds.ReadXml(fsReadXml);
                    if (ds.Tables.Count > 0)
                    {
                        return GetSortedTableDate(ds.Tables[0]);
                    }
                    else return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    fsReadXml.Close();
                }
            }
            else
            {
                XmlDocument document = new XmlDocument();
                XmlNode node1 = document.CreateNode(XmlNodeType.Element, "Canciones", null);
                document.AppendChild(node1);
                document.Save(@Direccion);
                return null;
            }
        }
        private DataTable ChangeColumnType(System.Data.DataTable dt, string p, Type type)
        {
            dt.Columns.Add(p + "_new", type);
            foreach (System.Data.DataRow dr in dt.Rows)
            {   // Will need switch Case for others if Date is not the only one.
                dr[p + "_new"] = Convert.ToDateTime(dr[p].ToString()); // dr[p].ToString();
            }
            dt.Columns.Remove(p);
            dt.Columns[p + "_new"].ColumnName = p;
            return dt;
        }
        public DataTable ReadListTop20(string Direccion)
        {
            DataSet ds = new DataSet();
            if (File.Exists(@Direccion))
            {
                System.IO.FileStream fsReadXml = new System.IO.FileStream(@Direccion, System.IO.FileMode.Open);
                try
                {
                    ds.ReadXml(fsReadXml);
                    if (ds.Tables.Count > 0)
                    {
                        return GetSortedTable(ds.Tables[0], 20);
                    }
                    else return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    fsReadXml.Close();
                }
            }
            else
            {
                XmlDocument document = new XmlDocument();
                XmlNode node1 = document.CreateNode(XmlNodeType.Element, "Canciones", null);
                document.AppendChild(node1);
                document.Save(@Direccion);
                return null;
            }
        }
        public DataTable ReadListNewAdded20(string Direccion)
        {
            DataSet ds = new DataSet();
            if (File.Exists(@Direccion))
            {
                System.IO.FileStream fsReadXml = new System.IO.FileStream(@Direccion, System.IO.FileMode.Open);
                try
                {
                    ds.ReadXml(fsReadXml);
                    if (ds.Tables.Count > 0)
                    {
                        return GetSortedTableDateTop20(ds.Tables[0]);
                    }
                    else return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    fsReadXml.Close();
                }
            }
            else
            {
                XmlDocument document = new XmlDocument();
                XmlNode node1 = document.CreateNode(XmlNodeType.Element, "Canciones", null);
                document.AppendChild(node1);
                document.Save(@Direccion);
                return null;
            }
        }
        DataTable GetSortedTable(DataTable dt, int count, string order = "DESC")
        {
            
            DataTable dt2 = dt.Clone();
            dt2.Columns["Contador"].DataType = Type.GetType("System.Int32");
            foreach (DataRow dr in dt.Rows)
            {
                dt2.ImportRow(dr);
            }
            dt2.AcceptChanges();
            DataTable dt3 = dt2.Clone();
            DataRow[] results = dt2.Select("Contador <> 0", "Contador " + order);
            if (count > results.Count())
                count = results.Count();
            for (int i = 0; i < count; ++i)
                dt3.ImportRow(results[i]);

            dt3.DefaultView.Sort = "Contador Desc";


            return dt3;
        }
        DataTable GetSortedTableDate(DataTable dt, string order = "ASC")
        {
            ChangeColumnType(dt, "FechaAdicion", typeof(DateTime));
            dt.DefaultView.Sort = "FechaAdicion Asc";
            return dt;
        }
        DataTable GetSortedTableDateTop20(DataTable dt, string order = "ASC")
        {
            ChangeColumnType(dt, "FechaAdicion", typeof(DateTime));
            DataTable dt2 = dt.Clone();
            DataRow[] results = dt.Select("", "FechaAdicion DESC");
            for (var i = 0; i < 20; i++)
                dt2.ImportRow(results[i]);
            return dt2;
        }
        //public DataTable ReadList(string sDireccion)
        //{
        //    DataSet ds = new DataSet();
        //    System.IO.FileStream fsReadXml = new System.IO.FileStream(@sDireccion, System.IO.FileMode.Open);
        //    try
        //    {
        //        ds.ReadXml(fsReadXml);
        //        return ds.Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        fsReadXml.Close();
        //    }
        //}
        public bool DeleteFromWaitList(string Direccion, string Cancion)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Direccion);
                XmlNodeList nodes = doc.SelectNodes("//Pista[Direccion='" + @Cancion + "']");
                //for (int i = nodes.Count - 1; i >= 0; i--)
                //{
                nodes[0].ParentNode.RemoveChild(nodes[0]);
                //}
                doc.Save(Direccion);
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
        public string GetFirstSongFromList(string Direccion)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Direccion);
                XmlNodeList nodes = doc.SelectNodes("//Pista/Direccion");
                return nodes[0].InnerText;
            }
            catch (Exception err)
            {
                return string.Empty;
            }
        }
    }
}
