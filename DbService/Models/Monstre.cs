﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DbService
{
    public partial class Monstre
    {
        public int IdMonstre { get; set; }
        public int IdGroupe { get; set; }
        public int IdRace { get; set; }

        public virtual GroupeMonstre IdGroupeNavigation { get; set; }
        public virtual RaceMonstre IdRaceNavigation { get; set; }
    }
}