﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WLA.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class wlaEntities : DbContext
    {
        public wlaEntities()
            : base("name=wlaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityGroupList> ActivityGroupLists { get; set; }
        public virtual DbSet<ActivityGroup> ActivityGroups { get; set; }
        public virtual DbSet<Fungsi> Fungsi { get; set; }
        public virtual DbSet<Jabatan> Jabatan { get; set; }
        public virtual DbSet<Pelaksana> Pelaksana { get; set; }
        public virtual DbSet<Periode> Periode { get; set; }
        public virtual DbSet<ResumeGroup> ResumeGroup { get; set; }
        public virtual DbSet<Standard_Time> Standard_Time { get; set; }
        public virtual DbSet<WLAHeader> WLAHeaders { get; set; }
        public virtual DbSet<WLATrx> WLATrx { get; set; }
    }
}
