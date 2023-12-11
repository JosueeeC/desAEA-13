﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.Request;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceCustomController : ControllerBase
    {
        private readonly ProjectContext _projectContext;
        public InvoiceCustomController(ProjectContext projectContext)
        {
            _projectContext = projectContext;
        }


        [HttpPost]
        public List<Invoice> GetByFilter(string request)
        {
            var response = _projectContext.Invoices.Include(x=> x.Customer.FirstName.Contains(request)).ToList();

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<Invoice>> InsertInvoiceCustom(Invoice_Request_v1 request)
        {
            try
            {
                Invoice invoice = new Invoice();
                invoice.CustomerId = request.CustomerId;
                invoice.Date = request.Date;
                invoice.InvoiceNumber = request.InvoiceNumber;
                invoice.Total = request.Total;


                _projectContext.Invoices.Add(invoice);
                await _projectContext.SaveChangesAsync();

                return CreatedAtAction("InsertInvoiceCustom", new { id = invoice.InvoiceId }, invoice);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        

    }
}
