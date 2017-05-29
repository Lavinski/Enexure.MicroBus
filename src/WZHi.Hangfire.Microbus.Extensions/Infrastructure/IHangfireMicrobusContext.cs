// Assembly           : WZHi.Hangfire.Microbus.Extensions
// Author              : Topsey
// Created On        : 22/5/2017
// Last Modified By : Ian
// Last Modified On : 24/5/2017 at 2:36 PM
// **************************************************************************
// <summary>Based on Derek Comartin article Background Commands with MediatR and Hangfire</summary>

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WZHi.Hangfire.Microbus.Integration.Model;

namespace WZHi.Hangfire.Microbus.Integration.Infrastructure
{
    public interface IHangfireMicrobusContext : IDisposable
    {
        DbSet<MicrobusRequest> MicrobusRrequests { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}