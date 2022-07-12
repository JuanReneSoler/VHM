#!/bin/bash

dotnet ef dbcontext scaffold "Server=10.0.0.9;Database=VHM;User Id=sa;Password=Masterxp01;Trusted_Connection=True;Integrated Security=False" Microsoft.EntityFrameworkCore.SqlServer -o Entities -c VHMContext --project ../Arquitecture/Persistence/Persistence.csproj


