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
   [Table("Microbus.Sagas")]
    public class MicrobusSaga : IModificationHistory
    {
        public Guid Id { get; set; }

        [Column(TypeName = "varbin(MAX)")]
        public byte[] Saga { get; set; }

        public DateTime ExpireAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime CompletedAt { get; set; }
        public bool IsDirty { get; set; }
    }
}