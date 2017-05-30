// Assembly           : Enexure.MicroBus.Sagas.Infrastructure
// Author              : Topsey
// Created On        : 28/5/2017
// Last Modified By : Ian
// Last Modified On : 28/5/2017 at 4:21 PM
// **************************************************************************
// <summary></summary>

using System;
using System.Threading.Tasks;
using Enexure.MicroBus.Sagas.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace Enexure.MicroBus.Sagas.Infrastructure.Context
{
    public interface IMicrobusSagaContext: IDisposable
    {
        DbSet<MicrobusSaga> MicrobusSagas { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}