// Assembly           : WZHi.Hangfire.Microbus.Extensions
// Author              : Topsey
// Created On        : 21/5/2017
// Last Modified By : Ian
// Last Modified On : 24/5/2017 at 2:36 PM
// **************************************************************************
// <summary>Based on Derek Comartin article Background Commands with MediatR and Hangfire</summary>

using Enexure.MicroBus;
using Hangfire;
using Hangfire.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WZHi.Hangfire.Microbus.Integration.Infrastructure;

namespace WZHi.Hangfire.Microbus.Integration.Extensions
{
    public static class HangfireMicorbusExtension

    {
        public delegate HangfireMicrobusContext DatabaseContext();

        public static HangfireMicrobusContext Context { get; set; }

        public static IGlobalConfiguration UseMicrobus(this IGlobalConfiguration config, IMicroMediator mediator, HangfireMicrobusContext context)
        {
            context.Database.Migrate();

            config.UseActivator(new MicrobusJobActivator(mediator, context));

            JobHelper.SetSerializerSettings(new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });

            Context = context;

            return config;
        }
    }
}