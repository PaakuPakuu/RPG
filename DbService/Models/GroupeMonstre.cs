﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DbService
{
    public partial class GroupeMonstre
    {
        public GroupeMonstre()
        {
            Monstre = new HashSet<Monstre>();
        }

        public int IdGroupeMonstre { get; set; }
        public int IdMap { get; set; }
        public sbyte EstBattu { get; set; }

        public virtual Map IdMapNavigation { get; set; }
        public virtual ICollection<Monstre> Monstre { get; set; }
    }
}