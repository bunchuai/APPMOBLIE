using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using APPMOBLIE.Droid;
using APPMOBLIE.Model;
using SQLite;

[assembly:Xamarin.Forms.Dependency(typeof(SQLiteInterface))]

namespace APPMOBLIE.Droid
{
   public class SQLiteInterface : InterfaceSQLite
    {
        public SQLiteAsyncConnection GetConnection()
        {
          var DatabasePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var CompletePath = System.IO.Path.Combine(DatabasePath, "SQLiteDatabase.db3");
            return new SQLiteAsyncConnection(CompletePath);
        }
    }
}