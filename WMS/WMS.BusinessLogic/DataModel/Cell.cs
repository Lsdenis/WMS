//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WMS.BusinessLogic.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cell
    {
        public Cell()
        {
            this.GoodsInCells = new HashSet<GoodsInCell>();
        }
    
        public int Id { get; set; }
        public int Number { get; set; }
        public int WarehouseId { get; set; }
        public int MaxValue { get; set; }
    
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<GoodsInCell> GoodsInCells { get; set; }
    }
}
