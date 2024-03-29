﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Extensions;
using Cvl.ApplicationServer.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cvl.ApplicationServer.Server.Setup
{    

    public static class ApplicationServerExtensions
    {
        public static IServiceCollection UseApplicationServer(this IServiceCollection services,
            string applicationServerContextConnectionString)            
        {
            services.UseRegisterApplicationServer(applicationServerContextConnectionString);
            services.AddControllers()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.PropertyNamingPolicy = null); //needed for telerik datasource (property need to start from Uppercase)
            services.AddRazorPages();

            return services;
        }
    }
}
