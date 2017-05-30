// Assembly           : Enexure.MicroBus.Sagas.Infrastructure
// Author              : Topsey
// Created On        : 28/5/2017
// Last Modified By : Ian
// Last Modified On : 28/5/2017 at 4:21 PM
// **************************************************************************
// <summary></summary>

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enexure.MicroBus.Sagas.Infrastructure.Model
{
    public interface IModificationHistory
    {
         DateTime ExpireAt { get; set; }
         DateTime CreatedAt { get; set; }

         DateTime CompletedAt { get; set; }

        [NotMapped]
        bool IsDirty { get; set; }
    }
}