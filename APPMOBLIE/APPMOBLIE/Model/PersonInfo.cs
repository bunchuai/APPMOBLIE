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
        public string Nickname { get; set; }
        public byte[] Userimage { get; set; }
    }
}
