﻿namespace AirTravelService.DataAccess.Exceptions;

public class AggregateUpdateException: Exception
{
    public AggregateUpdateException(string message) : base(message)
    {
    }

    public AggregateUpdateException(string message, Exception innerException) : base(message, innerException)
    {
    }
}