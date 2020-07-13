using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using APPMOBLIE.iOS;
using APPMOBLIE.Model;
using Foundation;
using SQLite;
using UIKit;

[assembly:Xamarin.Forms.Dependency(typeof(SQLiteInterface))]

namespace APPMOBLIE.iOS
{
   public class SQLiteInterface : InterfaceSQLite
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var DatabasePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var CompletePath = Path.Combine(DatabasePath, "SQLiteDatabase.db3");
            return new SQLiteAsyncConnection(CompletePath);
        }
    }
}