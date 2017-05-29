// Assembly           : WZHi.Hangfire.Microbus.Extensions
// Author              : Topsey
// Created On        : 21/5/2017
// Last Modified By : Ian
// Last Modified On : 24/5/2017 at 2:36 PM
// **************************************************************************
// <summary>Based on Derek Comartin article Background Commands with MediatR and Hangfire</summary>

using System;
using System.Threading.Tasks;
using Enexure.MicroBus;
using Hangfire;
using Newtonsoft.Json;
using WZHi.Hangfire.Microbus.Integration.Model;

namespace WZHi.Hangfire.Microbus.Integration.Extensions
{
    public static class MicrobusExtension
    {
        public static void Enqueue(this IMicroMediator mediator, ICommand command)
        {
            BackgroundJob.Enqueue<HangfireMicrobusMediator>(m => m.SendCommandAsync(command));
        }

        public static void Enqueue(this IMicroMediator mediator,  TimeSpan delay, ICommand command)
        {
            BackgroundJob.Schedule<HangfireMicrobusMediator>(m => m.SendCommandAsync(command),delay);
        }

        public static void Enqueue(this IMicroMediator mediator, DateTimeOffset enqueueAt, ICommand command)
        {
            BackgroundJob.Schedule<HangfireMicrobusMediator>(m => m.SendCommandAsync(command), enqueueAt);
        }

        public static async Task Enqueue(this IMicroMediator mediator, Guid id, ICommand command)// todo add scheduling
        {
            using (var db = HangfireMicorbusExtension.Context)
            {
                await db.MicrobusRrequests.AddAsync(new MicrobusRequest
                {
                    Id = id,
                    Body = JsonConvert.SerializeObject(command, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
                });

                await db.SaveChangesAsync();

                BackgroundJob.Enqueue<HangfireMicrobusMediator>(m => m.ProcessAsync(id));
            }
        }
    }
}