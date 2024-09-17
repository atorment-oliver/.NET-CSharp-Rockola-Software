using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;

namespace RockoTouch
{
    class MonitorDrives
    {
        public enum EventType
        {
            Inserted = 2,
            Removed = 3
        }
        public static int CopyDataFromUsbToHardDrive(string sDireccionCanciones, string sExtensiones)
        {
            int iCantidadVideos = 0;
            var drives = DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.DriveType == DriveType.Removable);
            foreach (var drive in drives)
            {
                iCantidadVideos = iCantidadVideos + CopyVideosFromUsb(@drive.ToString(), sDireccionCanciones, sExtensiones);
            }
            return iCantidadVideos;
        }
        static int CopyVideosFromUsb(string DriveName, string Direccion, string Extension)
        {
            string[] vExtensiones = Extension.Split(';');
            int iCantidadCanciones = 0;
            foreach (string Exten in vExtensiones)
            {
                iCantidadCanciones = iCantidadCanciones + Directory.GetFiles(@DriveName, Exten).Count();
                foreach (string file in Directory.GetFiles(@DriveName, Exten))
                {
                    if (file.Contains("'"))
                    {
                        File.Copy(file, @Direccion + "\\" + Path.GetFileName(file).Replace("'", ""), true);
                    }
                    else
                    {
                        File.Copy(file, @Direccion + "\\" + Path.GetFileName(file), true);
                    }
                }
            }
            return iCantidadCanciones;
        }
    }
}
