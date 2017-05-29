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
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WZHi.Hangfire.Microbus.Integration.Infrastructure;

namespace WZHi.Hangfire.Microbus.Integration.Extensions
{
    public class HangfireMicrobusMediator
    {
        private readonly HangfireMicrobusContext _context;
        private readonly IMicroMediator _mediator;

        public HangfireMicrobusMediator(IMicroMediator mediator, HangfireMicrobusContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task SendCommandAsync(ICommand command)
        {
            await _mediator.SendAsync(command);
        }

        public async Task ProcessAsync(Guid id)
        {
            using (var db = HangfireMicorbusExtension.Context)
            {
                try
                {
                    var data = await db.MicrobusRrequests.SingleAsync(s => s.Id == id);

                    var request = JsonConvert.DeserializeObject<ICommand>(data.ToString(),
                        new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

                    await _mediator.SendAsync(request).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException($"Request (id) not found.");
                }
            }
        }
    }
}