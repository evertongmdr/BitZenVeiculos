using BitZenVeiculos.Domain.Entities;
using BitZenVeiculos.Repository.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using static BitZenVeiculos.Domain.DTOs.ReportDTO;

namespace BitZenVeiculos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly BitZenVeiculosContext _reportContext;
        public ReportsController(BitZenVeiculosContext reportContext)
        {
            _reportContext = reportContext;
        }

        //api/reports/liters
        [HttpGet("liters")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetLiters([FromBody] ReportRequestDTO liter)
        {

            DateTime start = liter.Start;
            DateTime end = liter.End;

            var diffMonths = DiffMonths(start, end);

            var whereGetFuelSupply = WhereGetFuelSupply(start, end);

            var fuelSupply = await _reportContext.FuelsSuplly.
               Where(whereGetFuelSupply).ToListAsync();

            dynamic report = new ExpandoObject();
            var reportDic = (IDictionary<string, object>)report;

            for (int i = 0; i <= diffMonths; i++)
            {
                var fuelSupplyMonth = fuelSupply.Where(fs => fs.DateOfSupply.Month == start.AddMonths(i).Month);

                decimal liters = 0;
                foreach (var fs in fuelSupplyMonth)
                {
                    liters+= fs.SupplyedLiters;
                }

                reportDic.Add(start.AddMonths(i).ToString("MMMM"), liters);
            }

            return Ok(reportDic);
        }

        //api/reports/payed
        [HttpGet("payed")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetPayed([FromBody] ReportRequestDTO liter)
        {

            DateTime start = liter.Start;
            DateTime end = liter.End;

            var diffMonths = DiffMonths(start, end);

            var whereGetFuelSupply = WhereGetFuelSupply(start,end);

            var fuelSupply = await _reportContext.FuelsSuplly.
               Where(whereGetFuelSupply).ToListAsync();

            dynamic report = new ExpandoObject();
            var reportDic = (IDictionary<string, object>)report;

            for (int i = 0; i <= diffMonths; i++)
            {
                var fuelSupplyMonth = fuelSupply.Where(fs => fs.DateOfSupply.Month == start.AddMonths(i).Month);

                decimal payed = 0;
                foreach (var fs in fuelSupplyMonth)
                {
                    payed += fs.ValuePay;
                }

                reportDic.Add(start.AddMonths(i).ToString("MMMM"), payed);
            }

            return Ok(reportDic);
        }

        //api/reports/payed
        [HttpGet("mileage-walked")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetMileageWalked([FromBody] ReportRequestDTO liter)
        {

            DateTime start = liter.Start;
            DateTime end = liter.End;

            var diffMonths = DiffMonths(start, end);

            var whereGetFuelSupply = WhereGetFuelSupply(start, end);

            var fuelSupply = await _reportContext.FuelsSuplly.
               Where(whereGetFuelSupply).ToListAsync();

            var vechilesIds = fuelSupply.Select(fs => fs.VehicleId).ToList();

            var vehiclesFueldMonths = await _reportContext.Vehicles.Where(v=> vechilesIds.Contains(v.Id)).ToListAsync();

            decimal sumMileage = 0;

            dynamic report = new ExpandoObject();
            var reportDic = (IDictionary<string, object>)report;
            for (int j = 0; j < diffMonths; j++)
            {
                var fuelSupplyMonth = fuelSupply.Where(fs => fs.DateOfSupply.Month == start.AddMonths(j).Month).ToList();
                sumMileage = 0;

                if (fuelSupplyMonth.Count() >0)
                {
                    foreach (var vehicle in vehiclesFueldMonths)
                    {
                        var fuelSupplyMonthThisVehicle = fuelSupplyMonth.Where(fs => fs.VehicleId == vehicle.Id)
                            .OrderBy(fs => fs.DateOfSupply).ToList();

                        int tamFSThisVehicle = fuelSupplyMonthThisVehicle.Count();

                        if (tamFSThisVehicle > 1)
                        {
                            var fsAux = _reportContext.FuelsSuplly.Where(fs => fs.VehicleId == vehicle.Id && fs.DateOfSupply < fuelSupplyMonthThisVehicle[0].DateOfSupply)
                               .OrderBy(fs => fs.DateOfSupply).Take(1).ToList();


                            if (fsAux.Count() == 0)
                            {
                                sumMileage += fuelSupplyMonthThisVehicle[0].SupplyedMileage - vehicle.Mileage;
                            }
                            else
                            {
                                sumMileage += fuelSupplyMonthThisVehicle[0].SupplyedMileage - fsAux[0].SupplyedMileage;
                            }

                            for (int i = 0; i < tamFSThisVehicle - 1; i++)
                            {
                                sumMileage += fuelSupplyMonthThisVehicle[i + 1].SupplyedMileage - fuelSupplyMonthThisVehicle[i].SupplyedMileage;
                            }

                            
                        }
                        else
                        {

                            if (tamFSThisVehicle != 0)
                            {
                                var fsAux = _reportContext.FuelsSuplly.Where(fs => fs.VehicleId == vehicle.Id && fs.DateOfSupply < fuelSupplyMonthThisVehicle[0].DateOfSupply)
                                .OrderBy(fs => fs.DateOfSupply).Take(1).ToList();


                                if (fsAux.Count() == 0)
                                {
                                    sumMileage += fuelSupplyMonthThisVehicle[0].SupplyedMileage - vehicle.Mileage;
                                }
                                else
                                {
                                    sumMileage += fuelSupplyMonthThisVehicle[0].SupplyedMileage - fsAux[0].SupplyedMileage;
                                }

                            }
                        }

                    }
                }
              
                reportDic.Add(start.AddMonths(j).ToString("MMMM"), sumMileage);


            }

            return Ok(reportDic);
        }

        private int DiffMonths(DateTime start, DateTime end)
        {
           return (int)(end.Subtract(start).Days / (365.25 / 12));
        }

        Expression<Func<FuelSupply, bool>> WhereGetFuelSupply(DateTime start, DateTime end)
        {
            Expression<Func<FuelSupply, bool>> where = (fs)
               => fs.DateOfSupply >= start && fs.DateOfSupply <= end;
            return where;
        }


    }
}
