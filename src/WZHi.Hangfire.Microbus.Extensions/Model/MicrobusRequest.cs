// Assembly           : WZHi.Hangfire.Microbus.Extensions
// Author              : Topsey
// Created On        : 22/5/2017
// Last Modified By : Ian
// Last Modified On : 24/5/2017 at 2:36 PM
// **************************************************************************
// <summary>Based on Derek Comartin article Background Commands with MediatR and Hangfire</summary>

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WZHi.Hangfire.Microbus.Integration.Model
{
    // note I still prefer the job activator rather than this method of placing directly in the db to be reread by hangfire

    [Table("Microbus.Request")]
    public class MicrobusRequest : IModificationHistory
    {
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Body { get; set; }

        public string StateName { get; set; }

        public DateTime ExpireAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime CompletedAt { get; set; }
        public bool IsDirty { get; set; }
    }
}