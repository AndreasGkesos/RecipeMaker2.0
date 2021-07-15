using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeMaker2._0.DTO;
using RecipeMaker2._0.Interfaces;
using RecipeMaker2._0.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMaker2._0.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Operator,Admin")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IReportService reportService, IMapper mapper, ILogger<ReportController> logger)
        {
            _reportService = reportService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Route("calculatemeals")]
        public async Task<IActionResult> CalculateMeals(CalculateMealsDTO data)
        {
            var result = await _reportService.CalculateMeals(data.Quantity, data.Meals, data.Date);

            var builder = new StringBuilder();
            builder.AppendLine("MealId,ProductQuantityId,ProductId,SupplierId,PQQuantity,ProductName,ProductQuanyity,SupplierName,Preference,DaysBefore,NeedsExtra,ExtraQuantity,InformDate");
            foreach (var item in result)
            {
                builder.AppendLine($"{item.MealId},{item.ProductQuantityId},{item.ProductId},{item.SupplierId},{item.PQQuantity},{item.ProductName},{item.ProductQuanyity},{item.SupplierName},{item.Preference},{item.DaysBefore},{item.NeedsExtra},{item.ExtraQuantity},{item.InformDate.ToShortDateString()}");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Report.csv");
        }
    }
}
