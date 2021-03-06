﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfflineOrderManager.Models.Enums;
using OfflineOrderManager.Services.Contracts;
using System.ComponentModel.DataAnnotations;

namespace OfflineOrderManager.Web.Pages.Abstractions.Orders
{
    [BindProperties]
    public abstract class OrderModel : PageModel
    {
        protected readonly IEntityService entityService;

        public OrderModel(IEntityService entityService)
        {
            this.entityService = entityService;
        }

        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Amount { get; set; }

        public string Payed { get; set; }

        public string LeftToPay { get; set; }

        public string Comment { get; set; }

        public string CustomerName { get; set; }

        [Required]
        public string CustormerPhoneNumber { get; set; }

        public Status Status { get; set; }
    }
}