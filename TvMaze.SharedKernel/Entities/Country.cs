﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TvMaze.SharedKernel.Entities;

[ComplexType]
public record Country(string Name, string Code, string Timezone);