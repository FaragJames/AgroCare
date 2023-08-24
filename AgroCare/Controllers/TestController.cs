using AgroCare.Data.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Newtonsoft.Json;
using Services;
using System;
using System.Numerics;
using System.Reflection;

namespace AgroCare.Controllers
{
    public class TestController : Controller
    {
        readonly FarmerService farmerService;
        readonly IService<Buyer> buyerService;
        Dictionary<string, object> services = new();

        public TestController(FarmerService farmerService, IService<Buyer> buyerService)
        {
            this.farmerService = farmerService;
            services.Add(farmerService.GetType().FullName!, farmerService);

            this.buyerService = buyerService;
            services.Add(buyerService.GetType().FullName!, buyerService);
        }

        public string Reflection(string objectName)
        {
            try
            {
                #region Farmer
                object farmer = new Farmer()
                {
                    UserName = "TestUsername",
                    Name = "TestName",
                    Phone = "TestPhone"
                };


                (string serviceName, object service) = services.Where(entry => entry.Key.Contains(objectName))
                    .First();


                Type serviceType = Type.GetType(serviceName + ", Services")!,
                    farmerType = Type.GetType("Models.Models." + objectName + ", Models")!;


                MethodInfo method = serviceType.GetMethod("AddAsync", new Type[] { farmerType })!;
                method.Invoke(service, new object[] { farmer });
                #endregion

                #region Buyer
                //object buyer = new Buyer()
                //{
                //    UserName = "TestUsername",
                //    Name = "TestName",
                //    Phone = "TestPhone"
                //};


                //(string serviceName, object service) = services.Where(entry => entry.Key.Contains(objectName))
                //    .First();


                //Type serviceType = Type.GetType(serviceName + ", Services")!,
                //    buyerType = Type.GetType("Models.Models." + objectName + ", Models")!;


                //MethodInfo method = serviceType.GetMethod("AddAsync", new Type[] { buyerType })!;
                //method.Invoke(service, new object[] { buyer });
                #endregion

                return "true";
            }
            catch (Exception)
            {
                return "false";
            }
        }

        public void test()
        {
            var type = buyerService.GetType();
        }
    }
}
