﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bins_PcQuickStart
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Bins_Pc_HelperEntities : DbContext
    {
        public Bins_Pc_HelperEntities()
            : base("name=Bins_Pc_HelperEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<business_control> business_control { get; set; }
        public virtual DbSet<quick_app_info> quick_app_info { get; set; }
    }
}