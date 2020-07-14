using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace APPMOBLIE.Model
{
  public  class PersonInfo
    {
        [PrimaryKey , AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Userimage { get; set; }
    }
}
