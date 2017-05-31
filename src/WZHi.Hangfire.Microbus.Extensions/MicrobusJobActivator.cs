// Assembly           : WZHi.Hangfire.Microbus.Extensions
// Author              : Topsey
// Created On        : 21/5/2017
// Last Modified By : Ian
// Last Modified On : 24/5/2017 at 2:36 PM
// **************************************************************************
// <summary>Based on Derek Comartin article Background Commands with MediatR and Hangfire</summary>

using System;
using Enexure.MicroBus;
using Hangfire;
using WZHi.Hangfire.Microbus.Integration.Extensions;
using WZHi.Hangfire.Microbus.Integration.Infrastructure;

namespace WZHi.Hangfire.Microbus.Integration
{
    public class MicrobusJobActivator : JobActivator
    {
        private readonly HangfireMicrobusContext _context;
        private readonly IMicroMediator _mediator;

        public MicrobusJobActivator(IMicroMediator mediator, HangfireMicrobusContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public override object ActivateJob(Type type)
        {
            return new HangfireMicrobusMediator(_mediator, _context);
        }
    }
}