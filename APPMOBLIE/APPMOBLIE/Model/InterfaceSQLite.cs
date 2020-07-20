using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace APPMOBLIE.Model
{
    public interface InterfaceSQLite
    {
        SQLiteAsyncConnection GetConnection();
    }
}
