﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceData.Models
{
    public class CartItem : BaseModel
    {
        /*[ForeignKey(nameof(Cart))]
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }*/
    }
}
