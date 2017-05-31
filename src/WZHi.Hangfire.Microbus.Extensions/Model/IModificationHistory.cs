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
    public interface IModificationHistory
    {
        DateTime CreatedAt { get; set; }

        [NotMapped]
        bool IsDirty { get; set; }
    }
}