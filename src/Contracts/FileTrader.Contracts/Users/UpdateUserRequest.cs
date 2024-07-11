﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FileTrader.Contracts.Users
{
    /// <summary>
    /// Модель для обновления записи пользователя
    /// </summary>
    public class UpdateUserRequest
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [StringLength(20)]
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Login { get; set; }
        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        [StringLength(256)]
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? UserEmail { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [StringLength(30)]
        [AllowNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Password  { get; set; }
    }
}
