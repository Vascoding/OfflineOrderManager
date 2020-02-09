﻿using OfflineOrderManager.Services.Contracts;
using OfflineOrderManager.Web.Pages.Abstractions.Orders;
using System.Collections.Generic;

namespace OfflineOrderManager.Web.Pages.Orders
{
    public class AllModel : ListingOrderModel
    {
        public AllModel(IEntityService entityService, IMappingService mapper) 
            : base(entityService, mapper) { }
    }
}