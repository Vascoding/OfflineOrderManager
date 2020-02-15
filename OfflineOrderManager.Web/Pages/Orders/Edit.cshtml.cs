﻿using Microsoft.AspNetCore.Mvc;
using OfflineOrderManager.Models.Data.Orders;
using OfflineOrderManager.Services.Contracts;
using OfflineOrderManager.Web.Pages.Abstractions.Orders;

namespace OfflineOrderManager.Web.Pages.Orders
{
    public class EditModel : OrderModel
    {
        public EditModel(IEntityService entityService) 
            : base(entityService) { }

        public void OnGet(int id)
        {
            var order = this.entityService.GetBy<Order>(o => o.Id == id);

            this.Id = order.Id;
            this.ProductName = order.ProductName;
            this.Amount = order.Amount;
            this.Payed = order.Payed;
            this.LeftToPay = order.LeftToPay;
            this.Comment = order.Comment;
            this.CustomerName = order.CustomerName;
            this.CustormerPhoneNumber = order.CustormerPhoneNumber;
            this.Status = order.Status;
        }

        public IActionResult OnPost()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "You need to login first";

                return RedirectToPage(new { id = this.Id });
            }

            var order = this.entityService.GetBy<Order>(o => o.Id == this.Id);

            order.ProductName = this.ProductName;
            order.Amount = this.Amount;
            order.Payed = this.Payed;
            order.LeftToPay = this.Amount - this.Payed;
            order.CustomerName = this.CustomerName;
            order.CustormerPhoneNumber = this.CustormerPhoneNumber;
            order.Comment = this.Comment;
            order.Status = this.Status;
            

            this.entityService.AddOrUpdate(order);

            return RedirectToPage("All");
        }
    }
}