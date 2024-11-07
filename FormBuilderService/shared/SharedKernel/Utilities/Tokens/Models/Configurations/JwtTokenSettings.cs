﻿using System.Diagnostics.CodeAnalysis;

namespace SharedKernel.Utilities.Tokens.Models.Configurations
{
    public class JwtTokenSettings
    {
        public static string SectionName = "JwtTokens";

        [NotNull]
        public string ValidIssuer { get; set; } = default!;

        [NotNull]
        public string ValidAudience { get; set; } = default!;

        [NotNull]
        public string SecretKey { get; set; } = default!;

        [NotNull]
        public ushort LifeTime { get; set; } = default!;
    }
}