using System;

namespace REVISAO.Models{

    public class User
    {
        public int Id { get ; set; }
        public int Age { get ; set; }
        public string? name { get ; set; }

        public DateTime CriadoEm { get ; set; } = DateTime.Now;
    }
        

}

