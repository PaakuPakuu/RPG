﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DbService
{
    public partial class Inventaire
    {
        public Inventaire()
        {
            Item = new HashSet<Item>();
            Joueur = new HashSet<Joueur>();
        }

        public int IdInventaire { get; set; }
        public int? IdBoutique { get; set; }
        public int Slots { get; set; }
        public string Titre { get; set; }

        public virtual Boutique IdBoutiqueNavigation { get; set; }
        public virtual ICollection<Item> Item { get; set; }
        public virtual ICollection<Joueur> Joueur { get; set; }
    }
}